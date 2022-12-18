using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;
using Object = UnityEngine.Object;

namespace Re.InGame.Presentation.Controller
{
    public sealed class GoalState : BaseState
    {
        private readonly OutGame.Domain.UseCase.SoundUseCase _soundUseCase;
        private readonly StageUseCase _stageUseCase;
        private readonly ClearView _clearView;

        public GoalState(OutGame.Domain.UseCase.SoundUseCase soundUseCase,
            StageUseCase stageUseCase, ClearView clearView)
        {
            _soundUseCase = soundUseCase;
            _stageUseCase = stageUseCase;
            _clearView = clearView;
        }

        public override GameState state => GameState.Goal;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // ゴール消滅演出
            _soundUseCase.SetUpPlaySe(OutGame.SeType.Goal);
            var goal = Object.FindObjectOfType<GoalView>();
            await goal.DisappearAsync(token);

            // ステージクリア演出
            _soundUseCase.SetUpPlaySe(OutGame.SeType.Clear);
            await _clearView.ShowAsync(UiConfig.ANIMATION_TIME, token);

            // 最終ステージである場合
            if (_stageUseCase.IsAllStageClear())
            {
                return GameState.Result;
            }
            else
            {
                _stageUseCase.Increase();
                return GameState.SetUp;
            }
        }
    }
}
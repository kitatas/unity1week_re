using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;
using Object = UnityEngine.Object;

namespace Re.InGame.Presentation.Controller
{
    public sealed class GoalState : BaseState
    {
        private readonly StageUseCase _stageUseCase;
        private readonly ClearView _clearView;

        public GoalState(StageUseCase stageUseCase, ClearView clearView)
        {
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
            var goal = Object.FindObjectOfType<GoalView>();
            await goal.DisappearAsync(token);

            // ステージクリア演出
            await _clearView.ShowAsync(UiConfig.ANIMATION_TIME, token);

            // 最終ステージである場合
            if (_stageUseCase.IsAllStageClear())
            {
                // TODO: game clear
                UnityEngine.Debug.Log($"game clear!!");
                return GameState.None;
            }
            else
            {
                _stageUseCase.Increase();
                return GameState.SetUp;
            }
        }
    }
}
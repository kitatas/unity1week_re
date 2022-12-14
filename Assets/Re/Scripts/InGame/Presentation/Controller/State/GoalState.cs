using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

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
            _clearView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            // ステージクリア演出
            var animationTime = 0.5f;
            await _clearView.ShowAsync(animationTime, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
            await _clearView.HideAsync(animationTime, token);

            // TODO: 最終ステージである場合
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
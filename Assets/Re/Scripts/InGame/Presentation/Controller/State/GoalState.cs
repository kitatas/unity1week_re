using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Controller
{
    public sealed class GoalState : BaseState
    {
        private readonly ClearView _clearView;

        public GoalState(ClearView clearView)
        {
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
            if (true)
            {
                // TODO: game clear
                UnityEngine.Debug.Log($"game clear!!");
                return GameState.None;
            }
            else
            {
                return GameState.SetUp;
            }
        }
    }
}
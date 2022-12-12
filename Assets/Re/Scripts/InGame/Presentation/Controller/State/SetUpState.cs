using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Re.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // TODO: 演出
            // 仮の待機時間
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            return GameState.Input;
        }
    }
}
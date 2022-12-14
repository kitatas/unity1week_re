using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;

namespace Re.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        private readonly StageUseCase _stageUseCase;

        public SetUpState(StageUseCase stageUseCase)
        {
            _stageUseCase = stageUseCase;
        }

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

            _stageUseCase.BuildStage();

            return GameState.Input;
        }
    }
}
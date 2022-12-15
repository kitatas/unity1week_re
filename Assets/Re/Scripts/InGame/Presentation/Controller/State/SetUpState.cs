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
            // Stage生成演出
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
            await _stageUseCase.BuildStageAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.Input;
        }
    }
}
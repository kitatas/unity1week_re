using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;
using Object = UnityEngine.Object;

namespace Re.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        private readonly StageUseCase _stageUseCase;
        private StageView _stageView;

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
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            if (_stageView)
            {
                await _stageView.DisappearAsync(UiConfig.ANIMATION_TIME, token);
                Object.Destroy(_stageView.gameObject);
            }

            // Stage生成演出
            _stageView = _stageUseCase.BuildStage();
            await _stageView.AppearAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.Input;
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Controller
{
    public sealed class BackState : BaseState
    {
        private readonly StopPointUseCase _stopPointUseCase;
        private readonly PlayerView _playerView;

        public BackState(StopPointUseCase stopPointUseCase, PlayerView playerView)
        {
            _stopPointUseCase = stopPointUseCase;
            _playerView = playerView;
        }

        public override GameState state => GameState.Back;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            var pointEntity = _stopPointUseCase.Pop();
            _playerView.SetPoint(pointEntity);
            await _playerView.AppearAsync(token);

            return GameState.Judge;
        }
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.Controller;
using UniRx;
using VContainer.Unity;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class StatePresenter : IInitializable, IDisposable
    {
        private readonly StateUseCase _stateUseCase;
        private readonly StateController _stateController;
        private readonly CancellationTokenSource _tokenSource;

        public StatePresenter(StateUseCase stateUseCase, StateController stateController)
        {
            _stateUseCase = stateUseCase;
            _stateController = stateController;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _stateController.InitAsync(_tokenSource.Token).Forget();

            _stateUseCase.gameState
                .Where(x => x != GameState.None)
                .Subscribe(x => ExecAsync(x, _tokenSource.Token).Forget())
                .AddTo(_tokenSource.Token);
        }

        private async UniTaskVoid ExecAsync(GameState state, CancellationToken token)
        {
            try
            {
                var nextState = await _stateController.TickAsync(state, token);
                _stateUseCase.SetState(nextState);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"{state}: {e}");
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}
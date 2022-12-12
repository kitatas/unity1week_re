using Re.InGame.Data.Entity;
using UniRx;

namespace Re.InGame.Domain.UseCase
{
    public sealed class StateUseCase
    {
        private readonly StateEntity _stateEntity;
        private readonly ReactiveProperty<GameState> _gameState;

        public StateUseCase(StateEntity stateEntity)
        {
            _stateEntity = stateEntity;
            _gameState = new ReactiveProperty<GameState>(_stateEntity.value);
        }

        public IReadOnlyReactiveProperty<GameState> gameState => _gameState;

        public void SetState(GameState state)
        {
            _stateEntity.Set(state);
            _gameState.Value = _stateEntity.value;
        }
    }
}
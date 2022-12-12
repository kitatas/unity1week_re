using Re.Common.Data.Entity;

namespace Re.InGame.Data.Entity
{
    public sealed class StateEntity : BaseEntity<GameState>
    {
        public StateEntity()
        {
            Set(GameState.SetUp);
        }
    }
}
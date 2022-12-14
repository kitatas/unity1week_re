using Re.Common.Data.Entity;

namespace Re.InGame.Data.Entity
{
    public sealed class StageLevelEntity : BaseEntity<int>
    {
        public StageLevelEntity()
        {
            Set(0);
        }

        public void Increase()
        {
            Set(value + 1);
        }
    }
}
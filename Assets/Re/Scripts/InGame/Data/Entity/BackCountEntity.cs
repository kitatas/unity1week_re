using Re.Common.Data.Entity;

namespace Re.InGame.Data.Entity
{
    public sealed class BackCountEntity : BaseEntity<int>
    {
        public BackCountEntity()
        {
            Set(0);
        }

        public void Increase()
        {
            Set(value + 1);
        }

        public int GetScore()
        {
            return value * ScoreConfig.BACK_BONUS_RATE;
        }
    }
}
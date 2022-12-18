using Re.Common.Data.Entity;

namespace Re.InGame.Data.Entity
{
    public sealed class ShotCountEntity : BaseEntity<int>
    {
        public ShotCountEntity()
        {
            Set(0);
        }

        public void Increase()
        {
            Set(value + 1);
        }

        public int GetScore()
        {
            return ScoreConfig.SHOT_BONUS - (value * ScoreConfig.SHOT_BONUS_RATE);
        }
    }
}
using Re.InGame.Data.Entity;

namespace Re.InGame.Domain.UseCase
{
    public sealed class ScoreUseCase
    {
        private readonly ShotCountEntity _shotCountEntity;
        private readonly BackCountEntity _backCountEntity;

        public ScoreUseCase(ShotCountEntity shotCountEntity, BackCountEntity backCountEntity)
        {
            _shotCountEntity = shotCountEntity;
            _backCountEntity = backCountEntity;
        }

        public string GetClearBonusStr()
        {
            return $"+{ScoreConfig.CLEAR_BONUS}";
        }

        public string GetShotBonusStr()
        {
            var score = _shotCountEntity.GetScore();
            var sign = score < 0 ? "" : "+";
            return $"{sign}{score}";
        }

        public string GetBackBonusStr()
        {
            return $"+{_backCountEntity.GetScore()}";
        }

        public string GetPlayBonusStr()
        {
            return $"+{ScoreConfig.PLAY_BONUS}";
        }

        public int GetTotalScore()
        {
            return
                ScoreConfig.CLEAR_BONUS
                + _shotCountEntity.GetScore()
                + _backCountEntity.GetScore()
                + ScoreConfig.PLAY_BONUS;
        }
    }
}
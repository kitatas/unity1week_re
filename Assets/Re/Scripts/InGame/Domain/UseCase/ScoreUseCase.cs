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

        public string GetShotCalcStr()
        {
            return $"-{ScoreConfig.SHOT_BONUS_RATE} × {_shotCountEntity.value}";
        }

        public string GetShotBonusStr()
        {
            return $"-{_shotCountEntity.GetScore()}";
        }

        public string GetBackCalcStr()
        {
            return $"+{ScoreConfig.BACK_BONUS_RATE} × {_backCountEntity.value}";
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
                - (ScoreConfig.SHOT_BONUS_RATE * _shotCountEntity.value) // shotのみ減算
                + (ScoreConfig.BACK_BONUS_RATE * _backCountEntity.value)
                + ScoreConfig.PLAY_BONUS;
        }
    }
}
namespace Re.InGame
{
    public sealed class ProjectConfig
    {
        public const string GAME_ID = "re_golf";
    }
    
    public sealed class StageConfig
    {
        public const int STAGE_COUNT = 10;
    }

    public sealed class UiConfig
    {
        public const float ANIMATION_TIME = 0.5f;
        public const float POPUP_TIME = 0.25f;
    }

    public sealed class PlayerConfig
    {
        public const float DISSOLVE_TIME = 0.5f;
    }

    public sealed class ScoreConfig
    {
        public const int CLEAR_BONUS = 10000;
        public const int SHOT_BONUS = 50000;
        public const int BACK_BONUS = 40000;
        public const int PLAY_BONUS = 1000000;

        public const int SHOT_BONUS_RATE = 1000;
        public const int BACK_BONUS_RATE = 800;
    }
}
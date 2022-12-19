using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;
using UniRx;

namespace Re.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseState
    {
        private readonly OutGame.Domain.UseCase.SceneUseCase _sceneUseCase;
        private readonly OutGame.Domain.UseCase.SoundUseCase _soundUseCase;
        private readonly ScoreUseCase _scoreUseCase;
        private readonly PlayerView _playerView;
        private readonly ResultView _resultView;
        private readonly ScoreView _scoreView;

        public ResultState(OutGame.Domain.UseCase.SceneUseCase sceneUseCase, ScoreUseCase scoreUseCase,
            OutGame.Domain.UseCase.SoundUseCase soundUseCase, PlayerView playerView, ResultView resultView,
            ScoreView scoreView)
        {
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _scoreUseCase = scoreUseCase;
            _playerView = playerView;
            _resultView = resultView;
            _scoreView = scoreView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _resultView.InitAsync(token).Forget();
            _scoreView.Init(_soundUseCase.SetUpPlaySe);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _playerView.Activate(false);
            await _resultView.ShowAsync(UiConfig.POPUP_TIME, token);

            // ランキングシーンのload
            var score = _scoreUseCase.GetTotalScore();

            // Tweet
            {
                var message = $"スコア: {score}\n";
                message += $"#{ProjectConfig.GAME_ID} #unity1week\n";
                _resultView.pushTweet
                    .Subscribe(_ => { UnityRoomTweet.Tweet(ProjectConfig.GAME_ID, message); })
                    .AddTo(_resultView);
            }

            RankingLoader.Instance.SendScoreAndShowRanking(score);

            // 結果表示演出
            await _scoreView.ShowClearBonusAsync(_scoreUseCase.GetClearBonusStr(), token);
            await _scoreView.ShowShotBonusAsync(_scoreUseCase.GetShotBonusStr(), token);
            await _scoreView.ShowBackBonusAsync(_scoreUseCase.GetBackBonusStr(), token);
            await _scoreView.ShowPlayBonusAsync(_scoreUseCase.GetPlayBonusStr(), token);
            await _scoreView.TweenScoreAsync(score, token);
            await _resultView.ShowCloseButtonAsync(UiConfig.ANIMATION_TIME, token);

            await _resultView.closeResult.ToUniTask(true, token);

            // ランキングシーンのunload
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ranking");

            // タイトルに戻る
            _sceneUseCase.SetUpLoad(OutGame.SceneName.Main);

            return GameState.None;
        }
    }
}
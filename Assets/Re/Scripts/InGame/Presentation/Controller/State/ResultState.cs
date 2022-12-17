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
        private readonly ScoreUseCase _scoreUseCase;
        private readonly PlayerView _playerView;
        private readonly ResultView _resultView;

        public ResultState(OutGame.Domain.UseCase.SceneUseCase sceneUseCase, ScoreUseCase scoreUseCase,
            PlayerView playerView, ResultView resultView)
        {
            _sceneUseCase = sceneUseCase;
            _scoreUseCase = scoreUseCase;
            _playerView = playerView;
            _resultView = resultView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _resultView.HideAsync(0.0f, token).Forget();

            _resultView.pushTweet
                .Subscribe(_ =>
                {
                    // TODO: Tweet
                })
                .AddTo(_resultView);

            // _resultView.pushRanking
            //     .Subscribe(_ =>
            //     {
            //         // TODO: Ranking
            //     })
            //     .AddTo(_resultView);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _playerView.Activate(false);
            await _resultView.ShowAsync(UiConfig.POPUP_TIME, token);

            // ランキングシーンのload
            var score = _scoreUseCase.GetTotalScore();
            // RankingLoader.Instance.SendScoreAndShowRanking(score);

            // 結果表示演出
            await _resultView.ShowClearBonusAsync(_scoreUseCase.GetClearBonusStr(), token);
            await _resultView.ShowShotBonusAsync(_scoreUseCase.GetShotCalcStr(), _scoreUseCase.GetShotBonusStr(), token);
            await _resultView.ShowBackBonusAsync(_scoreUseCase.GetBackCalcStr(), _scoreUseCase.GetBackBonusStr(), token);
            await _resultView.ShowPlayBonusAsync(_scoreUseCase.GetPlayBonusStr(), token);
            await _resultView.TweenScoreAsync(score, token);

            await _resultView.closeResult.ToUniTask(true, token);

            // ランキングシーンのunload
            // await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ranking");

            // タイトルに戻る
            _sceneUseCase.SetUpLoad(OutGame.SceneName.Main);

            return GameState.None;
        }
    }
}
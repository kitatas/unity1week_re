using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;
using UniRx;

namespace Re.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly PlayerView _playerView;
        private readonly TitleView _titleView;
        private readonly ConfigView _configView;
        private readonly MainView _mainView;

        public TitleState(PlayerView playerView, TitleView titleView, ConfigView configView,
            MainView mainView)
        {
            _playerView = playerView;
            _titleView = titleView;
            _configView = configView;
            _mainView = mainView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.ShowAsync(0.0f, token).Forget();
            _configView.HideAsync(0.0f, token).Forget();
            _mainView.HideAsync(0.0f, token).Forget();

            _configView.pushConfig
                .Subscribe(_ =>
                {
                    _configView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
                    _titleView.HideAsync(UiConfig.POPUP_TIME, token).Forget();
                    _playerView.Activate(false);
                })
                .AddTo(_configView);

            _configView.closeConfig
                .Subscribe(_ =>
                {
                    _configView.HideAsync(UiConfig.POPUP_TIME, token).Forget();
                    _titleView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
                    _playerView.Activate(true);
                })
                .AddTo(_configView);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var animationTime = 0.25f;
            await _titleView.StartGameAsync(animationTime, token);

            await _playerView.SetUpAsync(animationTime, token);

            await _mainView.ShowAsync(animationTime, token);

            return GameState.SetUp;
        }
    }
}
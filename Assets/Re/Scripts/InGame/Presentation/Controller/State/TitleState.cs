using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;
using UniRx;

namespace Re.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly OutGame.Domain.UseCase.SoundUseCase _soundUseCase;
        private readonly PlayerView _playerView;
        private readonly TitleView _titleView;
        private readonly ConfigView _configView;
        private readonly LicenseView _licenseView;
        private readonly MainView _mainView;

        public TitleState(OutGame.Domain.UseCase.SoundUseCase soundUseCase,
            PlayerView playerView, TitleView titleView, ConfigView configView, LicenseView licenseView,
            MainView mainView)
        {
            _soundUseCase = soundUseCase;
            _playerView = playerView;
            _titleView = titleView;
            _configView = configView;
            _licenseView = licenseView;
            _mainView = mainView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _soundUseCase.SetUpPlayBgm(OutGame.BgmType.Main);

            _titleView.ShowAsync(0.0f, token).Forget();
            _configView.HideAsync(0.0f, token).Forget();
            _licenseView.HideAsync(0.0f, token).Forget();
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

            _licenseView.pushLicense
                .Subscribe(_ =>
                {
                    _licenseView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
                    _titleView.HideAsync(UiConfig.POPUP_TIME, token).Forget();
                    _playerView.Activate(false);
                })
                .AddTo(_licenseView);

            _licenseView.closeLicense
                .Subscribe(_ =>
                {
                    _licenseView.HideAsync(UiConfig.POPUP_TIME, token).Forget();
                    _titleView.ShowAsync(UiConfig.POPUP_TIME, token).Forget();
                    _playerView.Activate(true);
                })
                .AddTo(_licenseView);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _titleView.StartGameAsync(UiConfig.POPUP_TIME, token);

            await _playerView.SetUpAsync(UiConfig.POPUP_TIME, token);

            await _mainView.ShowAsync(UiConfig.POPUP_TIME, token);

            return GameState.SetUp;
        }
    }
}
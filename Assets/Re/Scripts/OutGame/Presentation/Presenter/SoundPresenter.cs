using Re.OutGame.Domain.UseCase;
using Re.OutGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Re.OutGame.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly SoundView _soundView;

        public SoundPresenter(SoundUseCase soundUseCase, SoundView soundView)
        {
            _soundUseCase = soundUseCase;
            _soundView = soundView;
        }

        public void Initialize()
        {
            _soundUseCase.PlayBgm()
                .Subscribe(_soundView.PlayBgm)
                .AddTo(_soundView);

            _soundUseCase.PlaySe()
                .Subscribe(_soundView.PlaySe)
                .AddTo(_soundView);

            _soundUseCase.UpdateBgmVolume()
                .Subscribe(_soundView.SetBgmVolume)
                .AddTo(_soundView);

            _soundUseCase.UpdateSeVolume()
                .Subscribe(_soundView.SetSeVolume)
                .AddTo(_soundView);
        }
    }
}
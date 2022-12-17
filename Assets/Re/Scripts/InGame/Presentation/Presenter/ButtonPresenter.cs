using Re.Common.Presentation.View;
using Re.InGame.Presentation.View;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable
    {
        private readonly OutGame.Domain.UseCase.SceneUseCase _sceneUseCase;
        private readonly OutGame.Domain.UseCase.SoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;

        public ButtonPresenter(OutGame.Domain.UseCase.SceneUseCase sceneUseCase,
            OutGame.Domain.UseCase.SoundUseCase soundUseCase, VolumeView volumeView)
        {
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init();

                if (buttonView is LoadButtonView loadButtonView)
                {
                    loadButtonView.Push()
                        .Subscribe(_sceneUseCase.SetUpLoad)
                        .AddTo(loadButtonView);
                }
            }

            _volumeView.Init(_soundUseCase.bgmVolume, _soundUseCase.seVolume);

            _volumeView.UpdateBgmVolume()
                .Subscribe(_soundUseCase.SetBgmVolume)
                .AddTo(_volumeView);

            _volumeView.UpdateSeVolume()
                .Subscribe(_soundUseCase.SetSeVolume)
                .AddTo(_volumeView);

            _volumeView.OnPointerUpBgmSlider()
                .Subscribe(_soundUseCase.SetUpPlaySe)
                .AddTo(_volumeView);

            _volumeView.OnPointerUpSeSlider()
                .Subscribe(_soundUseCase.SetUpPlaySe)
                .AddTo(_volumeView);
        }
    }
}
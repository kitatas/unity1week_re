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

        public ButtonPresenter(OutGame.Domain.UseCase.SceneUseCase sceneUseCase)
        {
            _sceneUseCase = sceneUseCase;
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
        }
    }
}
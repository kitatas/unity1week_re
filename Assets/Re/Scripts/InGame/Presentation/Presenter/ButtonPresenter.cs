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
            foreach (var loadButtonView in Object.FindObjectsOfType<LoadButtonView>())
            {
                Debug.Log($"init load button: {loadButtonView.gameObject.name}");
                
                loadButtonView.Push()
                    .Subscribe(_sceneUseCase.SetUpLoad)
                    .AddTo(loadButtonView);
            }
        }
    }
}
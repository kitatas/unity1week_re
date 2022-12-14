using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.OutGame.Domain.UseCase;
using Re.OutGame.Presentation.View;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Re.OutGame.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;

        public ScenePresenter(SceneUseCase sceneUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
            _transitionView = transitionView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _transitionView.FadeOutAsync(0.0f, _tokenSource.Token).Forget();

            _sceneUseCase.Load()
                .Subscribe(x =>
                {
                    // シーン遷移
                    FadeLoadAsync(x, _tokenSource.Token).Forget();
                })
                .AddTo(_transitionView);
        }

        private async UniTaskVoid FadeLoadAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionView.FadeInAsync(SceneConfig.FADE_TIME, token);
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
            await _transitionView.FadeOutAsync(SceneConfig.FADE_TIME, token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}
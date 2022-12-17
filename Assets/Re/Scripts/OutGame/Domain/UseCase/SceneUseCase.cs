using System;
using UniRx;

namespace Re.OutGame.Domain.UseCase
{
    public sealed class SceneUseCase
    {
        private readonly Subject<SceneName> _load;

        public SceneUseCase()
        {
            _load = new Subject<SceneName>();
        }

        public IObservable<SceneName> Load() => _load.Where(x => x != SceneName.None);

        public void SetUpLoad(SceneName sceneName)
        {
            _load?.OnNext(sceneName);
        }
    }
}
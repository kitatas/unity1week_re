using Re.OutGame.Data.DataStore;
using Re.OutGame.Domain.Repository;
using Re.OutGame.Domain.UseCase;
using Re.OutGame.Presentation.Presenter;
using Re.OutGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Re.OutGame.Installer
{
    public sealed class Installer : LifetimeScope
    {
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Repository
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);
            builder.Register<SoundUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();
            builder.RegisterEntryPoint<SoundPresenter>();

            // View
            builder.RegisterInstance<TransitionView>(FindObjectOfType<TransitionView>());
            builder.RegisterInstance<SoundView>(FindObjectOfType<SoundView>());
        }
    }
}
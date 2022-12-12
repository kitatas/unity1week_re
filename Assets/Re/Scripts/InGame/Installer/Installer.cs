using Re.InGame.Data.Entity;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.Controller;
using Re.InGame.Presentation.Presenter;
using VContainer;
using VContainer.Unity;

namespace Re.InGame.Installer
{
    public sealed class Installer : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<StateEntity>(Lifetime.Scoped);

            // UseCase
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();
        }
    }
}
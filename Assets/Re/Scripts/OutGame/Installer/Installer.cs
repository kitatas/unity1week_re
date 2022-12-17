using Re.OutGame.Domain.UseCase;
using Re.OutGame.Presentation.Presenter;
using Re.OutGame.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace Re.OutGame.Installer
{
    public sealed class Installer : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();

            // View
            builder.RegisterInstance<TransitionView>(FindObjectOfType<TransitionView>());
        }
    }
}
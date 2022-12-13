using Re.InGame.Data.Entity;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.Controller;
using Re.InGame.Presentation.Presenter;
using Re.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Re.InGame.Installer
{
    public sealed class Installer : LifetimeScope
    {
        [SerializeField] private ClearView clearView = default;
        [SerializeField] private DragHandleView dragHandleView = default;
        [SerializeField] private GoalView goalView = default;
        [SerializeField] private PlayerView playerView = default;
        [SerializeField] private ShotCountView shotCountView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<ShotCountEntity>(Lifetime.Scoped);
            builder.Register<StateEntity>(Lifetime.Scoped);
            builder.Register<StopPointsEntity>(Lifetime.Scoped);

            // UseCase
            builder.Register<ShotCountUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<StopPointUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<BackState>(Lifetime.Scoped);
            builder.Register<GoalState>(Lifetime.Scoped);
            builder.Register<InputState>(Lifetime.Scoped);
            builder.Register<JudgeState>(Lifetime.Scoped);
            builder.Register<SetUpState>(Lifetime.Scoped);
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<ShotCountPresenter>();
            builder.RegisterEntryPoint<StatePresenter>();

            // View
            builder.RegisterInstance<ClearView>(clearView);
            builder.RegisterInstance<DragHandleView>(dragHandleView);
            builder.RegisterInstance<GoalView>(goalView);
            builder.RegisterInstance<PlayerView>(playerView);
            builder.RegisterInstance<ShotCountView>(shotCountView);
        }
    }
}
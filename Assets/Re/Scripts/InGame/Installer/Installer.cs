using Re.InGame.Data.DataStore;
using Re.InGame.Data.Entity;
using Re.InGame.Domain.Repository;
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
        [SerializeField] private StageTable stageTable = default;

        [SerializeField] private BackCountView backCountView = default;
        [SerializeField] private ClearView clearView = default;
        [SerializeField] private ConfigView configView = default;
        [SerializeField] private DragHandleView dragHandleView = default;
        [SerializeField] private MainView mainView = default;
        [SerializeField] private PlayerView playerView = default;
        [SerializeField] private PointStackView pointStackView = default;
        [SerializeField] private ShotCountView shotCountView = default;
        [SerializeField] private StageLevelView stageLevelView = default;
        [SerializeField] private TitleView titleView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<StageTable>(stageTable);

            // Entity
            builder.Register<BackCountEntity>(Lifetime.Scoped);
            builder.Register<ShotCountEntity>(Lifetime.Scoped);
            builder.Register<StageLevelEntity>(Lifetime.Scoped);
            builder.Register<StateEntity>(Lifetime.Scoped);
            builder.Register<StopPointsEntity>(Lifetime.Scoped);

            // Repository
            builder.Register<StageRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BackCountUseCase>(Lifetime.Scoped);
            builder.Register<ShotCountUseCase>(Lifetime.Scoped);
            builder.Register<StageUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<StopPointUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<BackState>(Lifetime.Scoped);
            builder.Register<GoalState>(Lifetime.Scoped);
            builder.Register<InputState>(Lifetime.Scoped);
            builder.Register<JudgeState>(Lifetime.Scoped);
            builder.Register<SetUpState>(Lifetime.Scoped);
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<BackCountPresenter>();
            builder.RegisterEntryPoint<ButtonPresenter>();
            builder.RegisterEntryPoint<PointStackPresenter>();
            builder.RegisterEntryPoint<ShotCountPresenter>();
            builder.RegisterEntryPoint<StageLevelPresenter>();
            builder.RegisterEntryPoint<StatePresenter>();

            // View
            builder.RegisterInstance<BackCountView>(backCountView);
            builder.RegisterInstance<ClearView>(clearView);
            builder.RegisterInstance<ConfigView>(configView);
            builder.RegisterInstance<DragHandleView>(dragHandleView);
            builder.RegisterInstance<MainView>(mainView);
            builder.RegisterInstance<PlayerView>(playerView);
            builder.RegisterInstance<PointStackView>(pointStackView);
            builder.RegisterInstance<ShotCountView>(shotCountView);
            builder.RegisterInstance<StageLevelView>(stageLevelView);
            builder.RegisterInstance<TitleView>(titleView);
        }
    }
}
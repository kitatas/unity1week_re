using Re.Common.Presentation.Presenter;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class StageLevelPresenter : BasePresenter<int>
    {
        public StageLevelPresenter(StageUseCase stageUseCase, StageLevelView stageCountView)
            : base(stageUseCase, stageCountView)
        {
        }
    }
}
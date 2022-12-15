using Re.Common.Presentation.Presenter;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class PointStackPresenter : BasePresenter<int>
    {
        public PointStackPresenter(StopPointUseCase stopPointUseCase, PointStackView pointStackView)
            : base(stopPointUseCase, pointStackView)
        {
        }
    }
}
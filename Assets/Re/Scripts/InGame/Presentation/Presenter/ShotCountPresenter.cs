using Re.Common.Presentation.Presenter;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class ShotCountPresenter : BasePresenter<int>
    {
        public ShotCountPresenter(ShotCountUseCase shotCountUseCase, ShotCountView shotCountView)
            : base(shotCountUseCase, shotCountView)
        {
        }
    }
}
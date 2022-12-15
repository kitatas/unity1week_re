using Re.Common.Presentation.Presenter;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Presenter
{
    public sealed class BackCountPresenter : BasePresenter<int>
    {
        public BackCountPresenter(BackCountUseCase backCountUseCase, BackCountView backCountView)
            : base(backCountUseCase, backCountView)
        {
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly TitleView _titleView;
        private readonly MainView _mainView;

        public TitleState(TitleView titleView, MainView mainView)
        {
            _titleView = titleView;
            _mainView = mainView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.ShowAsync(0.0f, token).Forget();
            _mainView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var animationTime = 0.25f;
            await _titleView.StartGameAsync(animationTime, token);

            await _mainView.ShowAsync(animationTime, token);

            return GameState.SetUp;
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;
using Input = UnityEngine.Input;

namespace Re.InGame.Presentation.Controller
{
    public sealed class InputState : BaseState
    {
        private readonly DragHandleView _dragHandleView;
        private readonly PlayerView _playerView;

        public InputState(DragHandleView dragHandleView, PlayerView playerView)
        {
            _dragHandleView = dragHandleView;
            _playerView = playerView;
        }

        public override GameState state => GameState.Input;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _dragHandleView.Init();
            _playerView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // ドラッグ距離が一定値を越した場合
                    var dragDiff = await _dragHandleView.SetUpAsync(_playerView.position, token);
                    if (dragDiff.magnitude * 0.01f > 1.0f)
                    {
                        _playerView.Shot(dragDiff);

                        // 停止待ち
                        await UniTask.WaitUntil(_playerView.IsStop, cancellationToken: token);
                        
                        // TODO: to judge
                        return GameState.SetUp;
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    // TODO: undo的な
                }

                await UniTask.Yield(token);
            }
        }
    }
}
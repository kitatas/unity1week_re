using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Domain.UseCase;
using Re.InGame.Presentation.View;
using Input = UnityEngine.Input;

namespace Re.InGame.Presentation.Controller
{
    public sealed class InputState : BaseState
    {
        private readonly BackCountUseCase _backCountUseCase;
        private readonly ShotCountUseCase _shotCountUseCase;
        private readonly StopPointUseCase _stopPointUseCase;
        private readonly DragHandleView _dragHandleView;
        private readonly PlayerView _playerView;

        public InputState(BackCountUseCase backCountUseCase, ShotCountUseCase shotCountUseCase, StopPointUseCase stopPointUseCase,
            DragHandleView dragHandleView, PlayerView playerView)
        {
            _backCountUseCase = backCountUseCase;
            _shotCountUseCase = shotCountUseCase;
            _stopPointUseCase = stopPointUseCase;
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
                        // 移動直前の位置を保持しておく
                        _stopPointUseCase.Push(_playerView.position, _playerView.rotation);

                        _playerView.Shot(dragDiff);
                        _shotCountUseCase.Increase();

                        return GameState.Judge;
                    }
                }
                else if (Input.GetMouseButtonDown(1) && _stopPointUseCase.IsStack())
                {
                    await _playerView.DissolveAsync(token);
                    _backCountUseCase.Increase();
                    return GameState.Back;
                }

                await UniTask.Yield(token);
            }
        }
    }
}
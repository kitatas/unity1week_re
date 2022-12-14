using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseState
    {
        private readonly PlayerView _playerView;

        public JudgeState(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public override GameState state => GameState.Judge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // 停止待ち
            await UniTask.WaitUntil(_playerView.IsStop, cancellationToken: token);

            // 当たり判定確認待ち
            await UniTask.DelayFrame(2, cancellationToken: token);

            if (_playerView.isGoal)
            {
                return GameState.Goal;
            }
            else
            {
                return GameState.Input;
            }
        }
    }
}
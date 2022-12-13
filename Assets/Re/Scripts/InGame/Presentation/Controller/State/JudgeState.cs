using System.Threading;
using Cysharp.Threading.Tasks;
using Re.InGame.Presentation.View;

namespace Re.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseState
    {
        private readonly GoalView _goalView;
        private readonly PlayerView _playerView;

        public JudgeState(GoalView goalView, PlayerView playerView)
        {
            _goalView = goalView;
            _playerView = playerView;
        }

        public override GameState state => GameState.Judge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _goalView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // 停止待ち
            await UniTask.WaitUntil(_playerView.IsStop, cancellationToken: token);

            // 1フレ待ち
            await UniTask.DelayFrame(1, cancellationToken: token);

            if (_goalView.isGoal)
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
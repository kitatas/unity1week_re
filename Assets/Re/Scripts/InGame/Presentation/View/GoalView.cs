using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class GoalView : MonoBehaviour
    {
        public bool isGoal { get; private set; }

        public void Init()
        {
            isGoal = false;

            this.OnTriggerEnter2DAsObservable()
                .Where(x => x.TryGetComponent(out PlayerView playerView))
                .Subscribe(_ => isGoal = true)
                .AddTo(this);

            this.OnTriggerExit2DAsObservable()
                .Where(x => x.TryGetComponent(out PlayerView playerView))
                .Subscribe(x => isGoal = false)
                .AddTo(this);
        }
    }
}
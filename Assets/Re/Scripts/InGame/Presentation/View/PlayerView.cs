using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private readonly float _shotPowerRate = 0.05f;

        public bool isGoal { get; private set; }

        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            isGoal = false;

            this.OnTriggerEnter2DAsObservable()
                .Merge(this.OnTriggerStay2DAsObservable())
                .Where(x => x.TryGetComponent(out GoalView goalView))
                .Subscribe(_ => isGoal = true)
                .AddTo(this);

            this.OnTriggerExit2DAsObservable()
                .Where(x => x.TryGetComponent(out GoalView goalView))
                .Subscribe(x => isGoal = false)
                .AddTo(this);
        }

        public Vector2 position => transform.position;

        public Vector3 rotation => transform.rotation.eulerAngles;

        public void SetPoint(Data.Entity.PointEntity entity)
        {
            transform.position = entity.position;
            transform.rotation = Quaternion.Euler(entity.rotation);
        }

        public void Shot(Vector2 direction)
        {
            
            _rigidbody.velocity = direction * _shotPowerRate;
        }

        public bool IsStop()
        {
            if (_rigidbody.velocity.magnitude <= 0.1f)
            {
                _rigidbody.velocity = Vector2.zero;
                return true;
            }

            return false;
        }
    }
}
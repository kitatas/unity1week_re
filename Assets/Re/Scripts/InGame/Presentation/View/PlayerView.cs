using UnityEngine;

namespace Re.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerView : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private readonly float _shotPowerRate = 0.05f;

        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Vector2 position => transform.position;

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
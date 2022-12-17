using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SpriteGlow;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private Material dissolve = default;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private SpriteGlowEffect _spriteGlowEffect;
        private TrailRenderer _trailRenderer;

        private readonly float _shotPowerRate = 0.05f;
        private static readonly int _threshold = Shader.PropertyToID("_Threshold");

        public bool isGoal { get; private set; }

        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteGlowEffect = GetComponent<SpriteGlowEffect>();
            _trailRenderer = GetComponent<TrailRenderer>();

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

        public async UniTask SetUpAsync(float animationTime, CancellationToken token)
        {
            await transform
                .DOScale(Vector3.one, animationTime)
                .SetEase(Ease.InQuart)
                .SetLink(gameObject)
                .WithCancellation(token);
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

        public async UniTask DissolveAsync(float animationTime, CancellationToken token)
        {
            _trailRenderer.enabled = false;
            var dissolveTime = animationTime / 2.0f;

            await DOTween.To(
                    () => _spriteGlowEffect.OutlineWidth,
                    x => _spriteGlowEffect.OutlineWidth = x,
                    0,
                    dissolveTime)
                .SetLink(gameObject)
                .WithCancellation(token);
            _spriteGlowEffect.enabled = false;

            _spriteRenderer.material = dissolve;
            await DOTween.To(
                    () => _spriteRenderer.material.GetFloat(_threshold),
                    x => _spriteRenderer.material.SetFloat(_threshold, x),
                    1.0f,
                    dissolveTime)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask AppearAsync(float animationTime, CancellationToken token)
        {
            var dissolveTime = animationTime / 2.0f;

            await DOTween.To(
                    () => _spriteRenderer.material.GetFloat(_threshold),
                    x => _spriteRenderer.material.SetFloat(_threshold, x),
                    0.0f,
                    dissolveTime)
                .SetLink(gameObject)
                .WithCancellation(token);

            _spriteGlowEffect.enabled = true;
            await DOTween.To(
                    () => _spriteGlowEffect.OutlineWidth,
                    x => _spriteGlowEffect.OutlineWidth = x,
                    10,
                    dissolveTime)
                .SetLink(gameObject)
                .WithCancellation(token);

            _trailRenderer.enabled = true;
        }

        public void Activate(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
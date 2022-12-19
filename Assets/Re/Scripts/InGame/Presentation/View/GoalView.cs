using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SpriteGlow;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class GoalView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteGlowEffect _spriteGlowEffect;

        private readonly float _dissolveTime = 0.5f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteGlowEffect = GetComponent<SpriteGlowEffect>();
        }

        public async UniTask AppearAsync(CancellationToken token)
        {
            await _spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetLink(gameObject)
                .WithCancellation(token);

            await (
                _spriteRenderer
                    .DOFade(1.0f, _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token),
                DOTween.To(
                        () => _spriteGlowEffect.GlowBrightness,
                        x => _spriteGlowEffect.GlowBrightness = x,
                        5.0f,
                        _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token),
                DOTween.To(
                        () => _spriteGlowEffect.OutlineWidth,
                        x => _spriteGlowEffect.OutlineWidth = x,
                        10,
                        _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token)
            );
        }

        public async UniTask DisappearAsync(CancellationToken token)
        {
            await (
                transform
                    .DOScale(Vector3.one * 3.0f, _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token),
                _spriteRenderer
                    .DOFade(0.0f, _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token),
                DOTween.To(
                        () => _spriteGlowEffect.GlowBrightness,
                        x => _spriteGlowEffect.GlowBrightness = x,
                        0.0f,
                        _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token),
                DOTween.To(
                        () => _spriteGlowEffect.OutlineWidth,
                        x => _spriteGlowEffect.OutlineWidth = x,
                        0,
                        _dissolveTime)
                    .SetLink(gameObject)
                    .WithCancellation(token)
            );
        }
    }
}
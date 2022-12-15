using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class StageObjectView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        public async UniTask AppearAsync(float animationTime, CancellationToken token)
        {
            await spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetLink(gameObject)
                .WithCancellation(token);

            await (
                spriteRenderer
                    .DOFade(1.0f, animationTime)
                    .SetLink(gameObject)
                    .WithCancellation(token)
            );
        }

        public async UniTask DisappearAsync(float animationTime, CancellationToken token)
        {
            await (
                spriteRenderer
                    .DOFade(0.0f, animationTime)
                    .SetLink(gameObject)
                    .WithCancellation(token)
            );
        }
    }
}
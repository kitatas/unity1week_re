using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class ClearView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clearText = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            var animator = new DOTweenTMPAnimator(clearText);
            for (int i = 0; i < animator.textInfo.characterCount; i++)
            {
                tasks.Add(DOTween.Sequence()
                    .Append(animator
                        .DOFadeChar(i, 0.0f, 0.0f))
                    .Join(animator
                        .DOOffsetChar(i, Vector3.right * 50f, 0.0f))
                    .Append(animator
                        .DOFadeChar(i, 1.0f, animationTime)
                        .SetDelay(i * 0.05f))
                    .Join(animator
                        .DOOffsetChar(i, Vector3.zero, animationTime)
                        .SetDelay(i * 0.05f))
                    .AppendInterval(0.5f)
                    .Append(animator
                        .DOFadeChar(i, 0.0f, animationTime)
                        .SetDelay(i * 0.05f))
                    .Join(animator
                        .DOOffsetChar(i, Vector3.left * 50f, animationTime)
                        .SetDelay(i * 0.05f))
                    .SetLink(gameObject)
                    .WithCancellation(token)
                );
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EFUK;
using UnityEngine;

namespace Re.Common.Presentation.View
{
    public sealed class BaseCanvasGroupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            canvasGroup.blocksRaycasts = true;

            await DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ConvertRectTransform()
                    .DOScale(Vector3.one, animationTime)
                    .SetEase(Ease.OutBack));
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.OutQuart))
                .Join(canvasGroup.transform.ConvertRectTransform()
                    .DOScale(Vector3.one * 0.8f, animationTime)
                    .SetEase(Ease.OutQuart));

            canvasGroup.blocksRaycasts = false;
        }
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Re.Common.Presentation.View;
using TMPro;
using UniRx;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class ResultView : MonoBehaviour
    {
        [SerializeField] private BaseButtonView tweetButton = default;
        [SerializeField] private BaseButtonView closeButton = default;
        [SerializeField] private BaseCanvasGroupView resultCanvas = default;

        public async UniTask InitAsync(CancellationToken token)
        {
            await (
                HideAsync(0.0f, token),
                HideCloseButtonAsync(0.0f, token)
            );
        }

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await resultCanvas.ShowAsync(animationTime, token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await resultCanvas.HideAsync(animationTime, token);
        }

        public IObservable<Unit> pushTweet => tweetButton.push;
        public IObservable<Unit> closeResult => closeButton.push;

        public async UniTask ShowCloseButtonAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(closeButton.image
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(closeButton.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);

            closeButton.Activate(true);
        }

        public async UniTask HideCloseButtonAsync(float animationTime, CancellationToken token)
        {
            closeButton.Activate(false);

            await DOTween.Sequence()
                .Append(closeButton.image
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(closeButton.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);
        }
    }
}
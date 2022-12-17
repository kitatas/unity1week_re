using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class LicenseView : MonoBehaviour
    {
        [SerializeField] private BaseButtonView licenseButton = default;
        [SerializeField] private BaseButtonView closeButton = default;
        [SerializeField] private BaseCanvasGroupView licenseCanvas = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await licenseCanvas.ShowAsync(animationTime, token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await licenseCanvas.HideAsync(animationTime, token);
        }

        public IObservable<Unit> pushLicense => licenseButton.push;
        public IObservable<Unit> closeLicense => closeButton.push;
    }
}
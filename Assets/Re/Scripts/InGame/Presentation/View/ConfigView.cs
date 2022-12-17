using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class ConfigView : MonoBehaviour
    {
        [SerializeField] private BaseButtonView configButton = default;
        [SerializeField] private BaseButtonView closeButton = default;
        [SerializeField] private BaseCanvasGroupView configCanvas = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await configCanvas.ShowAsync(animationTime, token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await configCanvas.HideAsync(animationTime, token);
        }

        public IObservable<Unit> pushConfig => configButton.push;
        public IObservable<Unit> closeConfig => closeButton.push;
    }
}
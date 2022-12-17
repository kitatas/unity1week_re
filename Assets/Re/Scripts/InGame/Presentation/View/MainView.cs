using System.Threading;
using Cysharp.Threading.Tasks;
using Re.Common.Presentation.View;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class MainView : MonoBehaviour
    {
        [SerializeField] private BaseCanvasGroupView mainCanvas = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await mainCanvas.ShowAsync(animationTime, token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await mainCanvas.HideAsync(animationTime, token);
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;
using Re.Common.Presentation.View;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private BaseButtonView startButton = default;
        [SerializeField] private BaseCanvasGroupView titleCanvas = default;

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await titleCanvas.ShowAsync(animationTime, token);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await titleCanvas.HideAsync(animationTime, token);
        }

        public async UniTask StartGameAsync(float animationTime, CancellationToken token)
        {
            await startButton.push.ToUniTask(true, token);

            await HideAsync(animationTime, token);
        }
    }
}
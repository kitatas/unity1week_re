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

        // TODO: 表示演出
        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await clearText
                .DOFade(1.0f, animationTime)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        // TODO: 非表示演出
        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            await clearText
                .DOFade(0.0f, animationTime)
                .SetLink(gameObject)
                .WithCancellation(token);
        }
    }
}
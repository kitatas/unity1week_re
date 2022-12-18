using System;
using DG.Tweening;
using EFUK;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Re.Common.Presentation.View
{
    public class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private Button button = default;

        private readonly float _animationTime = 0.1f;

        public void Init(Action<OutGame.SeType> playSe)
        {
            var rectTransform = transform.ConvertRectTransform();
            var scale = rectTransform.localScale;

            push.Subscribe(_ =>
                {
                    // 押下時のアニメーション
                    DOTween.Sequence()
                        .Append(rectTransform
                            .DOScale(scale * 0.8f, _animationTime))
                        .Append(rectTransform
                            .DOScale(scale, _animationTime))
                        .SetLink(gameObject);

                    playSe?.Invoke(OutGame.SeType.Button);
                })
                .AddTo(this);
        }

        public IObservable<Unit> push => button.OnClickAsObservable();

        public Image image => button.image;

        public void Activate(bool value)
        {
            button.enabled = value;
        }
    }
}
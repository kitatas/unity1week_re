using System;
using System.Collections.Generic;
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

        [SerializeField] private TextMeshProUGUI clearBonusText = default;
        [SerializeField] private TextMeshProUGUI clearBonusValueText = default;
        [SerializeField] private TextMeshProUGUI shotBonusText = default;
        [SerializeField] private TextMeshProUGUI shotBonusCalcText = default;
        [SerializeField] private TextMeshProUGUI shotBonusValueText = default;
        [SerializeField] private TextMeshProUGUI backBonusText = default;
        [SerializeField] private TextMeshProUGUI backBonusCalcText = default;
        [SerializeField] private TextMeshProUGUI backBonusValueText = default;
        [SerializeField] private TextMeshProUGUI playBonusText = default;
        [SerializeField] private TextMeshProUGUI playBonusValueText = default;
        [SerializeField] private TextMeshProUGUI scoreText = default;

        private Action<OutGame.SeType> _playSe;

        public async UniTask InitAsync(Action<OutGame.SeType> playSe, CancellationToken token)
        {
            _playSe = playSe;

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

        public async UniTask ShowClearBonusAsync(string clearBonus, CancellationToken token)
        {
            await ShowBonusTextAsync(clearBonusText, token);

            clearBonusValueText.text = $"{clearBonus}";
            await TweenBonusScoreAsync(clearBonusValueText, token);
        }

        public async UniTask ShowShotBonusAsync(string shotCalc, string shotBonus, CancellationToken token)
        {
            shotBonusCalcText.text = $"{shotCalc}";
            await (
                ShowBonusTextAsync(shotBonusText, token),
                ShowBonusTextAsync(shotBonusCalcText, token)
            );

            shotBonusValueText.text = $"{shotBonus}";
            await TweenBonusScoreAsync(shotBonusValueText, token);
        }

        public async UniTask ShowBackBonusAsync(string backCalc, string backBonus, CancellationToken token)
        {
            backBonusCalcText.text = $"{backCalc}";
            await (
                ShowBonusTextAsync(backBonusText, token),
                ShowBonusTextAsync(backBonusCalcText, token)
            );

            backBonusValueText.text = $"{backBonus}";
            await TweenBonusScoreAsync(backBonusValueText, token);
        }

        public async UniTask ShowPlayBonusAsync(string playBonus, CancellationToken token)
        {
            await ShowBonusTextAsync(playBonusText, token);

            playBonusValueText.text = $"{playBonus}";
            await TweenBonusScoreAsync(playBonusValueText, token);
        }

        private async UniTask ShowBonusTextAsync(TextMeshProUGUI textMeshProUGUI, CancellationToken token)
        {
            await textMeshProUGUI
                .DOFade(1.0f, UiConfig.ANIMATION_TIME)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        private async UniTask TweenBonusScoreAsync(TextMeshProUGUI textMeshProUGUI, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            var animator = new DOTweenTMPAnimator(textMeshProUGUI);
            for (int i = 0; i < animator.textInfo.characterCount; i++)
            {
                var index = animator.textInfo.characterCount - i - 1;

                tasks.Add(DOTween.Sequence()
                    .Append(animator
                        .DOFadeChar(i, 0.0f, 0.0f))
                    .Join(animator
                        .DOOffsetChar(i, new Vector3(-20.0f, 0.0f), 0.0f))
                    .Append(animator
                        .DOFadeChar(i, 1.0f, UiConfig.ANIMATION_TIME)
                        .SetDelay(index * 0.05f))
                    .Join(animator
                        .DOOffsetChar(i, new Vector3(0.0f, 0.0f), UiConfig.ANIMATION_TIME)
                        .SetDelay(index * 0.05f))
                    .SetLink(gameObject)
                    .WithCancellation(token)
                );
            }

            _playSe?.Invoke(OutGame.SeType.Clear);
            await UniTask.WhenAll(tasks);
            await UniTask.Delay(TimeSpan.FromSeconds(UiConfig.ANIMATION_TIME), cancellationToken: token);
        }

        public async UniTask TweenScoreAsync(int score, CancellationToken token)
        {
            var tweenScore = new ReactiveProperty<int>(0);
            tweenScore
                .Subscribe(x => scoreText.text = $"{x}")
                .AddTo(this);

            _playSe?.Invoke(OutGame.SeType.Score);
            await DOTween.To(
                    () => tweenScore.Value,
                    x => tweenScore.Value = x,
                    score,
                    UiConfig.ANIMATION_TIME * 2
                )
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);

            _playSe?.Invoke(OutGame.SeType.Result);
        }

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
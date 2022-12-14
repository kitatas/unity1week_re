using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clearBonusText = default;
        [SerializeField] private TextMeshProUGUI clearBonusValueText = default;
        [SerializeField] private TextMeshProUGUI shotBonusText = default;
        [SerializeField] private TextMeshProUGUI shotBonusValueText = default;
        [SerializeField] private TextMeshProUGUI backBonusText = default;
        [SerializeField] private TextMeshProUGUI backBonusValueText = default;
        [SerializeField] private TextMeshProUGUI playBonusText = default;
        [SerializeField] private TextMeshProUGUI playBonusValueText = default;
        [SerializeField] private TextMeshProUGUI scoreText = default;

        private Action<OutGame.SeType> _playSe;

        public void Init(Action<OutGame.SeType> playSe)
        {
            _playSe = playSe;
        }

        public async UniTask ShowClearBonusAsync(string clearBonus, CancellationToken token)
        {
            await ShowBonusTextAsync(clearBonusText, token);

            clearBonusValueText.text = $"{clearBonus}";
            await TweenBonusScoreAsync(clearBonusValueText, token);
        }

        public async UniTask ShowShotBonusAsync(string shotBonus, CancellationToken token)
        {
            await ShowBonusTextAsync(shotBonusText, token);

            shotBonusValueText.text = $"{shotBonus}";
            await TweenBonusScoreAsync(shotBonusValueText, token);
        }

        public async UniTask ShowBackBonusAsync(string backBonus, CancellationToken token)
        {
            await ShowBonusTextAsync(backBonusText, token);

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
    }
}
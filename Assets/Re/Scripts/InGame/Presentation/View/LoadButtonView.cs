using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Re.InGame.Presentation.View
{
    public sealed class LoadButtonView : MonoBehaviour
    {
        [SerializeField] private OutGame.SceneName sceneName = default;
        [SerializeField] private Button button = default;

        public IObservable<OutGame.SceneName> Push()
        {
            return button
                .OnClickAsObservable()
                .Select(_ => sceneName);
        }
    }
}
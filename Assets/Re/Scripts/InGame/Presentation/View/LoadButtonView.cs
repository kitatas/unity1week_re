using System;
using Re.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class LoadButtonView : BaseButtonView
    {
        [SerializeField] private OutGame.SceneName sceneName = default;

        public IObservable<OutGame.SceneName> Push()
        {
            return push
                .Select(_ => sceneName);
        }
    }
}
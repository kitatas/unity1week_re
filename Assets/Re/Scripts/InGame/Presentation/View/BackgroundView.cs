using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class BackgroundView : MonoBehaviour
    {
        [SerializeField] private Transform background = default;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    transform.AddLocalEulerAngleZ(Time.deltaTime * 5.0f);
                })
                .AddTo(this);
        }
    }
}
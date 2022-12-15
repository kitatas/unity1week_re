using Re.Common.Presentation.View;
using TMPro;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class BackCountView : BaseView<int>
    {
        [SerializeField] private TextMeshProUGUI countText = default;

        public override void Render(int value)
        {
            countText.text = $"{value:000}";
        }
    }
}
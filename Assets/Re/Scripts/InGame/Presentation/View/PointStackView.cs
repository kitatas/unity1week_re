using Re.Common.Presentation.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Re.InGame.Presentation.View
{
    public sealed class PointStackView : BaseView<int>
    {
        [SerializeField] private Image[] stockImages = default;
        [SerializeField] private TextMeshProUGUI countText = default;

        public override void Render(int value)
        {
            for (int i = 0; i < stockImages.Length; i++)
            {
                stockImages[i].enabled = value > i;
            }

            if (value > stockImages.Length)
            {
                var lack = value - stockImages.Length;
                countText.text = $"+{lack}";
            }
            else
            {
                countText.text = $"";
            }
        }
    }
}
using Re.Common.Presentation.View;
using TMPro;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class StageLevelView : BaseView<int>
    {
        [SerializeField] private TextMeshProUGUI countText = default;

        public override void Render(int value)
        {
            countText.text = $"{value + 1:00} / {StageConfig.STAGE_COUNT:00}";
        }
    }
}
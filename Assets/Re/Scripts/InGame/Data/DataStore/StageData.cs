using UnityEngine;

namespace Re.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(StageData), menuName = "DataTable/" + nameof(StageData))]
    public sealed class StageData : ScriptableObject
    {
        [SerializeField] private DifficultyType difficultyType = default;
        [SerializeField] private Presentation.View.StageView stageView = default;

        public DifficultyType type => difficultyType;
        public Presentation.View.StageView stage => stageView;
    }
}
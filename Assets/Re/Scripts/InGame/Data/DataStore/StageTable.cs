using System.Collections.Generic;
using UnityEngine;

namespace Re.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(StageTable), menuName = "DataTable/" + nameof(StageTable))]
    public sealed class StageTable : ScriptableObject
    {
        [SerializeField] private List<StageData> dataList = default;

        public List<StageData> data => dataList;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Re.OutGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable))]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> dataList = default;

        public List<SeData> data => dataList;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Re.OutGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmTable), menuName = "DataTable/" + nameof(BgmTable))]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private List<BgmData> dataList = default;

        public List<BgmData> data => dataList;
    }
}
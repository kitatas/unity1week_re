using Re.InGame.Data.DataStore;
using UnityEngine;

namespace Re.InGame.Domain.Repository
{
    public sealed class StageRepository
    {
        private readonly StageTable _stageTable;

        public StageRepository(StageTable stageTable)
        {
            _stageTable = stageTable;
        }

        public GameObject FindByLevel(int level)
        {
            var stageData = _stageTable.data[level];
            return stageData.stage;
        }
    }
}
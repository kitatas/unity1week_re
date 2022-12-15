using Re.InGame.Data.DataStore;

namespace Re.InGame.Domain.Repository
{
    public sealed class StageRepository
    {
        private readonly StageTable _stageTable;

        public StageRepository(StageTable stageTable)
        {
            _stageTable = stageTable;
        }

        public Presentation.View.StageView FindByLevel(int level)
        {
            var stageData = _stageTable.data[level];
            return stageData.stage;
        }
    }
}
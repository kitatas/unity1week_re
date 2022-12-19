using Re.Common.Domain.UseCase;
using Re.InGame.Data.Entity;
using Re.InGame.Domain.Repository;
using UnityEngine;

namespace Re.InGame.Domain.UseCase
{
    public sealed class StageUseCase : BaseModelUseCase<int>
    {
        private readonly StageLevelEntity _stageLevelEntity;
        private readonly StageRepository _stageRepository;

        public StageUseCase(StageLevelEntity stageLevelEntity, StageRepository stageRepository)
        {
            _stageLevelEntity = stageLevelEntity;
            _stageRepository = stageRepository;

            Set(_stageLevelEntity.value);
        }

        public void Increase()
        {
            _stageLevelEntity.Increase();
            Set(_stageLevelEntity.value);
        }

        public bool IsAllStageClear()
        {
            return _stageLevelEntity.value + 1 == StageConfig.STAGE_COUNT;
        }

        public Presentation.View.StageView BuildStage()
        {
            var stage = _stageRepository.FindByLevel(_stageLevelEntity.value);
            return Object.Instantiate(stage, Vector3.zero, Quaternion.identity);
        }
    }
}
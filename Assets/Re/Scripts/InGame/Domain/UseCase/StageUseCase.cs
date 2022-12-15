using System.Threading;
using Cysharp.Threading.Tasks;
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

        private Presentation.View.StageView _currentStage;

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

        public async UniTask BuildStageAsync(float animationTime, CancellationToken token)
        {
            if (_currentStage)
            {
                // 生成済みのStageを削除
                await _currentStage.DisappearAsync(animationTime, token);
                Object.Destroy(_currentStage.gameObject);
            }

            // 新しいStageを生成
            var stage = _stageRepository.FindByLevel(_stageLevelEntity.value);
            _currentStage = Object.Instantiate(stage, Vector3.zero, Quaternion.identity);
            await _currentStage.AppearAsync(animationTime, token);
        }
    }
}
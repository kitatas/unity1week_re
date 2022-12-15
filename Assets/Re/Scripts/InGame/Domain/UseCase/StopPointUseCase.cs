using Re.Common.Domain.UseCase;
using Re.InGame.Data.Entity;
using UnityEngine;

namespace Re.InGame.Domain.UseCase
{
    public sealed class StopPointUseCase : BaseModelUseCase<int>
    {
        private readonly StopPointsEntity _stopPointsEntity;

        public StopPointUseCase(StopPointsEntity stopPointsEntity)
        {
            _stopPointsEntity = stopPointsEntity;
            Set(_stopPointsEntity.stackCount);
        }

        public void Push(Vector2 position, Vector3 rotation)
        {
            var entity = new PointEntity(position, rotation);
            _stopPointsEntity.Push(entity);
            Set(_stopPointsEntity.stackCount);
        }

        public PointEntity Pop()
        {
            var point = _stopPointsEntity.Pop();
            Set(_stopPointsEntity.stackCount);
            return point;
        }

        public bool IsStack()
        {
            return _stopPointsEntity.stackCount > 0;
        }
    }
}
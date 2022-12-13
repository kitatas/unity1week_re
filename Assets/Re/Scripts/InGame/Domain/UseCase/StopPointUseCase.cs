using Re.InGame.Data.Entity;
using UnityEngine;

namespace Re.InGame.Domain.UseCase
{
    public sealed class StopPointUseCase
    {
        private readonly StopPointsEntity _stopPointsEntity;

        public StopPointUseCase(StopPointsEntity stopPointsEntity)
        {
            _stopPointsEntity = stopPointsEntity;
        }

        public void Push(Vector2 position, Vector3 rotation)
        {
            var entity = new PointEntity(position, rotation);
            _stopPointsEntity.Push(entity);
        }

        public PointEntity Pop()
        {
            return _stopPointsEntity.Pop();
        }

        public bool IsStack()
        {
            return _stopPointsEntity.stackCount > 0;
        }
    }
}
using System.Collections.Generic;

namespace Re.InGame.Data.Entity
{
    public sealed class StopPointsEntity
    {
        private readonly Stack<PointEntity> _pointEntities;

        public StopPointsEntity()
        {
            _pointEntities = new Stack<PointEntity>();
        }

        public void Push(PointEntity entity)
        {
            _pointEntities.Push(entity);
        }

        public PointEntity Pop()
        {
            return _pointEntities.Pop();
        }

        public int stackCount => _pointEntities.Count;
    }
}
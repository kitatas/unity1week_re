using UnityEngine;

namespace Re.InGame.Data.Entity
{
    public sealed class PointEntity
    {
        public Vector2 position { get; }
        public Vector3 rotation { get; }

        public PointEntity(Vector2 position, Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
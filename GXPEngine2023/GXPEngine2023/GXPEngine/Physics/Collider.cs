using System;
using System.Collections.Generic;
using GXPEngine;

namespace Physics
{
    /// <summary>
    /// Super class for colliders. Build the other colliders on top of this.
    /// </summary>
    public class Collider
    {
        public readonly GameObject owner;
        public Vec2 position;

        public Collider(GameObject _owner, Vec2 startPosition)
        {
            owner = _owner;
            position = startPosition;

        }

        public virtual CollisionInfo GetEarliestCollision(Collider other, Vec2 velocity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Overlaps(Collider other)
        {
            throw new NotImplementedException();
        }
    }
}

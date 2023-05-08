using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

namespace Physics
{
    class ColliderManager
    {
        public static ColliderManager main
        {
            get
            {
                if (_main == null)
                {
                    _main = new ColliderManager();
                }
                return _main;
            }
        }
        static ColliderManager _main;

        List<Collider> solidColliders;

        public ColliderManager()
        {
            solidColliders = new List<Collider>();
        }

        public void AddSolidCollider(Collider col)
        {
            solidColliders.Add(col);
        }

        public void RemoveSolidCollider(Collider col)
        {
            solidColliders.Remove(col);
        }

        public CollisionInfo MoveUntilCollision(Collider collider, Vec2 velocity)
        {
            CollisionInfo firstCollision = null;
            foreach (Collider other in solidColliders)
            {
                if (other != collider)
                {
                    CollisionInfo collisionInfo = collider.GetEarliestCollision(other, velocity);
                    if (collisionInfo != null && collisionInfo.timeOfImpact < 1)
                    {
                        if (firstCollision == null || firstCollision.timeOfImpact > collisionInfo.timeOfImpact)
                        {
                            firstCollision = collisionInfo;
                        }
                    }
                }
            }
            // Given the earliest time of impact, move to the point of impact:
            float TOI = 1;
            if (firstCollision != null && firstCollision.timeOfImpact < 1 && firstCollision.timeOfImpact >= 0)
            {
                TOI = firstCollision.timeOfImpact;
            }
            collider.position += velocity * TOI;
            return firstCollision;
        }
    }
}

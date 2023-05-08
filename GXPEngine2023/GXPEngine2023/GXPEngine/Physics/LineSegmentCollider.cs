using System;
using GXPEngine;

namespace Physics
{
    class LineSegmentCollider : Collider
    {
        public Vec2 start;
        public Vec2 end;
        public Vec2 lineVector;

        // Create a line segment between position and position + (pDeltaX,0).
        // Convention: the normal points up if and only if pDeltaX is positive.
        public LineSegmentCollider(GameObject owner, Vec2 pStart, Vec2 pEnd) : base(owner, pStart)
        {
            start = pStart;
            end = pEnd;
            lineVector = end - start;
        }
        public override CollisionInfo GetEarliestCollision(Collider other, Vec2 velocity)
        {
            if (other != null && other is BallCollider)
            {
                return GetEarliestCollision((BallCollider)other, velocity);
            }
            else if (other != null && other is LineSegmentCollider)
            {
                return GetEarliestCollision((LineSegmentCollider)other, velocity);
            }
            else return null;
        }
    }
}
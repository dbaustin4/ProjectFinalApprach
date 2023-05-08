using System;
using System.Collections.Generic;
using GXPEngine;

namespace Physics
{
    class BallCollider : Collider
    {
        public static float bounciness = 0.98f;

        public readonly int radius;
        public readonly bool moving;

        float _density = 1;

        public BallCollider(GameObject owner, int pRadius, Vec2 pPosition, bool pMoving = true, float density = 1) : base(owner, pPosition)
        {
            radius = pRadius;
            moving = pMoving;
            _density = density;

        }


        public float Mass
        {
            get
            {
                return radius * radius * _density;
            }
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


        CollisionInfo GetEarliestCollision(BallCollider mover, Vec2 velocity)
        {
            Vec2 _oldPosition = position;
            Vec2 relativePosition = _oldPosition - mover.position;

            //abc formula for TOI calculation
            float a = Mathf.Pow(velocity.Length(), 2);
            float b = (2 * relativePosition).Dot(velocity);
            float c = Mathf.Pow(relativePosition.Length(), 2) - Mathf.Pow(radius + mover.radius, 2);

            float d = (b * b) - (4 * a * c);
            if (d < 0) return null;

            float toi = (-b - Mathf.Sqrt(d)) / (2 * a);

            relativePosition = (_oldPosition + (velocity * toi)) - mover.position;
            Vec2 normal = relativePosition.Normalized();

            if (c < 0)
            {
                if (b < 0) return new CollisionInfo(normal, mover, 0);
                else return null;
            }

            if (toi >= 0 && toi < 1)
            {
                return new CollisionInfo(normal, mover.moving ? mover : null, toi);
            }
            return null;
        }

        CollisionInfo GetEarliestCollision(LineSegmentCollider line, Vec2 velocity)
        {
            Vec2 _oldPosition = position;
            Vec2 relativePosition = _oldPosition - line.start;
            Vec2 lineVector = line.end - line.start;
            Vec2 lineNormal = lineVector.Normal();
            float toi;

            Vec2 projected = relativePosition.Project(lineVector);

            //toi variables
            float a = Vec2.Distance(relativePosition, projected) - radius;  //distance to the line
            float b = -Vec2.Dot(lineNormal, velocity);
            if (b <= 0) return null;
            if (a >= 0)
            {
                toi = a / b;

            }
            else if (a >= -radius)
            {
                toi = 0;
            }
            else return null;

            if (toi <= 1)
            {
                Vec2 poi = (_oldPosition - line.start) + toi * velocity;
                float d = poi.Dot(lineVector.Normalized());
                if (d >= 0 && d <= lineVector.Length())
                {
                    return new CollisionInfo(lineNormal, line, toi);
                }
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using Physics;


class LineSegmentSolid : LineSegment
{
    ColliderManager engine;
    LineSegmentCollider lineCollider;
    BallCollider startCap;
    BallCollider endCap;
    public LineSegmentSolid(Vec2 pStart, Vec2 pEnd) : base(pStart, pEnd)
    {
        engine = ColliderManager.main;
        lineCollider = new LineSegmentCollider(this, pStart, pEnd);
        startCap = new BallCollider(this, 0, pStart);
        endCap = new BallCollider(this, 0, pEnd);
        engine.AddSolidCollider(lineCollider);
        engine.AddSolidCollider(startCap);
        engine.AddSolidCollider(endCap);

    }

    protected override void OnDestroy()
    {
        // Remove the collider when the sprite is destroyed:
        engine.RemoveSolidCollider(lineCollider);
    }
}

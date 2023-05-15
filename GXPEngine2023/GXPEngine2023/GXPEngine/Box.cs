using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using GXPEngine.Core;
using System.Runtime.InteropServices;

public class Box : AnimationSprite
{
    public Box(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {

    }

    public void Push(float vx, float vy)
    {
        if (vx != 0) MoveUntilCollision(vx, 0);
        else MoveUntilCollision(0, vy);
    }

    void Update()
    {
        float gravityY = MyGame.gravityY;
        float gravityX = MyGame.gravityX;
        // TODO: Get gravity direction... Use gravity (acceleration)
        // For now:

        Collision collision = null;
        if (MyGame.gravitysideway) collision = MoveUntilCollision(gravityX, 0);
        else collision = MoveUntilCollision(0, gravityY);
    }
}


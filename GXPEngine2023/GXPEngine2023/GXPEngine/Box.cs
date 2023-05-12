using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;

public class Box : AnimationSprite
{
    public Box(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {

    }

    public void Push(float vx, float vy)
    {
        Console.WriteLine("Box pushed in direction {0},{1}",vx,vy);
    }

    void Update()
    {
        float gravity = MyGame.acceleration;
        // TODO: Get gravity direction... Use gravity (acceleration)
        // For now:

        var collision = MoveUntilCollision(0, gravity);
    }

    
}


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
    private bool horizontalOnly;
    private bool verticalOnly;

    public Box(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows, -1, false, true)
    {
        if (obj != null)
        {
            horizontalOnly = obj.GetBoolProperty("Horizontal only", false);
            verticalOnly = obj.GetBoolProperty("Vertical only", false);

            if (horizontalOnly && verticalOnly)
            {
                throw new Exception("You cannot set Horizontal only and Vertical only to true");
            }
        }
    }

    private int squishSpeed = 5;

    public void Push(float vx, float vy)
    {
        if (vx != 0) MoveUntilCollision(vx, 0);
        else MoveUntilCollision(0, vy);
    }

    void Update()
    {
        float gravityX = 0;
        float gravityY = 0;
        if (!MyGame.gravitysideway && !horizontalOnly) gravityY = MyGame.gravityY;
        else if (!verticalOnly) gravityX = MyGame.gravityX;
        // TODO: Get gravity direction... Use gravity (acceleration)
        // For now:

        Collision collision = MoveUntilCollision(gravityX, gravityY);
        if (collision != null && collision.other is Player)
        {
            MyGame.death = true;
            Player player = (Player)collision.other;
            player.height -= squishSpeed;

            if (player.height < squishSpeed) MyGame.restart = true;
            Console.WriteLine(player.height);
        }
    }

    
}


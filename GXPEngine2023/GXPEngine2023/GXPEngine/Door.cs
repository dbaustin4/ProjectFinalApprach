using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Door : AnimationSprite
{
    float currentX;
    public bool isactive = false;
    public Door(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows)
    {
    }

    void Update()
    {
        if (isactive)
        {
            Console.WriteLine(isactive);
            y = 1000000;
        }
        else
        {
           visible= true;
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is Player)
        {
            Console.WriteLine("Touching the door");
        }
    }
}


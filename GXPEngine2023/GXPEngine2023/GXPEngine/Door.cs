using System;
using GXPEngine;
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
        }
        else
        {
        }
    }
}


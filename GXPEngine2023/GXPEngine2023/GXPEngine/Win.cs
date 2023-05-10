using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;
using TiledMapParser;

public class Win : AnimationSprite
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }

    Vec2 _position;


    public Win(TiledObject winObject) : base("barry.png", 7, 1, -1, false, false)
    {
        if (winObject != null)
        {
            _position.x = winObject.X;
            _position.y = winObject.Y;
        }
    }

    void Update()
    {

    }
}
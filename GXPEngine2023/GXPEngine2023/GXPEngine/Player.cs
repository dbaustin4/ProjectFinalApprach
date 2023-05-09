using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Player : AnimationSprite
{
    public Player(TiledObject tiledObjectPlayer = null) : base("barry.png", 7, 1)
    {
        if (tiledObjectPlayer != null)
        {
        }
        SetCycle(0, 3);
    }

    void Update()
    {

    }
}
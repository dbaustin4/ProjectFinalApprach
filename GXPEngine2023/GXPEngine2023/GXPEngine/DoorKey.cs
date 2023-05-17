using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class DoorKey : AnimationSprite
{
    public DoorKey(TiledObject keyObject) : base("lever_spritesheet.png", 2, 1, -1, false, true)
    {
        SetFrame(0);
    }
}
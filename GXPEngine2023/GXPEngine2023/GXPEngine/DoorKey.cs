using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class DoorKey : AnimationSprite
{
    public DoorKey(string filename, int cols, int rows, TiledObject keyObject) : base(filename, cols, rows, -1, false, true)
    {

    }
}
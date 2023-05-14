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
    private int level = 1;

    public Win(TiledObject winObject) : base("barry.png", 7, 1, -1, false, true)
    {
    }

    public void WinLevel()
    {
        /*
        if (level < 3)
        {
            level++;
            MyGame.levelToLoad = "level" + level + ".tmx";
        }
        else MyGame.levelToLoad = "Main_Menu.tmx";
        */

        MyGame.levelToLoad = "Main_Menu.tmx";

        MyGame.levelComplete = true;
        Console.WriteLine("DONE");
    }
}
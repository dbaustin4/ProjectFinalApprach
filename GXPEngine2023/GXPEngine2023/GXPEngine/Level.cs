using System;
using TiledMapParser;
using GXPEngine;
using System.Collections.Generic;

class Level : GameObject
{
    public static float cameraX;
    public static float cameraWidth;

    Player player;
    TiledLoader loader;
    string currentLevelName;
    public Level(string filename)
    {
        currentLevelName = "level3.tmx";
        loader = new TiledLoader(filename);
        CreateLevel(filename);
    }
    void CreateLevel(string filename)
    {
        loader.rootObject = this;
        loader.addColliders = false;
        loader.LoadImageLayers();
        loader.addColliders = true;
        loader.LoadTileLayers(); // maybe do this manually
        loader.autoInstance = true;
        loader.LoadObjectGroups();

        player = FindObjectOfType<Player>();
        if(player != null) 
        { 
            AddChild(player);
            y = 128 - player.y;
        }

        switch (filename)
        {
            case "level1V2.tmx":
                Camera map1 = new Camera(0, 0, game.width, game.height);
                map1.SetXY(925, 580);
                map1.scale = 1.05f;
                AddChild(map1);
                break;

            case "level2V2.tmx":
                Camera map2 = new Camera(0, 0, game.width, game.height);
                map2.SetXY(960, 550);
                map2.scale = 1;
                AddChild(map2);
                break;

            case "level3V2.tmx":
                Camera map3 = new Camera(0, 0, game.width, game.height);
                map3.SetXY(830, 515);
                map3.scale = 0.93f;
                AddChild(map3);
                break;


        }

        //y = (game.height - 128) - player.y;
        //x = 128 + player.x;
    }

    void Update()
    {
        cameraX = x;

        if (player!= null)
        {
            Scrolling();
        }
        if (Input.GetKeyDown(Key.R))
        {
            MyGame.restart = true;
        }
    }

    void Scrolling()
    {
        int xBoundriesStart = 128;  //The screen boudries for scrolling
        int xBoundriesEnd = 128;  //The screen boudries for scrolling
        int yBoundriesStart = 128;  //The screen boudries for scrolling
        int yBoundriesEnd = 128;  //The screen boudries for scrolling


        if (player.x + x > game.width - xBoundriesEnd)
            x = (game.width - xBoundriesEnd) - player.x;
        if (player.x + x < xBoundriesStart)
            x = xBoundriesStart - player.x;
        if (player.y + y > game.height - yBoundriesStart)
            y = (game.height - yBoundriesStart) - player.y;
        if (player.y + y < yBoundriesEnd)
            y = yBoundriesEnd - player.y;
    }
}

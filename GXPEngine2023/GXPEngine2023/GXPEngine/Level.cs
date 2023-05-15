using System;
using TiledMapParser;
using GXPEngine;
using System.Collections.Generic;

class Level : GameObject
{
    public static float cameraX;
    public static float cameraWidth;

    private List<Button> buttons = new List<Button>();
    private bool allButtonsPressed = false;

    Player player;
    TiledLoader loader;
    string currentLevelName;
    public Level(string filename)
    {
        currentLevelName = filename;
        loader = new TiledLoader(filename);
        CreateLevel();
    }
    void CreateLevel(bool IncludeImageLayer = true)
    {
        loader.rootObject = this;
        loader.addColliders = true;
        loader.LoadImageLayers();
        loader.LoadTileLayers(); // maybe do this manually
        loader.autoInstance = true;
        loader.LoadObjectGroups();

        player = FindObjectOfType<Player>();
        if(player != null) 
        { 
            AddChild(player);
            y = 128 - player.y;
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

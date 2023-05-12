using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Security.AccessControl;

public class MyGame : Game {

    string levelToLoad = "testmap.tmx";
    public string currentLevel;
    internal static float acceleration = 0.8f;

    //private SoundChannel backgroundMusicSC;

    public MyGame() : base(900, 500, false)     
	{
        LoadLevel(levelToLoad, 0);
        OnAfterStep += CheckLoadLevel;

        Console.WriteLine("MyGame initialized");
	}

	
	void Update() {

	}

    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    void CheckLoadLevel()
    {
        if (levelToLoad != null)
        {
            DestroyAll();
            AddChild(new Level(levelToLoad));
            if (levelToLoad != "EndScreen.tmx" && levelToLoad != "MainMenu.tmx")
            {
                //AddChild(new Hud());
            }

            levelToLoad = null;
        }
    }

    public void LoadLevel(string filename, int currentLevelSoundTrack = 1)
    {
        //if (backgroundMusicSC != null)
        //    backgroundMusicSC.Stop();
        //Sound backgroundMusic = new Sound("ping");
        //backgroundMusicSC = backgroundMusic.Play(false, 0, 0.5f, 0);
        levelToLoad = filename;
        currentLevel = filename;
    }

    public void ResetCurrentLevel(int currentLevelSoundTrack = 1)
    {
        DestroyAll();
        //playerData.Reset();
        AddChild(new Level(currentLevel));
        //AddChild(new Hud());
        //if (backgroundMusicSC != null)
        //    backgroundMusicSC.Stop(); 
        //Sound backgroundMusic = new Sound("ping");
        //backgroundMusicSC = backgroundMusic.Play(false, 0, 0.5f, 0);
    }

    static void Main()                          
	{
		new MyGame().Start();                   
	}

}
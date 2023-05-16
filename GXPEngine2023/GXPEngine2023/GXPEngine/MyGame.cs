using System;
using GXPEngine;
using System.Collections.Generic;

public class MyGame : Game {

    internal static string levelToLoad = "level1V2.tmx";
    internal static string BackgroundToLoad = "Background.mp3";
    public string currentLevel;
    internal static bool gravitysideway = false;
    internal static float gravityY = 9.8f;
    internal static float gravityX = 9.8f;
    internal static bool levelComplete = false;
    internal static bool death = false;
    internal static bool restart = false;

    private SoundChannel backgroundMusicSC;

    public MyGame() : base(1920, 1080, true)     
	{
        LoadLevel(levelToLoad, 0);
        OnAfterStep += CheckLoadLevel;

        Console.WriteLine("MyGame initialized");
	}

	
	void Update()
    {
        Console.WriteLine("hello");
        if (levelComplete)
        {
            LoadLevel(levelToLoad, 0);
            OnAfterStep += CheckLoadLevel;
        }
        if (restart || Input.GetKeyDown(Key.R))
        {
            ResetCurrentLevel();
        }
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
        if (backgroundMusicSC != null)
            backgroundMusicSC.Stop();
        Sound backgroundMusic = new Sound(BackgroundToLoad, true, false);
        backgroundMusicSC = backgroundMusic.Play(false, 0, 0.5f, 0);
        levelToLoad = filename;
        currentLevel = filename; 
        gravitysideway = false;
        gravityY = 9.8f;
        gravityX = 9.8f;
        levelComplete = false;
        death = false;
        restart = false;
}

    public void ResetCurrentLevel(int currentLevelSoundTrack = 1)
    {
        DestroyAll();
        //playerData.Reset();
        AddChild(new Level(currentLevel));
        //AddChild(new Hud());
        if (backgroundMusicSC != null)
            backgroundMusicSC.Stop();
        Sound backgroundMusic = new Sound(BackgroundToLoad, true, false);
        backgroundMusicSC = backgroundMusic.Play(false, 0, 0.5f, 0);
        gravitysideway = false;
        gravityY = 9.8f;
        gravityX = 9.8f;
        levelComplete = false;
        death = false;
        restart = false;
    }

    static void Main()                          
	{
		new MyGame().Start();                   
	}

}
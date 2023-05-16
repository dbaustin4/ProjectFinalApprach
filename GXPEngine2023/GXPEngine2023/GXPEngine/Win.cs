using GXPEngine;
using TiledMapParser;

public class Win : AnimationSprite
{
    private int level = 1;
    public bool openDoor;
    private SoundChannel soundEffectSC;
    private bool opened = false;

    public Win(string filename, int cols, int rows, TiledObject winObject) : base(filename, cols, rows, -1, false, true)
    {
        if (winObject != null)
        {
            openDoor = winObject.GetBoolProperty("Door is open", true);
        }
    }

    public void WinLevel()
    {
        if (openDoor)
        {
            if (!opened)
            {
                Sound soundeffect = new Sound("door_open.wav", false, false);
                soundEffectSC = soundeffect.Play(false, 0, 0.5f, 0);
                opened = true;
            }
            if (level < 3)
            {
                level++;
                MyGame.levelToLoad = "level" + level + "V2.tmx";
                //MyGame.BackgroundToLoad = "level" + level + ".mp3";
            }
            else MyGame.levelToLoad = "Main_Menu.tmx";

            MyGame.levelComplete = true;
        }
    }
}
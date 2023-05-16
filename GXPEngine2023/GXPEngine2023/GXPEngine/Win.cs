using GXPEngine;
using TiledMapParser;

public class Win : AnimationSprite
{
    private int level = 1;
    public bool openDoor;
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
            if (level < 3)
            {
                level++;
                MyGame.levelToLoad = "level" + level + ".tmx";
            }
            else MyGame.levelToLoad = "Main_Menu.tmx";

            MyGame.levelComplete = true;
        }
    }
}
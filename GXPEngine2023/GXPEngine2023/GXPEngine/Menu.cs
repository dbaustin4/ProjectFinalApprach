using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Menu : AnimationSprite
{
    GLContext _glcontext;

    private bool mainMenu;

    Vec2 miniMenuPosition = new Vec2();

    public Menu(TiledObject menuObject) : base("barry.png", 7, 1)
    {
        if (menuObject != null)
        {
            mainMenu = menuObject.GetBoolProperty("Main Menu");
        }
    }

    void Update()
    {
        if (mainMenu)
        {
            if (Input.GetKey(Key.ESCAPE))
            {
                _glcontext.Close();
            }
        }
        else
        {
            EasyDraw canvas = new EasyDraw(800, 600);
            canvas.Clear(Color.MediumPurple);
            canvas.Fill(Color.Yellow);
            canvas.Rect(width / 2, height / 2, 200, 200);
            canvas.Fill(50);
            canvas.TextSize(32);
            canvas.TextAlign(CenterMode.Center, CenterMode.Center);
            canvas.Text("Welcome!", width / 2, height / 2);
        }
    }
}
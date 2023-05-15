using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Managers;
using TiledMapParser;

public class Button : AnimationSprite
{
    Door door;
    public Button(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows, -1, false, true)
    {
        collider.isTrigger = true;
    }

    void Update()
    {

        if (game.FindObjectOfType(typeof(Door)) != null) door = (Door)game.FindObjectOfType(typeof(Door));
        else throw new Exception("You cannot create buttons without a door");
        door.isactive = false;

    }

    void OnCollision(GameObject other)
    {
        if (other is Box)
        {
            door.visible = false;
            door.isactive = true;
            door.collider
        }
    }
}

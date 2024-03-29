﻿using System;
using GXPEngine;
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
        //door.isactive = false;
        //door.visible = true;
    }

    void OnCollision(GameObject other)
    {
        if (other is Box)
        {
            door.visible = false;
            door.isactive = true;
        }
    }
}

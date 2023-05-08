using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

class Mover : Sprite {

    int moveDir = 1;
    bool controllable = false;

    float moveSpeed = 2;
    float turnSpeed = 1;

    public Mover() : base("triangle.png"){

        //SetColor(Utils.Random(0, 1f), (Utils.Random(0, 1f), Utils.Random(0, 1f));


    }

    void update() {
        if (controllable)
        {
            if (Input.GetKey(Key.UP)) {
               Move(0,-moveSpeed);
            }
            if (Input.GetKey(Key.DOWN))
            {
                Move(0, moveSpeed);
            }
            if (Input.GetKey(Key.LEFT))
            {
                Move(-turnSpeed, 0);
            }
            if (Input.GetKey(Key.RIGHT))
            {
                Move(turnSpeed, 0);
            }
        } else
        {
            Move(0, 2);
            rotation += moveDir;

            if (Utils.Random(0,100) == 0)
            {
                //moveDir = -turnSpeed * moveDir;
            }

        }

        //reset
        if (Input.GetKeyDown(Key.SPACE)) {
            SetXY(game.width / 2, game.height / 2);
        }
    }
}

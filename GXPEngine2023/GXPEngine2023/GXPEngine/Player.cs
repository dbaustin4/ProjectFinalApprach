using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
class Player : AnimationSprite  {

    int frame = 0;
    int counter = 0;
    int animDelay = 5;
    public Player() : base("barry.png", 7, 1) {
        scale = 2;
    }

    void update() {
        counter++;

        this.currentFrame++;
        if (currentFrame== 4)
        {
            frame = 0;
        }
        SetFrame(frame);
    }
}


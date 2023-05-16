using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine;
using GXPEngine.Core;

public class Box : AnimationSprite
{
    private bool horizontalOnly;
    private bool verticalOnly;
    private bool beingPushed = false;

    SoundChannel soundChannel;
    private bool[] soundCheck = new bool[] { false, false, false };

    public Box(string filename, int cols, int rows, TiledObject obj) : base(filename, cols, rows, -1, false, true)
    {
        if (obj != null)
        {
            horizontalOnly = obj.GetBoolProperty("Horizontal only", false);
            verticalOnly = obj.GetBoolProperty("Vertical only", false);

            if (horizontalOnly && verticalOnly)
            {
                throw new Exception("You cannot set Horizontal only and Vertical only to true");
            }
        }
    }

    private int squishSpeed = 5;

    void Update()
    {
        float gravityX = 0;
        float gravityY = 0;
        if (!MyGame.gravitysideway && !horizontalOnly) gravityY = MyGame.gravityY;
        else if (!verticalOnly) gravityX = MyGame.gravityX;
        // TODO: Get gravity direction... Use gravity (acceleration)
        // For now:

        Collision collision = MoveUntilCollision(gravityX, gravityY);
        if (collision != null && collision.other is Player && !beingPushed)
        {
            Sound soundeffect = new Sound("crushed.wav", false, false);
            MyGame.death = true;
            Player player = (Player)collision.other;
            player.height -= squishSpeed;

            if (player.height < squishSpeed) MyGame.restart = true;
            Console.WriteLine(player.height);
            SoundEffect(1);
        }
        else if (collision != null && !beingPushed) 
        {
            SoundEffect(2);
        }
        else if (collision == null)
        {
            SoundEffect(0);
        }

        beingPushed = false;
    }

    public void Push(float vx, float vy)
    {
        if (vx != 0) MoveUntilCollision(vx, 0);
        else MoveUntilCollision(0, vy);
        beingPushed = true;
        SoundEffect(0);
    }

    public void SoundEffect(int soundnumber = 0)
    {            
        string[] filename = new string[] { "moving_block.wav", "crushed.wav", "thud.wav" };
        Sound sound = new Sound(filename[soundnumber]);
        if (!soundCheck[0])
        {
            soundChannel = sound.Play(false, 0, 0.1f, 0);
            soundCheck[0] = true;
        }
        if (!soundCheck[1] && soundnumber == 1)
        {
            soundChannel = sound.Play(false, 0, 0.8f, 0);
            soundCheck[1] = true;
        }
        if (!soundCheck[2] && soundnumber == 2)
        {
            soundChannel = sound.Play(false, 0, 0.5f, 0);
            soundCheck[2] = true;
        }
        
        if (!soundChannel.IsPlaying)
        {
            for (int i = 0; i < soundCheck.Length; i++)
            {
                soundCheck[i] = false;
            }
        }
    }
}


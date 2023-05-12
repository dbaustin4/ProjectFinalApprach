using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using Physics;
using System.Drawing;

public class Player : AnimationSprite
{
    //public Vec2 position
    //{
    //    get
    //    {
    //        return _position;
    //    }
    //}
    public Vec2 velocity;
    //Vec2 _position;
    float _speed = 5.0f;


    private bool xFlip = false;
    private bool yFlip = false;

    private bool isGravityFlippedY = false;
    private bool isGravityFlippedX = false;
    float speedY = 0;
    float speedX = 0;
    public Player(TiledObject tiledObjectPlayer = null) : base("barry.png", 7, 1, -1, false, true)
    {
        if (tiledObjectPlayer != null)
        {
            //_position.x = tiledObjectPlayer.X + 32;
            //_position.y = tiledObjectPlayer.Y + 32;
        }
        SetCycle(0, 3);

        collider.isTrigger = true;

    }

    void Update()
    {
        // do stuff here
        //gravity declarating
        float gravityY = MyGame.gravityY;
        float gravityX = MyGame.gravityX;

        velocity.y += MyGame.gravityY;
        velocity.x += MyGame.gravityX;

        bool grounded = false;

        //  do MoveUntilCollision (MUC) in gravity direction -> check if grounded (if needed?!)
        //  
        // Next, do "sideways movement" (acceleration in this direction depends on whether he's grounded, and of course key presses


        // example of box interaction, just for downwards gravity:
        //float acceleration = 0.4f; // store in MyGame as well?
        //velocity.y += acceleration;
        //this brings the player down
        Collision collision = null;
        if (MyGame.gravitysideway) collision = MoveUntilCollision(gravityX, 0);
        else collision = MoveUntilCollision(0, gravityY);

        //not touching anything, could be mid air most probably and jumping down
        if (collision != null)
        {
            grounded = true;
            velocity.y = 0;
            y = Mathf.Round(y);
        }

        if (grounded)
        {
            if (Input.GetKeyDown(Key.W))
            {
                MyGame.gravityY = -Math.Abs(MyGame.gravityY);
                MyGame.gravitysideway = false;
            }
            if (Input.GetKeyDown(Key.S))
            {
                MyGame.gravityY = Math.Abs(MyGame.gravityY);
                MyGame.gravitysideway = false;
            }
            if (Input.GetKeyDown(Key.D))
            {
                Console.WriteLine("Right");
                MyGame.gravityX = Math.Abs(MyGame.gravityX);
                MyGame.gravitysideway = true;
            }
            if (Input.GetKeyDown(Key.A))
            {
                Console.WriteLine("Left");
                MyGame.gravityX = -Math.Abs(MyGame.gravityX);
                MyGame.gravitysideway = true;
            }


            //movement
            velocity.x = 0; // this is not really Euler integration/physics movement...
            if (Input.GetKey(Key.LEFT))
            {
                xFlip = true;
                velocity.x = -_speed;
            }
            if (Input.GetKey(Key.RIGHT))
            {
                xFlip = false;
                velocity.x = _speed;
            }
            if (Input.GetKey(Key.UP))
            {
                velocity.y -= _speed;
            }
            if (Input.GetKey(Key.DOWN))
            {
                velocity.y -= -_speed;
            }
            collision = MoveUntilCollision(velocity.x, 0); // move perpendicular to gravity
            collision = MoveUntilCollision(0, velocity.y); // same shit

            if (collision != null && collision.other is Box)
            {
                Box pushee = (Box)collision.other;
                pushee.Push(velocity.x, 0);
            }
        }
        else
        {
            velocity.x = 0;
        }

        Mirror(xFlip, yFlip);


        void UpdateScreenPosition()
        {
            // This is the ONLY bit of code that modifies x and y!
            //x = _position.x;
            //y = _position.y;
        }

        // For efficiency, we put this in player:
        void OnCollision(GameObject other)
        {
            if (other is Box)
            {
                // Might give false positives because of floating point errors!
                Console.WriteLine("Dead!");
            }
        }
    }
}
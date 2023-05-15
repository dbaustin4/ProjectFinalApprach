using GXPEngine;
using GXPEngine.Core;
using System;
using TiledMapParser;

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

    //private bool isGravityFlippedY = false;
    //private bool isGravityFlippedX = false;
    ////float speedY = 0;
    ////float speedX = 0;
    public Player(TiledObject tiledObjectPlayer = null) : base("barry.png", 7, 1, -1, false, true)
    {
        if (tiledObjectPlayer != null)
        {
            //_position.x = tiledObjectPlayer.X + 32;
            //_position.y = tiledObjectPlayer.Y + 32;
        }
        SetCycle(0, 3);

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


        // example of box interaction, just for downwards gravity:
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
            if (Input.GetKeyDown(Key.UP))
            {
                MyGame.gravityY = -Math.Abs(MyGame.gravityY);
                MyGame.gravitysideway = false;

                rotation = 180;
                if (rotation > 180)
                {
                    rotation -= 5;

                    if (rotation < 180) rotation = 180;
                }
                else
                {
                    rotation += 5;

                    if (rotation > 180) rotation = 180;
                }
            }
            if (Input.GetKeyDown(Key.DOWN))
            {
                MyGame.gravityY = Math.Abs(MyGame.gravityY);
                MyGame.gravitysideway = false;

                rotation = 0;
                if (rotation > 0 && rotation < 180)
                {
                    rotation -= 5;

                    if (rotation < 0 || rotation > 360) rotation = 0;
                }
                else
                {
                    rotation += 5;

                    if (rotation < 0 || rotation > 360) rotation = 0;
                }
            }
            if (Input.GetKeyDown(Key.RIGHT))
            {
                MyGame.gravityX = Math.Abs(MyGame.gravityX);
                MyGame.gravitysideway = true;

                rotation = 270;
                if (rotation > 270 || rotation < 90)
                {
                    rotation -= 5;

                    if (rotation < 270) rotation = 270;
                }
                else
                {
                    rotation += 5;

                    if (rotation > 270) rotation = 270;
                }
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                MyGame.gravityX = -Math.Abs(MyGame.gravityX);
                MyGame.gravitysideway = true;

                rotation = 90;
                if (rotation > 90)
                {
                    rotation -= 5;

                    if (rotation < 90) rotation = 90;
                }
                else
                {
                    rotation += 5;

                    if (rotation > 90) rotation = 90;
                }
            }

            if (rotation < 0) rotation = 360 + rotation;
            if (rotation > 360) rotation = rotation - 360;
        


            //movement
            velocity.x = 0; // this is not really Euler integration/physics movement...
            if (Input.GetKey(Key.A))
            {
                xFlip = true;
                velocity.x = -_speed;
            }
            if (Input.GetKey(Key.D))
            {
                xFlip = false;
                velocity.x = _speed;
            }
            if (Input.GetKey(Key.W))
            {
                velocity.y -= _speed;
            }
            if (Input.GetKey(Key.S))
            {
                velocity.y -= -_speed;
            }

            if (!MyGame.gravitysideway) collision = MoveUntilCollision(velocity.x, 0); // move perpendicular to gravity
            else collision = MoveUntilCollision(0, velocity.y); // same shit

            if (collision != null && collision.other is Box)
            {
                Box pushee = (Box)collision.other;
                pushee.Push(velocity.x, 0);
            }
            else if (collision != null && collision.other is Door)
            {
                x += velocity.x;
                y += velocity.y;
            }
        }
        else
        {
            velocity.x = 0;
        }

        // finally:
        //UpdateScreenPosition();

        Mirror(xFlip, yFlip);


        void UpdateScreenPosition()
        {
            // This is the ONLY bit of code that modifies x and y!
            //x = _position.x;
            //y = _position.y;
        }
    }
    // For efficiency, we put this in player:
    void OnCollision(GameObject other)
    {
    }
}
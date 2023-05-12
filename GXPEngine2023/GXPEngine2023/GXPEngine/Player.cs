using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using Physics;

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
    float _speed = 4.9994234f;


    private bool isGravityFlipped = false;
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
        //bottomGravity = true;

        collider.isTrigger = true;

    }

    void Update()
    {
        if (Input.GetKeyDown(Key.W))
        {
            isGravityFlipped = !isGravityFlipped;
            MyGame.acceleration *= 1;
        }
        // do stuff here
        //gravity declarating
        float gravity = MyGame.acceleration;
        velocity.y += gravity;
        velocity.x += gravity;
        //  do MoveUntilCollision (MUC) in gravity direction -> check if grounded (if needed?!)
        //  
        // Next, do "sideways movement" (acceleration in this direction depends on whether he's grounded, and of course key presses


        // example of box interaction, just for downwards gravity:
        //float acceleration = 0.4f; // store in MyGame as well?
        //velocity.y += acceleration;
        //this brings the player down
        var collision = MoveUntilCollision(0, velocity.y);
        bool grounded = false;

        //not touching anything, could be mid air most probably and jumping down
        if (collision != null)
        {
            grounded = true;
            velocity.y = 0;
            y = Mathf.Round(y);
        }

        if (grounded)
        {
            velocity.x = 0; // this is not really Euler integration/physics movement...
            if (Input.GetKey(Key.LEFT))
            {
                velocity.x = -_speed; 
            }
            if (Input.GetKey(Key.RIGHT))
            {
                velocity.x = _speed;
            }
            Console.WriteLine("Velocity: "+velocity);
            collision = MoveUntilCollision(velocity.x, 0); // move perpendicular to gravity



            if (collision !=null && collision.other is Box)
            {
                Box pushee = (Box)collision.other;
                pushee.Push(velocity.x, 0);
            }
        } else
        {
            velocity.x = 0;
        }

        if (Input.GetKey(Key.W))
        {
            Console.WriteLine("FLIP");
            MyGame.acceleration *= -1;
        }


        // finally:
        //UpdateScreenPosition();
    }

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

    /*
    void Update()
    {
        Collision c = MoveUntilCollision(1, 1);
        if (c == null)
        {
            x--;
            y--;
            SwitchGravity(null);
        }
        else
        {
            PlayerMovement();
            Movement();
            SwitchGravity(c.other);
        }
        Movement();
    }

    private void PlayerMovement()
    {
        // Player movement
        velocity.x = 0;
        velocity.y = 0;

        if (Input.GetKey(Key.RIGHT))
        {
            velocity.x += _speed;
        }
        if (Input.GetKey(Key.LEFT))
        {
            velocity.x -= _speed;
        }
        if (Input.GetKey(Key.UP))
        {
            velocity.y -= _speed;
        }
        if (Input.GetKey(Key.DOWN))
        {
            velocity.y += _speed;
        }

        _position.x += velocity.x;
        _position.y += velocity.y;

        
    }

    private void SwitchGravity(GameObject overlap)
    {
        float gravityForce = 0.5f;

        // Change gravity direction based on input
        if (Input.GetKey(Key.W) && !topGravity)
        {
            bottomGravity = false;
            topGravity = true;
            rightGravity = false;
            leftGravity = false;
            y--;
        }
        else if (Input.GetKey(Key.S) && !bottomGravity)
        {
            bottomGravity = true;
            topGravity = false;
            rightGravity = false;
            leftGravity = false;
            y++;
        }
        else if (Input.GetKey(Key.D) && !rightGravity)
        {
            bottomGravity = false;
            topGravity = false;
            rightGravity = true;
            leftGravity = false;
            x++;
        }
        else if (Input.GetKey(Key.A) && !leftGravity)
        {
            bottomGravity = false;
            topGravity = false;
            rightGravity = false;
            leftGravity = true;
            x--;
        }

        if (bottomGravity)
        {
            _position.x = x;
            topGravity = false;
            rightGravity = false;
            leftGravity = false;

            if (overlap == null)
            {
                speedY += gravityForce;
                y += speedY;
                speedX = 0;
            }
            else
            {
                speedY = 0;
                rotation = 0;
            }

            if (rotation > 0 && rotation < 180)
            {
                rotation -= 5;

                if (rotation < 0 || rotation > 360) rotation = 0;
            }
            else
            {
                rotation += 5;

                if(rotation < 0 || rotation > 360) rotation = 0;
            }
        }
        else if (topGravity)
        {
            _position.x = x;
            bottomGravity = false;
            rightGravity = false;
            leftGravity = false;
            
            if (overlap == null)
            {
                speedY -= gravityForce;
                y += speedY;
                speedX = 0;
            }
            else
            {
                speedY = 0;
                rotation = 180;
            }

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
        else if (rightGravity)
        {
            _position.y = y;
            bottomGravity = false;
            topGravity = false;
            leftGravity = false;

            if (overlap == null)
            {
                speedX += gravityForce;
                x += speedX;
                speedY = 0;
            }
            else
            {
                speedX = 0;
                rotation = 270;
            }

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
        else if (leftGravity)
        {
            _position.y = y;
            bottomGravity = false;
            topGravity = false;
            rightGravity = false;

            if (overlap == null)
            {
                speedX += gravityForce;
                x += speedX;
                speedY = 0;
            }
            else
            {
                speedX = 0;
                rotation = 90;
            }

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
    }

    void Movement()
    {
        if (!leftGravity && !rightGravity)
        {
            x = _position.x;
        }
        else if (!bottomGravity && !topGravity)
        {
            y = _position.y;
        }
    }
    */

}
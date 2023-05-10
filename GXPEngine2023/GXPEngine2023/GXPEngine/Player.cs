using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using Physics;

public class Player : AnimationSprite
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }
    public Vec2 velocity;
    Vec2 _position;
    float _speed = 10;

    //gravity direction bools
    bool bottomGravity = false;
    bool topGravity = false;
    bool rightGravity;
    bool leftGravity;

    //bool[4] gravity = new bool { false, false, false, false }

    float speedY = 0;
    float speedX = 0;
    //bool normalGravity = true;
    public Player(TiledObject tiledObjectPlayer = null) : base("barry.png", 7, 1, -1, false, false)
    {
        if (tiledObjectPlayer != null)
        {
            _position.x = tiledObjectPlayer.X + 32;
            _position.y = tiledObjectPlayer.Y + 32;
        }
        SetCycle(0, 3);
        _position.RotateDegree(0);
        bottomGravity = true;
    }

    void Update()
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
        Movement();

        // Change gravity direction based on input
        if (Input.GetKey(Key.W))
        {
            bottomGravity = false;
            topGravity = true;
            rightGravity = false;
            leftGravity = false;
        }
        else if (Input.GetKey(Key.S))
        {
            bottomGravity = true;
            topGravity = false;
            rightGravity = false;
            leftGravity = false;
        }
        else if (Input.GetKey(Key.D))
        {
            bottomGravity = false;
            topGravity = false;
            rightGravity = true;
            leftGravity = false;
        }
        else if (Input.GetKey(Key.A))
        {
            bottomGravity = false;
            topGravity = false;
            rightGravity = false;
            leftGravity = true;
        }

        SwitchGravity();

    }

    private void SwitchGravity()
    {
        float gravityForce = 0.5f;

        if (bottomGravity)
        {
            _position.x = x;
            topGravity = false;
            rightGravity = false;
            leftGravity = false;
            speedY += gravityForce;
            y += speedY;
            speedX = 0;

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

            if (y > 4200 - height)
            {
                y = 4200 - height;
                speedY = 0;
                rotation = 0;
            }
        }
        else if (topGravity)
        {
            _position.x = x;
            bottomGravity = false;
            rightGravity = false;
            leftGravity = false;
            speedY -= gravityForce;
            y += speedY;
            speedX = 0;

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

            if (y < 3700)
            {
                y = 3700;
                speedY = 0;
                rotation = 180;
            }
        }
        else if (rightGravity)
        {
            _position.y = y;
            bottomGravity = false;
            topGravity = false;
            leftGravity = false;
            speedX += gravityForce;
            x += speedX;
            speedY = 0;

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

            if (x > 960 - width)
            {
                x = 960 - width;
                speedX = 0;
                rotation = 270;
            }
        }
        else if (leftGravity)
        {
            _position.y = y;
            bottomGravity = false;
            topGravity = false;
            rightGravity = false;
            speedX -= gravityForce;
            x += speedX;
            speedY = 0;

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

            if (x < 128)
            {
                x = 128;
                speedX = 0;
                rotation = 90;
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

    void MoveUntilCollision(GameObject other)
    {   
        if(other is Box)
        {
            Console.WriteLine("touching boundary");
        }
    }
}
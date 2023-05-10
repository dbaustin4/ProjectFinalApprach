using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

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
    //gravity direction
    float speedY = 0;
    float speedX = 0;
    //bool normalGravity = true;
    public Player(TiledObject tiledObjectPlayer = null) : base("barry.png", 7, 1)
    {
        if (tiledObjectPlayer != null)
        {
            _position.x = tiledObjectPlayer.X + 32;
            _position.y = tiledObjectPlayer.Y + 32;
        }
        SetCycle(0, 3);
    }

    void Update()
    {
        velocity.x = 0;

        if (Input.GetKey(Key.RIGHT))
        {
            velocity.x += _speed;
        }
        if (Input.GetKey(Key.LEFT))
        {
            velocity.x -= _speed;
        }

        _position.x += velocity.x;
        Movement();
        Gravity();
    }

    void Gravity()
    {
        SwitchGravity();
        //InverseGravity();
    }

    private void SwitchGravity()
    {
        if (Input.GetKey(Key.S))
        {
            speedY = speedY + 1;
            y = y + speedY;
            Mirror(false, false);

            if (y > 4200 - height)
            {
                y = 4200 - height;
                speedY = 0;
            }
        } 
        else if (Input.GetKey(Key.W))
        {
            speedY = speedY - 1;
            y = y - speedY;
            Mirror(false, true);

            if (y > 3700 + height)
            {
                y = 3700 + height;
                speedY = 0;
            }
        }
        else if (Input.GetKey(Key.D))
        {
            speedX = speedX + 1;
            x = x + speedX;
            Mirror(true, false);

            if (x > 960 - width)
            {
                x = 960 - width;
                speedX = 0;
            }
        }
        else if (Input.GetKey(Key.A))
        {
            speedX = speedX - 1;
            x = x - speedX;
            Mirror(false, false);

            if (x > 60 + width)
            {
                x = 60 + width;
                speedX = 0;
            }
        }
    }

    void Movement()
    {
        x = _position.x;
    }

    void MoveUntilCollision(GameObject other)
    {   
        if(other is Box)
        {
            Console.WriteLine("touching boundary");
        }
    }
}
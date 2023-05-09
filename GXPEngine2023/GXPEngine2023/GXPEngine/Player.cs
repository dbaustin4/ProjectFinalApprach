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
    float speedY = 0;
    bool normalGravity = true;
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

        if (Input.GetKey(Key.RIGHT) || (Input.GetKey(Key.D)))
        {
            velocity.x += _speed;
        }
        if (Input.GetKey(Key.LEFT) || (Input.GetKey(Key.A)))
        {
            velocity.x -= _speed;
        }

        if (Input.GetKeyDown(Key.SPACE))
        {
            speedY = -10;
        }

        if (normalGravity)
        {
            speedY += 1;
            y += speedY;
        }
      



        /*if(Input.GetKey(Key.G)){
            _position.y += speedY;
        }
        velocity.y += speedY;*/

        _position.x += velocity.x;
        Movement();
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
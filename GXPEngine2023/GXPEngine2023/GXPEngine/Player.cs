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
    float _speed = 5;

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
        velocity.y = 0;

        if (Input.GetKey(Key.RIGHT))
        {
            velocity.x += _speed;
        }

        _position.x += velocity.x;

        Movement();
    }

    void Movement()
    {
        x = _position.x;
        y = _position.y;
    }
}
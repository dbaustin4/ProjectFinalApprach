using GXPEngine;
using GXPEngine.Core;
using System;
using System.Drawing;
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

    private bool gravityFlipReverse = false;
    private bool xFlip = false;
    private bool yFlip = false;

    bool wGravity, sGravity, dGravity, aGravity;
    private float targetRotation = 0.0f;
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
        Rotate();
        float rotationSpeed = 5.0f;
        float angleDifference = targetRotation - rotation;

        if (angleDifference > 180)
        {
            angleDifference -= 360;
        }
        else if (angleDifference < -180)
        {
            angleDifference += 360;
        }

        if (angleDifference != 0.0f)
        {
            float rotationStep = Mathf.Sign(angleDifference) * rotationSpeed;
            rotation += rotationStep;

            // Check if the rotation has reached the target
            if (Mathf.Abs(angleDifference) <= Mathf.Abs(rotationStep))
            {
                rotation = targetRotation;
            }
        }
        PlayerMovement();
    }

    private void Rotate()
    {
        if (Input.GetKey(Key.UP))
        {
            targetRotation = 180.0f;
            wGravity= true;
        }
        else if (Input.GetKey(Key.DOWN))
        {
            targetRotation = 0.0f;
            sGravity = true;
        }
        else if (Input.GetKey(Key.RIGHT))
        {
            targetRotation = 270.0f;
            dGravity= true;
        }
        else if (Input.GetKey(Key.LEFT))
        {
            targetRotation = 90.0f;
            aGravity= true;
        }
    }

    private void PlayerMovement()
    {
        float gravityX = 0;
        float gravityY = 0;

        if (!MyGame.gravitysideway) gravityY = MyGame.gravityY;
        else gravityX = MyGame.gravityX;

        velocity.y += MyGame.gravityY;
        velocity.x += MyGame.gravityX;

        bool grounded = false;

        Collision collision = MoveUntilCollision(gravityX, gravityY);

        if (Input.GetKey(Key.UP))
        {
            MyGame.gravityY = -Math.Abs(MyGame.gravityY);
            MyGame.gravitysideway = false;
            wGravity = true;
            gravityFlipReverse = true;
        }
        if (Input.GetKeyDown(Key.DOWN))
        {
            MyGame.gravityY = Math.Abs(MyGame.gravityY);
            MyGame.gravitysideway = false;
            gravityFlipReverse = false;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            MyGame.gravityX = Math.Abs(MyGame.gravityX);
            MyGame.gravitysideway = true;
            gravityFlipReverse = false;
        }
        if (Input.GetKeyDown(Key.LEFT))
        {
            MyGame.gravityX = -Math.Abs(MyGame.gravityX);
            MyGame.gravitysideway = true;
            gravityFlipReverse = true;
        }

        if (rotation < 0) rotation = 360 + rotation;
        if (rotation > 360) rotation = rotation - 360;


        //for (int i = 0; i < gravitySide.Length; i++)
        //{
        //    gravitySide[i] = true;

        //    switch (i)
        //    {
        //        case 0:
        //            //rotation = 180;
        //            break;
        //        case 1:
        //            //rotation = 180;
        //            break;
        //        case 2:
        //            //rotation = 180;
        //            break;
        //        case 3:
        //            //rotation = 180;
        //            break;
        //    }
        //}

        if (collision != null)
        {
            grounded = true;
            velocity.y = 0;
            y = Mathf.Round(y);
            if (!MyGame.death) Alive(collision);
        }
    }

    void Alive(Collision collision)
    {
        


        //movement
        velocity.x = 0; // this is not really Euler integration/physics movement...
        if (Input.GetKey(Key.A) && !MyGame.gravitysideway)
        {
            velocity.x = -_speed;
            if (gravityFlipReverse) xFlip = false;
            else xFlip = true;
        }
        if (Input.GetKey(Key.D) && !MyGame.gravitysideway)
        {
            velocity.x = _speed;
            if (gravityFlipReverse) xFlip = true;
            else xFlip = false;
        }
        if (Input.GetKey(Key.W) && MyGame.gravitysideway)
        {
            velocity.y -= _speed;
            if (gravityFlipReverse) xFlip = true;
            else xFlip = false;
        }
        if (Input.GetKey(Key.S) && MyGame.gravitysideway)
        {
            velocity.y -= -_speed;
            if (gravityFlipReverse) xFlip = false;
            else xFlip = true;
        }
        if (!MyGame.gravitysideway) collision = MoveUntilCollision(velocity.x, 0); // move perpendicular to gravity
        else collision = MoveUntilCollision(0, velocity.y); // same shit

        if (collision != null)
        {
            OnCollision(collision.other);
        }
        else
        {
            velocity.x = 0;
        }

        // finally:
        //UpdateScreenPosition();

        Mirror(xFlip, yFlip);
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
            Box pushee = (Box)other;
            pushee.Push(velocity.x, velocity.y);

        }
        if (other is Door)
        {
            x += velocity.x;
            y += velocity.y;
        }
        if (other is Win)
        {
            Win win = (Win)other;
            win.WinLevel();
        }
    }
}
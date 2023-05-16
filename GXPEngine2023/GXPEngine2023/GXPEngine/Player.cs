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

    private bool gravityFlipReverse = false;
    private bool xFlip = false;
    private bool yFlip = false;

    private SoundChannel soundEffectSC;
    private bool deathSoundEffect = false;

    //bool[] gravitySide = new bool[4] { false, false, false, false };
    public Player(TiledObject tiledObjectPlayer = null) : base("SpriteSheetCharacter.png", 10, 5, -1, false, true)
    {
        if (tiledObjectPlayer != null)
        {
            //_position.x = tiledObjectPlayer.X + 32;
            //_position.y = tiledObjectPlayer.Y + 32;
        }
        SetCycle(0, 5);
    }

    void Update()
    {
        // do stuff here
        //gravity declarating

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
            gravityFlipReverse = true;
        }
        if (Input.GetKeyDown(Key.DOWN))
        {
            MyGame.gravityY = Math.Abs(MyGame.gravityY);
            MyGame.gravitysideway = false;
            gravityFlipReverse = false;

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
        if (Input.GetKey(Key.RIGHT))
        {
            MyGame.gravityX = Math.Abs(MyGame.gravityX);
            MyGame.gravitysideway = true;
            gravityFlipReverse = false;

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
            gravityFlipReverse = true;

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


        if (collision != null)
        {
            grounded = true;
            velocity.y = 0;
            y = Mathf.Round(y);
            if (!MyGame.death) Alive(collision);
            else SetCycle(39, 10);
        }
        else
        {
            SetCycle(5, 10);
        }

        if (!MyGame.death) Animate(0.25f);
        else Animate(0.4f);
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
        Console.WriteLine(other);
        if (other is Box)
        {
            // Might give false positives because of floating point errors!
            Box pushee = (Box)other;
            pushee.Push(velocity.x, velocity.y); 
            SetCycle(32, 7);
        }
        else SetCycle(14, 17);
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
        if (other is DoorKey)
        {
            Win win = (Win)game.FindObjectOfType(typeof(Win));
            win.openDoor = true;
            other.LateDestroy();
        }
    }
}
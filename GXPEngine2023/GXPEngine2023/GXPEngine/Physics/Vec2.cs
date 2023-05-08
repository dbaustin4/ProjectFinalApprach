using GXPEngine;
using System;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    // TODO: Implement Length, Normalize, Normalized, SetXY methods

    //length of vector
    public float Length()
    {
        return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    }
    //operators
    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x * right.x, left.y * right.y);
    }
    public static Vec2 operator /(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x / right.x, left.y / right.y);
    }

    //Normalize vector
    public void Normalize()
    {
        if (this.Length() > 0)
        {
            this /= this.Length();

        }
    }

    //returns normalized vector
    public Vec2 Normalized()
    {
        if (this.Length() > 0)
        {
            return new Vec2(x / Length(), y / Length());
        }
        else { return new Vec2(0, 0); }
    }

    public override string ToString()
    {
        return String.Format("({0:0.00},{1:0.00})", x, y);
    }

    //sets x and y of the vector
    public void SetXY(float newX, float newY)
    {
        this.x = newX;
        this.y = newY;
    }

    //degrees to radians
    public static float Deg2Rad(float degrees)
    {
        return degrees * (Mathf.PI / 180);
    }

    //radians to degrees
    public static float Rad2Deg(float radians)
    {
        return radians * (180 / Mathf.PI);
    }

    //rotate by radians
    public void RotateRadians(float rads)
    {
        SetXY(x * (float)Math.Cos(rads) - y * (float)Math.Sin(rads), x * (float)Math.Sin(rads) + y * (float)Math.Cos(rads));
    }

    //rotate by degrees
    public void RotateDegree(float deg)
    {
        RotateRadians(Deg2Rad(deg));
    }

    //get the unit vector in degrees
    public static Vec2 GetUnitVectorDegree(float angle)
    {
        Vec2 x = new Vec2(1, 0);
        x.RotateDegree(angle);
        return x;
    }

    //get the unit vector in radians
    public static Vec2 GetUnitVectorRadians(float angle)
    {
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    //get a random unit vector in degrees
    public static Vec2 RandomUnitVector()
    {
        Vec2 vector = GetUnitVectorDegree(Utils.Random(0, 360));
        return vector;
    }

    //set the angle in degrees
    public void SetAngleDegree(float angle)
    {
        this = GetUnitVectorRadians(Deg2Rad(angle)) * Length();
        //RotateDegree(angle - GetAngleDegrees());
    }

    //set the angle in radians
    public void SetAngleRadians(float angle)
    {
        this = GetUnitVectorRadians(angle) * Length();
        //this = GetUnitVectorRadians(angle);
        //RotateRadians(angle - GetAngleRadians());
    }

    //get the angle in radians
    public float GetAngleRadians()
    {
        return (float)Math.Atan2(y, x);
    }

    //get the angle in degrees
    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    //rotate around a point in degrees
    public void RotateAroundDegrees(float angle, Vec2 point)
    {
        Vec2 diff = this - point;
        diff.RotateDegree(angle);
        this = diff + point;
    }

    //rotate around a point in radians
    public void RotateAroundRadians(float angle, Vec2 point)
    {
        Vec2 diff = this - point;
        diff.RotateRadians(angle);
        this = diff + point;
    }

    //dot product calculation
    public static float Dot(Vec2 left, Vec2 right)
    {
        return left.x * right.x + left.y * right.y;
    }
    public float Dot(Vec2 right)
    {
        return Dot(this, right);
    }

    //returns the normal of a vector
    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }
    //reflect
    public Vec2 Reflect(Vec2 normal, float bounciness = 1)
    {
        normal.Normalize();
        this = this - ((1 + bounciness) * (this.Dot(normal) * normal));
        return this;
    }

    //project a vector onto another
    public Vec2 Project(Vec2 other)
    {
        Vec2 normalized = other.Normalized();
        return this.Dot(normalized) * normalized;
    }

    //distance between 2 vectors
    public static float Distance(Vec2 a, Vec2 b)
    {
        Vec2 diff = a - b;
        return diff.Length();
    }

    //zero vector
    public static Vec2 Zero()
    {
        return new Vec2(0, 0);
    }

    //easier operators

    // +
    public static Vec2 operator +(Vec2 left, float right) => new Vec2(left.x + right, left.y + right);
    public static Vec2 operator +(float left, Vec2 right) => new Vec2(left + right.x, left + right.y);

    // -
    public static Vec2 operator -(Vec2 left, float right) => new Vec2(left.x - right, left.y - right);
    public static Vec2 operator -(float left, Vec2 right) => new Vec2(left - right.x, left - right.y);

    // *
    public static Vec2 operator *(Vec2 left, float right) => new Vec2(left.x * right, left.y * right);
    public static Vec2 operator *(float left, Vec2 right) => new Vec2(left * right.x, left * right.y);

    // /
    public static Vec2 operator /(Vec2 left, float right) => new Vec2(left.x / right, left.y / right);
    public static Vec2 operator /(float left, Vec2 right) => new Vec2(left / right.x, left / right.y);

}


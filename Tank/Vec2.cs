using System;
using GXPEngine;

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public Vec2 Add(Vec2 other) 
	{
		x += other.x;
		y += other.y;
		return this;
	}

    public Vec2 Subtract(Vec2 other)
    {
        x -= other.x;
        y -= other.y;
        return this;
    }

    public Vec2 Scale(float scalar)
    {
        x *= scalar;
        y *= scalar;
        return this;
    }

    public void SetXY(Vec2 other)
    {
        x = other.x;
        y = other.y;
    }

    public void SetXY(float a, float b)
    {
        x = a;
        y = b;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float length = Length();
        if(length != 0)
        {
            x = x / length;
            y = y / length;
        }
    }

    public Vec2 Normalized()
    {
        float length = Length();
        if((int)length == 0) { return new Vec2(0, 0); }
        return new Vec2(x / length, y / length);
    }

    public static float Deg2Rad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float Rad2Deg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        return GetUnitVectorRad(Deg2Rad(degrees));
    }

    public static Vec2 RandomUnitVector()
    {
        float angle = Utils.Random(0, Mathf.PI);
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public void SetAngleRadians(float radians)
    {
        float length = Length();
        x = length * Mathf.Cos(radians);
        y = length * Mathf.Sin(radians);
    }

    public void SetAngleDegrees(float degrees)
    {
        SetAngleRadians(Deg2Rad(degrees));
    }

    public float GetAngleRadians()
    {
        float angle = Mathf.Atan2(y, x);
        if(angle < 0) { return angle + Mathf.PI * 2; }
        return angle;
    }

    public float GetAngleDegrees()
    {
        float angle = GetAngleRadians();
        return Rad2Deg(angle);
    }

    public void RotateRadians(float radians)
    {
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }

    public void RotateAroundRadians(Vec2 other, float radians)
    {
        Subtract(other);
        RotateRadians(radians);
        Add(other);
    }

    public void RotateAroundDegrees(Vec2 other, float degrees)
    {
        RotateAroundRadians(other, Deg2Rad(degrees));
    }



	public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
}


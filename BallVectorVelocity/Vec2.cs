﻿using System;
using GXPEngine;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Addition                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public Vec2 Add(Vec2 other)
    {
        x += other.x;
        y += other.y;
        return this;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Subtraction                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public Vec2 Subtract(Vec2 other)
    {
        x -= other.x;
        y -= other.y;
        return this;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Scaling                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public Vec2 Scale(float scalar)
    {
        x = x * scalar;
        y = y * scalar;
        return this;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Length                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Normalize                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void Normalize()
    {
        float length = Length();
        if (length != 0)
        {
            x = x / length;
            y = y / length;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Normalized                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public Vec2 Normalized()
    {
        float length = Length();
        if ((int)length == 0) { return new Vec2(0, 0); }
        Vec2 result = new Vec2(x / length, y / length);
        return result;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Setting X and Y                                           
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void SetXY(float xNew, float yNew)
    {
        x = xNew;
        y = yNew;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Setting X and Y Vec2                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void SetXY(Vec2 other)
    {
        x = other.x;
        y = other.y;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Degrees to Radians                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public static float Deg2Rad(float degrees)
    {
        float radians = degrees * Mathf.PI / 180;
        return radians;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Radians to Degrees                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public static float Rad2Deg(float radians)
    {
        float degrees = radians * 180 / Mathf.PI;
        return degrees;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Get Unit Vector Radian                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Get Unit Vector Degree                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        return GetUnitVectorRad(Deg2Rad(degrees));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Random Unit Vector                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public static Vec2 RandomUnitVector()
    {
        float angle = Utils.Random(0, Mathf.PI * 2);
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Set Angle in Radians                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void SetAngleRadians(float radians)
    {
        float length = Length();
        x = length * Mathf.Cos(radians);
        y = length * Mathf.Sin(radians);
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Set Angle in Degrees                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void SetAngleDegrees(float degrees)
    {
        SetAngleRadians(Deg2Rad(degrees));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Get Angle in Radians                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public float GetAngleRadians()
    {
        float angle = Mathf.Atan2(y, x);
        if (angle < 0) { return angle + Mathf.PI * 2; }
        return angle;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Get Angle in Degrees                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public float GetAngleDegrees()
    {
        float angle = GetAngleRadians();
        return Rad2Deg(angle);
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Rotate in Radians                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void RotateRadians(float radians)
    {
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Rotate in Degrees                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Rotate around point in Radians                                         
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void RotateAroundRadians(Vec2 other, float radians)
    {
        Subtract(other);
        RotateRadians(radians);
        Add(other);
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Rotate around point in Degrees                                        
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void RotateAroundDegrees(Vec2 other, float degrees)
    {
        RotateAroundRadians(other, Deg2Rad(degrees));
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           POI Point of Impact                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    //public static Vec2 PointOfImpact(Vec2 oldPos, float a, float b, Vec2 vel)
    //{
    //    return oldPos + ((a / b) * vel);
    //}

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           DOT Product                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public float Dot(Vec2 other)
    {
        float dotproduct = this.x * other.x + this.y * other.y;
        return dotproduct;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Normal                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public Vec2 Normal()
    {
        Vec2 unitnormal = new Vec2(-this.y, this.x);
        unitnormal.Normalize();
        return unitnormal;
    }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                           Reflection                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public void ReflectOnLine(Vec2 pLine, float pBounciness = 1)
    {
        Vec2 normal = pLine.Normal();
        Reflect(normal, pBounciness);       
    }

    public void Reflect(Vec2 normal, float pBounciness = 1)
    {
        normal.Normalize();
        Subtract(normal.Scale((1 + pBounciness) * Dot(normal)));
    }

    /*
    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        Vec2 normal = pNormal.Normal();
        normal.Normalize();
        Subtract(normal.Scale((1 + pBounciness) * Dot(normal)));
    }
    */


    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                            Operators                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    /* OK */public static Vec2 operator *(Vec2 a, float b) { return new Vec2(a.x * b, a.y * b); }
    /* OK */public static Vec2 operator *(float b, Vec2 a) { return new Vec2(a.x * b, a.y * b); }
    /* OK */public static Vec2 operator /(Vec2 a, float b) { return new Vec2(a.x / b, a.y / b); }
    /*    */public static Vec2 operator +(Vec2 a, float b) { return new Vec2(a.x + b, a.y + b); }
    /*    */public static Vec2 operator -(Vec2 a, float b) { return new Vec2(a.x - b, a.y - b); }

    /*    */public static Vec2 operator *(Vec2 a, Vec2 b) { return new Vec2(a.x * b.x, a.y * b.y); }
    /*    */public static Vec2 operator /(Vec2 a, Vec2 b) { return new Vec2(a.x / b.x, a.y / b.y); }
    /* OK */public static Vec2 operator +(Vec2 a, Vec2 b) { return new Vec2(a.x + b.x, a.y + b.y); }
    /* OK */public static Vec2 operator -(Vec2 a, Vec2 b) { return new Vec2(a.x - b.x, a.y - b.y); }

    //-------------------------------------------------------------------------------------------------------------------------------//
    //							                            To String                                          
    //-------------------------------------------------------------------------------------------------------------------------------//
    public override string ToString()
    {
        return String.Format("({0} ; {1})", x, y);
    }
}

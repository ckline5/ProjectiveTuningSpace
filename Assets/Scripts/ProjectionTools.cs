using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectionTools
{
    const float PERSPECTIVE_FACTOR = 10f;
    static Vector2 X = new Vector2(.5f, -Mathf.Sqrt(3) / 4f) * 1000;
    static Vector2 Y = new Vector2(.5f, Mathf.Sqrt(3) / 4f) * 1000;
    static Vector2 Z = new Vector2(-1, 0) * 1000;
    static Vector2 C = new Vector2(0, 0);

    public static Vector3 Project(Tuple<float,float,float> p)
    {
        return OldProjection(new Vector3(p.Item1, p.Item2, p.Item3));
    }

    static float distanceToRatio(float s)
    {
        return (float) (1.0 - 1.0 / (PERSPECTIVE_FACTOR * s));
    }

    static Vector2 lli(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
    {
        return lli(l1p1.x, l1p1.y, l1p2.x, l1p2.y, l2p1.x, l2p1.y, l2p2.x, l2p2.y);
    }

    static Vector2 lli(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
    {
        float d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (d == 0)
            return new Vector2(0,0);

        float c12 = (x1 * y2 - y1 * x2);
        float c34 = (x3 * y4 - y3 * x4);
        float nx = c12 * (x3 - x4) - c34 * (x1 - x2);
        float ny = c12 * (y3 - y4) - c34 * (y1 - y2);
        return new Vector2(nx / d, ny / d);
    }


    static public Vector2 ThreePointPerspective(Vector3 p)
    {
        if (p.Equals(Vector3.zero))
            return C;

        Vector2 px = Vector2.Lerp(C, X, distanceToRatio(p.x));
        Vector2 pz = Vector2.Lerp(C, Z, distanceToRatio(p.z));

        if (p.y == 0)
            return lli(X, pz, Z, px);

        Vector2 py = Vector2.Lerp(C, Y, distanceToRatio(p.y));
        Vector2 YZ = lli(Y, pz, Z, py);
        Vector2 XY = lli(Y, px, X, py);
        return lli(XY, Z, X, YZ);
    }

    static public Vector2 OldProjection(Vector3 p)
    {
        Vector3 theta = new Vector3(0,45,45) * Mathf.Deg2Rad;
        Vector3 e = new Vector3(1,1,1);

        float sqrt2over2 = 0.70710678118654752440084436210485f;

        //float dx = Mathf.Cos(theta.y) * (Mathf.Sin(theta.z) * p.y + Mathf.Cos(theta.z) * p.x) - Mathf.Sin(theta.y) * p.z;
        //float dy = Mathf.Sin(theta.x) * (Mathf.Cos(theta.y) * p.z + Mathf.Sin(theta.y) * (Mathf.Sin(theta.z) * p.y + Mathf.Cos(theta.z) * p.x)) + Mathf.Cos(theta.x) * (Mathf.Cos(theta.z) * p.y - Mathf.Sin(theta.z) * p.x);
        //float dz = Mathf.Cos(theta.x) * (Mathf.Cos(theta.y) * p.z + Mathf.Sin(theta.y) * (Mathf.Sin(theta.z) * p.y + Mathf.Cos(theta.z) * p.x)) - Mathf.Sin(theta.x) * (Mathf.Cos(theta.z) * p.y - Mathf.Sin(theta.z) * p.x);

        float dx = sqrt2over2 * (sqrt2over2 * p.y + sqrt2over2 * p.x) - sqrt2over2 * p.z;
        float dy = sqrt2over2 * p.y - sqrt2over2 * p.x;
        float dz = sqrt2over2 * (sqrt2over2 * p.y + sqrt2over2 * p.x) + sqrt2over2 * p.z;

        return new Vector2((e.z / dz) * dx + e.x, (e.z / dz) * dy + e.y) * 100;
    }

    static public Vector2 GnomonicProjection(Vector3 p)
    {
        float phi = Mathf.Atan(p.y / (p.x == 0 ? 1 : p.x)); //latitude
        float lambda = Mathf.Atan(p.z / (p.x == 0 ? 1 : p.x)); //longitude
        float phi1 = 45 * Mathf.Deg2Rad; //central latitude
        float lambda0 = 45 * Mathf.Deg2Rad; //central longitude

        float cosc = Mathf.Sin(phi1) * Mathf.Sin(phi) + Mathf.Cos(phi1) * Mathf.Cos(phi) * Mathf.Cos(lambda - lambda0);

        float x = -(Mathf.Cos(phi) * Mathf.Sin(lambda - lambda0)) / cosc;
        float y = (Mathf.Cos(phi1) * Mathf.Sin(phi) - Mathf.Sin(phi1) * Mathf.Cos(phi) * Mathf.Cos(lambda - lambda0)) / cosc;

        return new Vector2(x, y) * 100;
    }

    static public Vector2 MyProjection(Vector3 p)
    {
        float d = 100;

        float s = p.x + p.y + p.z;
        float t = d / s;
        Vector3 c = p * t; //c is a point on the projection plane

        Vector3 normal = Vector3.one;

        float x = Vector3.Dot(c, Vector3.Cross(normal, Vector3.right));
        float y = Vector3.Dot(c, Vector3.Cross(normal, Vector3.back));

        return new Vector2(x, y);
    }
}

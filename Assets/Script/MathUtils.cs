using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    private static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        float result;
        Vector3 projC;
        result = Vector3.Dot((target - a), (b - a).normalized);
        result = Mathf.Clamp(result, 0, Vector3.Distance(a, b));
        projC = a + ((b - a).normalized) * result;
        return projC;
    }

    public static Vector3 GetNearestPoint(Rail rail, DollyView view)
    {
        Vector3 nearestPoint = Vector3.zero;
        float nearestDistance = 100000;
        float temp;
        for (int i = 0; i < rail.nodes.Count - 1; i++)
        {
            temp = Vector3.Distance(GetNearestPointOnSegment(rail.nodes[i].position, rail.nodes[i + 1].position, view.target.transform.position), view.target.transform.position);
            if (temp < nearestDistance)
            {
                nearestDistance = temp;
                nearestPoint = GetNearestPointOnSegment(rail.nodes[i].position, rail.nodes[i + 1].position, view.target.transform.position);
            }

            if (i == rail.nodes.Count - 2 && rail.isLoop)
            {
                temp = Vector3.Distance(GetNearestPointOnSegment(rail.nodes[0].position, rail.nodes[rail.nodes.Count - 1].position, view.target.transform.position), view.target.transform.position);
                if (temp < nearestDistance)
                {
                    nearestDistance = temp;
                    nearestPoint = GetNearestPointOnSegment(rail.nodes[0].position, rail.nodes[rail.nodes.Count - 1].position, view.target.transform.position);
                }
            }
        }
        return nearestPoint;
    }

    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        return (1 - t) * A + t * B;
    }
    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
    {
        return (1 - t) * Vector3.Lerp(A, B, t) + t * Vector3.Lerp(B, C, t);
    }
    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
       return (1 - t)*QuadraticBezier(A, B, C, t) + t * QuadraticBezier(B, C, D, t);
    }
}

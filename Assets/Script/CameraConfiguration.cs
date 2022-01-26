using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiguration : MonoBehaviour
{
    public float yaw, pitch, roll, distance, fov;
    public Vector3 pivot;

    public CameraConfiguration(float _y, float _p, float _r, Vector3 _pivot, float _d, float _f)
    {
        yaw = _y;
        pitch = _p;
        roll = _r;
        pivot = _pivot;
        distance = _d;
        fov = _f;
    }

    public Quaternion GetRotation()
    {
        Quaternion temp = Quaternion.Euler(yaw, pitch, roll);
        return temp;
    }
    public Vector3 GetPosition()
    {
        Vector3 offset = GetRotation() * (Vector3.back * distance);
        return pivot + offset;
    }
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic : MonoBehaviour
{
    Vector3 A, B, C, D;
    Vector3 GetPosition(float t)
    {

    }
    Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {
        pWorld = localToWorldMatrix.MultiplyPoint(pLocal);
    }
    void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = localToWorldMatrix;
    }
}

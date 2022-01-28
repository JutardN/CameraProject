using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic : MonoBehaviour
{
    public Vector3 A, B, C, D;
    Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }
    Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
    {

        return localToWorldMatrix.MultiplyPoint(GetPosition(t));
    }
    void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {

        Gizmos.color = c;

        for(int i = 0; i<20; i++)
        {
            float t = (float)i /(20-1);
            Vector3 sample = GetPosition(t, localToWorldMatrix);
            Gizmos.DrawSphere(sample, 0.25f);
        }
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.30f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.30f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.30f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.30f);
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 mat = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        DrawGizmo(Color.red, mat);
    }
}

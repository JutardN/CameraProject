using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float[] pitch = new float[3];
    public float[] roll = new float[3];
    public float[] fov = new float[3];
    public float yaw, yawSpeed;
    public GameObject target;
    public Cubic curve;
    public float curvePosition, curveSpeed;
    float hzt, vrt;
    float vPitch, vRoll, vFov;

    private void Update()
    {
        hzt = Input.GetAxisRaw("Horizontal");
        vrt = Input.GetAxisRaw("Vertical");
        yaw += hzt * Time.deltaTime * yawSpeed;
        curvePosition += vrt * Time.deltaTime * curveSpeed;
        curvePosition = Mathf.Clamp(curvePosition, 0, 1);
        Matrix4x4 matrix = ComputeCurveToWorldMatrix();
        transform.position = curve.GetPosition(curvePosition, matrix);
        vPitch = Mathf.Lerp(pitch[Mathf.FloorToInt(curvePosition)], pitch[Mathf.FloorToInt(curvePosition)+1] , curvePosition);
        vRoll = Mathf.Lerp(roll[Mathf.FloorToInt(curvePosition)], roll[Mathf.FloorToInt(curvePosition)+1], curvePosition);
        vFov = Mathf.Lerp(fov[Mathf.FloorToInt(curvePosition)], fov[Mathf.FloorToInt(curvePosition)+1], curvePosition);
    }

    public Matrix4x4 ComputeCurveToWorldMatrix()
    {
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        return Matrix4x4.TRS(target.transform.position, rotation, Vector3.one);
    }

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(yaw, vPitch, vRoll,transform.position, 0, vFov);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float[] pitch, roll, fov = new float[3];
    public float yaw, yawSpeed;
    public GameObject target;
    public float curve, curvePosition, curveSpeed,hzt,vrt;

    private void Update()
    {
        pitch[0] = roll[0] = fov[0] =0;
        pitch[1] = roll[1] = fov[1] =1;
        pitch[2] = roll[2] = fov[2] =2;
        transform.position = target.transform.position;
        hzt = Input.GetAxisRaw("Horizontal");
        vrt = Input.GetAxisRaw("Vertical");
        yaw = hzt * Time.deltaTime * yawSpeed;
        curvePosition = vrt * Time.deltaTime * curveSpeed;
        curvePosition = Mathf.Clamp(curvePosition, 0, 1);
    }

    public Matrix4x4 ComputeCurveToWorldMatrix()
    {
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        return Matrix4x4.TRS(target.transform.position, rotation, Vector3.one);
    }
}

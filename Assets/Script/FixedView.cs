using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float yaw, pitch, roll, fov;

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(yaw,pitch,roll,transform.position,0,fov);
    }
}

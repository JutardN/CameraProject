using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll, fov, yawOffsetMax, pitchOffsetMax;
    public GameObject target;
    public GameObject centralPoint;
    
    public float GetCentralYaw()
    {
        return Mathf.Atan2((centralPoint.transform.position - transform.position).normalized.x, (centralPoint.transform.position - transform.position).normalized.z) * Mathf.Rad2Deg;
    }

    public float GetCentralPitch()
    {
        return -Mathf.Asin((centralPoint.transform.position - transform.position).normalized.y) * Mathf.Rad2Deg;
    }
    public float GetYaw()
    {
        return Mathf.Atan2((target.transform.position - transform.position).normalized.x, (target.transform.position - transform.position).normalized.z) * Mathf.Rad2Deg;
    }

    public float GetPitch()
    {
        return -Mathf.Asin((target.transform.position - transform.position).normalized.y) * Mathf.Rad2Deg;
    }


    public float GetClampedYaw()
    {
        float CentralYaw = this.GetCentralYaw();
        float targetYaw = this.GetYaw();
        float deltaYaw = targetYaw - CentralYaw;
        while (deltaYaw > 180) deltaYaw -= 360;
        while (deltaYaw < -180) deltaYaw += 360;

        deltaYaw = Mathf.Clamp(deltaYaw, -yawOffsetMax, yawOffsetMax);

        float finalYaw = CentralYaw + deltaYaw;

        return finalYaw;
    }

    public float GetClampedPitch()
    {
        float CentralPitch = this.GetCentralPitch();
        float targetPitch = this.GetPitch();
        float deltaPitch = targetPitch - CentralPitch;

        deltaPitch = Mathf.Clamp(deltaPitch, -pitchOffsetMax, pitchOffsetMax);

        float finalPitch = CentralPitch + deltaPitch;

        return finalPitch;
    }

    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(GetClampedYaw(), GetClampedPitch(), roll, transform.position, 0, fov);
    }
}

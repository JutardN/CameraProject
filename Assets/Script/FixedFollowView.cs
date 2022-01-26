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


    public bool CheckYawOffset()
    {
        float CentralYaw = this.GetCentralYaw();
        float targetYaw = this.GetYaw();
        if (targetYaw > 180) targetYaw = -180 + (targetYaw - 180);
        else if (targetYaw < -180) targetYaw = -(-180 + (targetYaw - 180));

        if (CentralYaw > 180) CentralYaw = -180 + (CentralYaw - 180);
        else if (CentralYaw < -180) CentralYaw = -(-180 + (CentralYaw - 180));

        if (Mathf.Abs(CentralYaw - targetYaw) > yawOffsetMax)
        {
            return false;
        }
        else return true;
    }

    public bool CheckPitchOffset()
    {
        float CentralPitch = this.GetCentralPitch();
        float targetPitch = this.GetPitch();
        

        if (Mathf.Abs(CentralPitch - targetPitch) > pitchOffsetMax)
        {
            return false;
        }
        else return true;
    }

    public override CameraConfiguration GetConfiguration()
    {
        if(CheckYawOffset())
        {
            if (CheckPitchOffset())
            {
                return new CameraConfiguration(GetYaw(), GetPitch(), roll, transform.position, 0, fov);
            }
            return new CameraConfiguration(GetYaw(), pitchOffsetMax, roll, transform.position, 0, fov);
        }

        if (CheckPitchOffset())
        {
            return new CameraConfiguration(yawOffsetMax, GetPitch(), roll, transform.position, 0, fov);
        }

        return new CameraConfiguration(yawOffsetMax, pitchOffsetMax, roll, transform.position, 0, fov);
    }
}

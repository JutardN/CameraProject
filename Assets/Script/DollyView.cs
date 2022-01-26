using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    public float roll, distance, fov, distanceOnRail, speed;
    public GameObject target;
    public Rail rail;
    float hzt;
    public bool isAuto;


    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            transform.position = MathUtils.GetNearestPoint(rail, this);
        }
        else
        {
            hzt = Input.GetAxisRaw("Horizontal");
            distanceOnRail += Time.deltaTime * hzt * speed;
            distanceOnRail = Mathf.Clamp(distanceOnRail, 0, Mathf.Infinity);
            transform.position = rail.GetPosition(distanceOnRail);
        }

    }

    public float GetYaw()
    {
        return Mathf.Atan2((target.transform.position - transform.position).normalized.x, (target.transform.position - transform.position).normalized.z) * Mathf.Rad2Deg;
    }

    public float GetPitch()
    {
        return -Mathf.Asin((target.transform.position - transform.position).normalized.y) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmos()
    {
        rail.Draw(rail.GetPosition(distanceOnRail));
    }
    public override CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(GetYaw(), GetPitch(), roll, transform.position, distance, fov);
    }
}

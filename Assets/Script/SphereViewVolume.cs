using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public GameObject target;
    public float innerRadius, outerRadius;

    private float distance;


    private void Update()
    {
        innerRadius = Mathf.Clamp(innerRadius, 0, outerRadius);
        distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= outerRadius && !IsActive) SetActive(true);
        if (distance > outerRadius && IsActive) SetActive(false);
    }

    private void OnDrawGizmos()
    {
        DrawGizmo(Color.red, Color.cyan);
    }

    public void DrawGizmo(Color innerColor, Color outerColor)
    {
        Gizmos.DrawWireSphere(transform.position, Mathf.Clamp(innerRadius, 0, outerRadius));
        Gizmos.color = innerColor;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
        Gizmos.color = outerColor;
    }
    public override float ComputeSelfWeight()
    {

        return innerRadius/outerRadius;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AViewVolume : MonoBehaviour
{
    public int priority = 0;
    public AView view;
    protected bool IsActive { get; private set; }

    private void Start()
    {
        SetActive(IsActive);
    }

    public virtual float ComputeSelfWeight()
    {
        return 1.0f;
    }

    protected void SetActive(bool isActive)
    {
        if (isActive)
        {
            
        }
    }
}

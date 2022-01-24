using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart;

    private void Start()
    {
        if (isActiveOnStart)
        {
            SetActive(false);
        }
    }

    public virtual CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(0,0,0,new Vector3(0,0,0),0,0);
    }

    void SetActive(bool isActive)
    {
        isActiveOnStart = isActive;
    }

}

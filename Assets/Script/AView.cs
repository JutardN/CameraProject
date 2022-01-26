using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;
    public bool isActiveOnStart;

    private void Start()
    {
       
            SetActive(isActiveOnStart);
    }

    public virtual CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(0,0,0,new Vector3(0,0,0),0,0);
    }

    void SetActive(bool isActive)
    {
        if(isActive)
        {
            CameraController.instance.AddView(this);
        }
        else
        {
            CameraController.instance.RemoveView(this);
        }
    }

}

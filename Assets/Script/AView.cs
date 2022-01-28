using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public float weight;

    //////////////// EXERCICE 2 /////////////
    //public bool isActiveOnStart;

    private void Start()
    {
        /////////////// EXERCICE 2 ////////////////
        //SetActive(isActiveOnStart);
    }

    public virtual CameraConfiguration GetConfiguration()
    {
        return new CameraConfiguration(0, 0, 0, new Vector3(0, 0, 0), 0, 0);
    }

    public void SetActive(bool isActive)
    {
        Debug.Log("tu rentres ?");
        if (isActive)
        {
            CameraController.instance.AddView(this);
        }
        else
        {
            CameraController.instance.RemoveView(this);
        }
    }

    public virtual void OnDrawGizmos()
    {
        CameraConfiguration temp = GetConfiguration();
        temp.DrawGizmos(Color.green);
    }
}

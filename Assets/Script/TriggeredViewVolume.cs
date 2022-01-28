using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredViewVolume : AViewVolume
{
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    
    public string tagTarget = "Target";
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            SetActive(true);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            SetActive(false);
        }
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        /*GUILayout.BeginHorizontal("box", GUILayout.Width(30), GUILayout.Height(30));
        GUILayout.Label("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah");
        GUILayout.EndHorizontal();*/
        
        for(int i = 0; i<ViewVolumeBlender.instance.activeViewVolumes.Count; i++)
        {
            GUI.Label(new Rect(5, i*30 + 5, 300, 300), ViewVolumeBlender.instance.activeViewVolumes[i].name, guiStyle);
        }
    }
}

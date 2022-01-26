using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    List<AViewVolume> activeViewVolumes;
    Dictionary<AView, List<AViewVolume>> volumesPerViews;
    public static ViewVolumeBlender instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddVolume(AViewVolume view)
    {
        activeViewVolumes.Add(view);
        if(!volumesPerViews.ContainsKey(view.view))
        {
            List<AViewVolume> viewList = new List<AViewVolume>();
            viewList.Add(view);
            volumesPerViews.Add(view.view, viewList);
            view.view.gameObject.SetActive(true);
        }
        else
        {
            volumesPerViews[view.view].Add(view);
        }
    }
    public void RemoveVolume(AViewVolume view)
    {
        activeViewVolumes.Remove(view);
        volumesPerViews[view.view].Remove(view);
        if (volumesPerViews[view.view].Count == 0)
        {
            view.view.gameObject.SetActive(false);
            volumesPerViews.Remove(view.view);
        }
    }
}

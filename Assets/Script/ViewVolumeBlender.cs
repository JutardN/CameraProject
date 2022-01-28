using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    public List<AViewVolume> activeViewVolumes;
    Dictionary<AView, List<AViewVolume>> volumesPerViews = new Dictionary<AView, List<AViewVolume>>();
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


    public void Update()
    {
        int maxPriority = 0;
        for(int i = 0; i < activeViewVolumes.Count; i++)
        {
            if(activeViewVolumes[i].priority < maxPriority)
            {
                activeViewVolumes[i].view.weight = 0;
            }
            else
            {
                activeViewVolumes[i].view.weight = Mathf.Max(activeViewVolumes[i].view.weight, activeViewVolumes[i].ComputeSelfWeight());
                maxPriority = activeViewVolumes[i].priority;
            }
        }
    }
    public void AddVolume(AViewVolume view)
    {
        
        activeViewVolumes.Add(view);
        if(!volumesPerViews.ContainsKey(view.view))
        {
            Debug.Log("et là ?");
            List<AViewVolume> viewList = new List<AViewVolume>();
            viewList.Add(view);
            volumesPerViews.Add(view.view, viewList);
            view.view.SetActive(true);
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
            view.view.SetActive(false);
            volumesPerViews.Remove(view.view);
        }
    }
}

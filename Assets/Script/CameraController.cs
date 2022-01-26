using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public static CameraController instance = null;
    public List<AView> activeViews;
    public CameraConfiguration currentConfig;
    public CameraConfiguration targetConfig;
    public float speed;


    float timer = 0;

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
    private void Start()
    {
    }


    void ApplyConfiguration(Camera camera, CameraConfiguration configuration)
    {
        camera.transform.rotation = configuration.GetRotation();
        camera.transform.position = configuration.GetPosition();
    }

    private void Update()
    {

        //////////////////// Exercice 3 ////////////////
        //CameraBlend(currentConfig);

        ////////////////// Exercice 2 //////////////////
        CameraConfiguration tempConfig = new CameraConfiguration(ComputeAverageYaw(), AveragePitch(), AverageRoll(), AveragePivot(), AverageDistance(), AverageFov());
        camera.transform.rotation = tempConfig.GetRotation();
        camera.transform.position = tempConfig.GetPosition();
    }
    private void OnDrawGizmos()
    {
        new CameraConfiguration(ComputeAverageYaw(), AveragePitch(), AverageRoll(), AveragePivot(), AverageDistance(), AverageFov()).DrawGizmos(Color.magenta);
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }
    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }
    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;
        foreach (AView config in activeViews)
        {
            sum += new Vector2(Mathf.Cos(config.GetConfiguration().yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.GetConfiguration().yaw * Mathf.Deg2Rad)) * config.weight;
        }
        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public float AveragePitch()
    {
        float pitch = 0;
        float weight = 0;
        foreach (AView config in activeViews)
        {
            pitch += config.GetConfiguration().pitch * config.weight;
            weight += config.weight;
        }
        return pitch / weight;
    }

    public float AverageRoll()
    {
        float roll = 0;
        float weight = 0;
        foreach (AView config in activeViews)
        {
            roll += config.GetConfiguration().roll * config.weight;
            weight += config.weight;
        }
        return roll / weight;
    }

    public Vector3 AveragePivot()
    {
        Vector3 pivot = Vector3.zero;
        float weight = 0;
        foreach (AView config in activeViews)
        {
            pivot += (config.GetConfiguration().pivot) * config.weight;
            weight += config.weight;
        }
        return pivot / weight;
    }

    public float AverageDistance()
    {
        float distance = 0;
        float weight = 0;
        foreach (AView config in activeViews)
        {
            distance += config.GetConfiguration().distance * config.weight;
            weight += config.weight;
        }
        return distance / weight;
    }

    public float AverageFov()
    {
        float fov = 0;
        float weight = 0;
        foreach (AView config in activeViews)
        {
            fov += config.GetConfiguration().fov * config.weight;
            weight += config.weight;
        }
        return fov / weight;
    }

    public void CameraBlend(CameraConfiguration blendConfiguration)
    {

        if (speed * Time.deltaTime < 1)
        {
            blendConfiguration.yaw = blendConfiguration.yaw + (targetConfig.yaw - blendConfiguration.yaw) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw;
            blendConfiguration.pitch = blendConfiguration.pitch + (targetConfig.pitch - blendConfiguration.pitch) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw;
            blendConfiguration.roll = blendConfiguration.roll + (targetConfig.roll - blendConfiguration.roll) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw;
            blendConfiguration.distance = blendConfiguration.distance + (targetConfig.distance - blendConfiguration.distance) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw;
            blendConfiguration.fov = blendConfiguration.fov + (targetConfig.fov - blendConfiguration.fov) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw;
            blendConfiguration.pivot = blendConfiguration.pivot + (targetConfig.pivot - blendConfiguration.pivot) * speed * Time.deltaTime;                   //(1 - timer) * currentConfig.yaw + timer * targetConfig.yaw; 
        }
        else
        {
            blendConfiguration = targetConfig;
        }

        ApplyConfiguration(camera, blendConfiguration);

    }

}

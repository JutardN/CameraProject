using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{

    public bool isLoop;
    public List<Transform> nodes;

    private float length;

    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            nodes.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        this.DrawGizmos(Color.green);
        this.Draw(GetPosition(distance));
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        for(int i = 0; i<nodes.Count -1; i++)
        {
            Gizmos.DrawSphere(nodes[i].position, 0.25f);
            Gizmos.DrawLine(nodes[i].position, nodes[i +1].position);
        }
        Gizmos.DrawSphere(nodes[nodes.Count - 1].position, 0.25f);
        if(isLoop)
        {
            Gizmos.DrawLine(nodes[nodes.Count - 1].position, nodes[0].position);
        }
        
    }


    public void Draw(Vector3 position)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(position, 0.25f);
    }

    public float GetLength()
    {
        float length = 0;
        for(int i = 0; i < nodes.Count -1; i++)
        {
            length += Vector3.Distance(nodes[i].position, nodes[i + 1].position);
        }
        if(isLoop)
        {
            length += Vector3.Distance(nodes[nodes.Count - 1].position, nodes[0].position);
        }
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        float length = GetLength();
        float distanceActuelle = 0;
        bool found = false;
        Vector3 position = Vector3.zero;
        Vector3 direction = Vector3.zero;
        int i = 0;
        distance = distance % length;
        while (i < nodes.Count -1 && !found)
        {
            
            if (distance < distanceActuelle + Vector3.Distance(nodes[i].position, nodes[i + 1].position))
            {
                direction = nodes[i + 1].position - nodes[i].position;
                position = direction + direction.normalized * (distance - distanceActuelle);
                found = true;
            }
            
            distanceActuelle += Vector3.Distance(nodes[i].position, nodes[i + 1].position);
            i++;

            if(i == nodes.Count && isLoop && !found)
            {
                if (distance < distanceActuelle + Vector3.Distance(nodes[i].position, nodes[0].position))
                {
                    direction = nodes[0].position - nodes[i].position;
                    position = direction + direction.normalized * (distance - distanceActuelle);
                    found = true;
                }
            }
        }
        return position;
    }

}

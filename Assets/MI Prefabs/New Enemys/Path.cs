using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Transform[] pathPointArray;

    public List<Transform> pathPointList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() //This method is only for viewing purpose in the editior and not will be added in the finak build
    {
        //Setting color of gizmos
        Gizmos.color = Color.green;

        //Getting the positions of the Childrens;
        pathPointArray = GetComponentsInChildren<Transform>();

        //Setting the pathpoint Array in the pathlist
        pathPointList.Clear(); //This List is Always updating So we need to clear it every time
        foreach(Transform pointPosition in pathPointArray)
        {
            if(pointPosition != this.transform)
            {
                pathPointList.Add(pointPosition);
            }
            
        }

        //Drawing The Line
        for (int i = 0; i < pathPointList.Count; i++)
        {
            Vector2 position = pathPointList[i].position;

            if(i>0)
            {
                Vector2 previousPosition = pathPointList[i - 1].position;
                Gizmos.DrawLine(previousPosition, position);
                Gizmos.DrawWireSphere(position, 0.1f);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Range(1, 20)] public int curvedPathDensity = 2;
    int overload;
    Transform[] pathPointArray;
    public List<Transform> pathPointList = new List<Transform>();
    public List<Vector2> curvedPathPointList = new List<Vector2>();


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

        //Curved Path

        //Check Overload
        if(pathPointList.Count %2 == 0)
        {
            pathPointList.Add(pathPointList[pathPointList.Count - 1]);
            overload = 2;
        }
        else
        {
            pathPointList.Add(pathPointList[pathPointList.Count - 1]);
            pathPointList.Add(pathPointList[pathPointList.Count - 1]);
            overload = 3;
        }

        //Creating Curve Path Point List
        curvedPathPointList.Clear();
        Vector2 lineStart = pathPointList[0].position;
        for (int i = 0; i < pathPointList.Count - overload; i=i+2)
        {
            for(int j=0; j<=curvedPathDensity; j++)
            {
                Vector2 lineEnd = GetPoint(pathPointList[i].position, pathPointList[i + 1].position, pathPointList[i + 2].position, j / (float)curvedPathDensity);
             
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lineStart, lineEnd);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(lineStart, 0.1f);
                lineStart = lineEnd;
                curvedPathPointList.Add(lineStart);
            }
        }
    }

    Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        return Vector2.Lerp(Vector2.Lerp(p0, p1, t), Vector2.Lerp(p1, p2, t), t);
    }
}

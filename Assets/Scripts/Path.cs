using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Color pathColor = Color.green;
    Transform[] pathPointArray;
    [Range(1, 20)] public int lineDensity = 13;
    public List<Transform> straightPathPointList = new List<Transform>();
    public List<Vector2> curvedPathPointList = new List<Vector2>();
    int overload;
    int totalStraightPathPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        //Creating Straight Path
        Gizmos.color = pathColor;
        //Filling the array
        pathPointArray = GetComponentsInChildren<Transform>();

        //Clear the list
        straightPathPointList.Clear();

        //Adding pat point to path list
        foreach(Transform obj in pathPointArray)
        {
            if(obj != this.transform)
            {
                straightPathPointList.Add(obj);
            }
        }

        //Draw the points and lines from one point to next point
        totalStraightPathPoint = straightPathPointList.Count;


        for(int i =0; i<totalStraightPathPoint; i++)
        {
            Vector2 currentPos = straightPathPointList[i].position;
            Gizmos.DrawSphere(currentPos, 0.3f);
            if (i>0)
            {
                Vector2 previousPos = straightPathPointList[i - 1].position;
                Gizmos.DrawLine(previousPos, currentPos);               
            }
        }

        //Curved Path

        //Check Overload
        if (totalStraightPathPoint %2 == 0)
        {
            straightPathPointList.Add (straightPathPointList[totalStraightPathPoint-1]);
            overload = 2;
        }

        else
        {
            straightPathPointList.Add(straightPathPointList[totalStraightPathPoint-1]);
            straightPathPointList.Add(straightPathPointList[totalStraightPathPoint-1]);
            overload = 3;
        }


        //Curve Creation
        curvedPathPointList.Clear();
        Vector2 lineStart = straightPathPointList[0].position;
        totalStraightPathPoint = straightPathPointList.Count;
        for (int i = 0; i <(straightPathPointList.Count - overload); i+=2)
        {
            for (int j = 0; j <=lineDensity; j++)
            {
                Vector2 lineEnd = GetPoint(straightPathPointList[i].position, straightPathPointList[i + 1].position, straightPathPointList[i + 2].position, (j / (float)lineDensity));
                
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lineStart, lineEnd);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(lineStart, 0.1f);
                lineStart = lineEnd;
                curvedPathPointList.Add(lineStart);
            }

        }

    }

    Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float time)
    {
        return Vector2.Lerp(Vector2.Lerp(p0, p1, time), Vector2.Lerp(p1, p2, time), time);
    }
}

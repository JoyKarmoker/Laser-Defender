using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Formation : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 2;
    [SerializeField] float gridOffsetX = 1f;
    [SerializeField] float gridOffsetY = 1f;
    [SerializeField] int div = 4;

    public List<Vector2> gridList = new List<Vector2>();

    //Formation Movement
    [Header("Movement")]
    public float maxMoveOffsetX = 2f;
    public float spped = 2f;
    int direction = -1;
    Vector2 startPos;
    float currentPosX;

    private void Start()
    {
        startPos = transform.position;
        currentPosX = transform.position.x;
        //CreateFormation();
    }

    private void Update()
    {
        currentPosX = currentPosX + Time.deltaTime * spped * direction;
        if(currentPosX >= maxMoveOffsetX)
        {
            direction = direction * (-1);
            currentPosX = maxMoveOffsetX;
        }

        else if(currentPosX <= (-maxMoveOffsetX))
        {
            direction = direction * (-1);
            currentPosX = (-maxMoveOffsetX);
        }

        transform.position = new Vector2(currentPosX, startPos.y);
    }

    private void OnDrawGizmos()
    {
        int num = 0;
        CreateFormation();
        foreach(Vector2 pos in gridList)
        {
            Handles.Label(GetVector(num), num.ToString());
            num++;
        }
    }

    private void CreateFormation()
    {
        gridList.Clear();
        int num = 0;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                float x = (gridOffsetX + gridOffsetX * 2 * (num / div)) * Mathf.Pow((-1), ((num % 2) + 1));
                float y = gridOffsetY * ((num % div) / 2);
                Vector2 vec = new Vector2(x,y);
                gridList.Add(vec);
                num++;
            }
        }
    }

    public Vector2 GetVector(int posInFormation)
    {
        return (Vector2)gameObject.transform.position + (Vector2)gridList[posInFormation];
    }
}

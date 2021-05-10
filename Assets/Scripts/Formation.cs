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
    public float speed = 2f;
    int direction = -1;
    Vector2 startPos;
    float currentPosX;

    //Formation Spreading
    [HideInInspector]public bool spreadStarted;
    float spreadAmount = 1.5f;
    float spreadSpeed = 1f;
    float curSpread;
    int spreadDir = 1;

    //[System.Serializable]
    public class FormationSpread
    {
        public int index;
        public float xPos;
        public float yPos;
        public GameObject enemy;

        public Vector2 target;
        public Vector2 start;       
        public FormationSpread(int _index, float _xPos,  float _yPos, GameObject _enemy)
        {
            index = _index;
            xPos = _xPos;
            yPos = _yPos;
            enemy = _enemy;

            start = new Vector2(_xPos, _yPos);
            target = new Vector2(_xPos + (+xPos * 0.3f), _yPos);
        }

    }
    [HideInInspector] public List<FormationSpread> enemyInThisFormation = new List<FormationSpread>();



    private void Start()
    {
        startPos = transform.position;
        currentPosX = transform.position.x;
        CreateFormation();
    }

    private void Update()
    {

        if(!spreadStarted)
        {
            currentPosX = currentPosX + Time.deltaTime * speed * direction;
            if (currentPosX >= maxMoveOffsetX)
            {
                direction = direction * (-1);
                currentPosX = maxMoveOffsetX;
            }

            else if (currentPosX <= (-maxMoveOffsetX))
            {
                direction = direction * (-1);
                currentPosX = (-maxMoveOffsetX);
            }

            transform.position = new Vector2(currentPosX, startPos.y);
        }

        else
        {
            transform.position = startPos; // setting the position of the formation to the initial position before spreading;
            curSpread += Time.deltaTime * spreadSpeed * spreadDir;
            if(curSpread >= spreadAmount || curSpread <=0)
            {
                spreadDir *= (-1);
            }

            int totalEnemy = enemyInThisFormation.Count;
            Debug.Log("Total enemys " + totalEnemy);
            for (int i = 0; i < totalEnemy; i++)
            {
                if (Vector2.Distance(enemyInThisFormation[i].enemy.transform.position, enemyInThisFormation[i].target) >= 0.001f)
                {
                    enemyInThisFormation[i].enemy.transform.position = Vector2.Lerp((Vector2)transform.position + (Vector2)enemyInThisFormation[i].start, (Vector2)transform.position + enemyInThisFormation[i].target, curSpread);
                }
            }
        }


    }

    /*private void OnDrawGizmos()
    {
        gridList.Clear();
        int num = 0;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                float x = (gridOffsetX + gridOffsetX * 2 * (num / div)) * Mathf.Pow((-1), ((num % 2) + 1));
                float y = gridOffsetY * ((num % div) / 2);
                Vector2 vec = new Vector2(this.transform.position.x + x, this.transform.position.y + y);
                
                //Visualize the Formation
                Handles.Label(vec, num.ToString());
                gridList.Add(vec);
                num++;
            }
        }
    }*/

    private void OnDrawGizmos()
    {
        int num = 0;
        CreateFormation();
        foreach(Vector2 pos in gridList)
        {
            Gizmos.DrawWireSphere(GetVector(num), 0.1f);
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
                Vector2 vec = new Vector2(x, y);
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

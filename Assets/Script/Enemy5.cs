using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Vector2 moveVector;
    private float bombPlacementTime;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        bombPlacementTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
         if(bombPlacementTime < 0){
           Instantiate(cube, transform.position, transform.rotation);
           bombPlacementTime = 1f;
       }

       bombPlacementTime -= Time.deltaTime;
    }
}

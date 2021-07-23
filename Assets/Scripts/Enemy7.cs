using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : MonoBehaviour
{
    public float startSpeed = 8f;
    private float speed;

    [SerializeField]
    private Vector3[] positions;
    private float countdown = 3f;

    private int index;
    private void Start() {
        speed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if(transform.position == positions[index]){
            if(index == positions.Length - 1){
                index = 0;
            }
            else{
                index++;
            }
        }
        if(countdown <= 2){
            speed = startSpeed - 5f;
            Debug.Log("Laser.....!!!");
            }
        if(countdown <= 0){
            speed = startSpeed;
            countdown = 3f;
             Debug.Log("Laser....stop.!!!");
            }
            countdown -= Time.deltaTime;
            //Debug.Log(countdown);
        }
        
    }
   

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    [Header("Stage1 Bullet")]
    public GameObject[] bulletPrefab;

    [Header("Stage2 Bullet")]
    public GameObject[] bulletPrefab1;

    [Header("Stage3 Bullet")]
    public GameObject[] bulletPrefab2;


    [Header("Stage4 Bullet")]
    public GameObject[] bulletPrefab3;

    [Header("General")]
    public float range = 2.5f;

    //Stage 2,3 and 4 movement positions
   // public Transform[] positions;

    //Boss Movement speed
    public float speed;
    bool switc = true;
    bool counter = true;

    //Start time for bullet spawning
    public float bulletTime = 5f;
   // private Transform target;

    //for random movement
    private Vector3 target1;

  //  private int wavepointIndex ;
    int count = 0;

    //Deciding Boss stage
    public static int bossStage;
    public int stage = 1;
    Vector3 objHeight;

    //Get player position
    [HideInInspector]
    public Transform playerPos;

    void Start()
    {
       // bulletTime += Random.Range(0f,0.5f);
       //Boss1 height measure
       objHeight.y = GetComponent<SpriteRenderer>().bounds.size.y/2;
       objHeight.y = objHeight.y - 0.25f;

       //Boss stage intialize 
       bossStage = stage;
       if(bossStage == 0){
           bossStage = 1;
       }
        // target = positions[0];

        //taking random position for movement
        target1 = new Vector3(Random.Range(-2.5f, 2.5f), 3f, 0f);
        Debug.Log("tar" + target1);

        //set the target for boss1 stage2 player follower bullet 
        if (playerPos == null) {
			if (GameObject.FindWithTag ("Player")!=null)
			{
				playerPos = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
    }

    void positionChange()
    {
        target1 = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(0.0f, 3.5f));
    }

    // Update is called once per frame
    void Update()
    {
       // wavepointIndex = 0;
        //Get into the scene
        if(counter){
           transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }
        //Boss Movement selection
        if(transform.position.y <= 3.0f && bossStage == 1){
         counter = false;
         StartCoroutine(MoveObject());
        }else if(transform.position.y <=3.0f && bossStage == 2){
            counter = false;
            StartCoroutine(Move());
        }else if(transform.position.y <=3.0f && bossStage == 3){
            counter = false;
            StartCoroutine(Move());
        }
        else if (transform.position.y <= 3.0f && bossStage == 4)
        {
            counter = false;
            StartCoroutine(Move());
        }
        //shooting bullet 
        bulletTime -= Time.deltaTime;
        if(bulletTime < 0.0f){
           Bullet();
        }
    }

    public void SetTarget(Transform newTarget)
	{
		playerPos = newTarget;
	}

    void moveRight(){
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }

     void moveLeft(){
        transform.Translate(-speed*Time.deltaTime, 0, 0);
    }
    void Bullet(){
        if(count==0){
        StartCoroutine(Shoot());
        } 
        count++;
    }
    //Move Boss1 for stage1
   IEnumerator MoveObject(){
        yield return new WaitForSeconds(0.2f);
      /*  if(bulletTime < 0f){
            yield return new WaitForSeconds(0.2f);
        }*/
         if(switc){
           moveRight();
        }
        if(!switc){
            moveLeft();
        }
        if(transform.position.x >= 3.0f){
            switc = false;
        }
        if(transform.position.x <= -3.0f){
            switc = true;
        }
    }
    //Move Boss for stage 2 and 3
    IEnumerator Move(){
        yield return new WaitForSeconds(0.2f);
        if (bulletTime < 0f)
        {
            yield return new WaitForSeconds(0.2f);
        }
        if (Vector3.Distance(transform.position, target1) <= 1f)
        {
            positionChange();
            yield return new WaitForSeconds(2f);
        }
        transform.position = Vector3.Lerp(transform.position, target1, Time.deltaTime * speed);
        
        // Vector3 dir = target.position - transform.position;
        //  Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        /* if(Vector3.Distance(transform.position,target.position) <= 0.4f){
            GetNextWaypoint();
         }*/
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

   /* void GetNextWaypoint(){
        pos1 = new Vector3(Random.Range(1, 2f), Random.Range(1, 2f), 0f);
        Debug.Log(pos1);
       // target.position = transform.position + pos1;
       int index = wavepointIndex;  
       while(index == wavepointIndex){
           wavepointIndex = Random.Range(0,positions.Length);
       }
       if(wavepointIndex >=  positions.Length - 1){
          wavepointIndex = 0;
       }
       wavepointIndex++;
       target = positions[wavepointIndex];
   }*/

    //spawn bullets for different stages
    IEnumerator Shoot(){
            yield return new WaitForSeconds(0.2f);
            if(bossStage == 1){
            GameObject bullet1 = (GameObject) Instantiate(bulletPrefab[0], transform.position - objHeight, Quaternion.identity);
            GameObject bullet2 = (GameObject) Instantiate(bulletPrefab[1], transform.position - objHeight, Quaternion.identity);
            GameObject bullet3 = (GameObject) Instantiate(bulletPrefab[2], transform.position - objHeight, Quaternion.identity);
           
            }
            else if(bossStage == 2){
            GameObject bullet1 = (GameObject) Instantiate(bulletPrefab1[0], transform.position - objHeight, Quaternion.identity);
            bullet1.GetComponent<bossBullet1Movement>().ShootAtPlayer(playerPos);
            GameObject bullet2 = (GameObject) Instantiate(bulletPrefab1[1], transform.position - objHeight, Quaternion.identity);
            GameObject bullet3 = (GameObject) Instantiate(bulletPrefab1[2], transform.position - objHeight, Quaternion.identity);
            }
           
            if(bossStage == 3){
            GameObject bullet1 = (GameObject) Instantiate(bulletPrefab2[0], transform.position - objHeight, Quaternion.identity);
            GameObject bullet2 = (GameObject) Instantiate(bulletPrefab2[1], transform.position - objHeight, Quaternion.identity);
            GameObject bullet3 = (GameObject) Instantiate(bulletPrefab2[2], transform.position - objHeight, Quaternion.identity);
            GameObject bullet4 = (GameObject) Instantiate(bulletPrefab2[3], transform.position - objHeight, Quaternion.identity);
            GameObject bullet5 = (GameObject) Instantiate(bulletPrefab2[4], transform.position - objHeight, Quaternion.identity);  
            }
            if(bossStage == 3){
            GameObject bullet1 = (GameObject) Instantiate(bulletPrefab2[0], transform.position - objHeight, Quaternion.identity);
            GameObject bullet2 = (GameObject) Instantiate(bulletPrefab2[1], transform.position - objHeight, Quaternion.identity);
            GameObject bullet3 = (GameObject) Instantiate(bulletPrefab2[2], transform.position - objHeight, Quaternion.identity);
            GameObject bullet4 = (GameObject) Instantiate(bulletPrefab2[3], transform.position - objHeight, Quaternion.identity);
            GameObject bullet5 = (GameObject) Instantiate(bulletPrefab2[4], transform.position - objHeight, Quaternion.identity);  
            }
            if (bossStage == 4)
            {
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab3[0], transform.position - objHeight, Quaternion.identity);
            bullet1.GetComponent<bossBullet1Movement>().ShootAtPlayer(playerPos);
            Destroy(bullet1, 8f);
            GameObject bullet2 = (GameObject)Instantiate(bulletPrefab3[1], transform.position - objHeight, Quaternion.identity);
            bullet2.GetComponent<bossBullet1Movement>().ShootAtPlayer(playerPos);
            Destroy(bullet2, 8f);
            GameObject bullet3 = (GameObject)Instantiate(bulletPrefab3[2], transform.position - objHeight, Quaternion.identity);
            bullet3.GetComponent<bossBullet1Movement>().ShootAtPlayer(playerPos);
            Destroy(bullet3, 8f);
            GameObject bullet4 = (GameObject)Instantiate(bulletPrefab3[3], transform.position - objHeight, Quaternion.identity);
            bullet4.GetComponent<bossBullet1Movement>().ShootAtPlayer(playerPos);
            Destroy(bullet4, 8f);
        }
            bulletTime = 2f;
            count = 0;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    public float speed;
    public float t;
    bool switc = true;
    bool counter = true;
    public float bulletTime = 5f;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject bulletPrefab4;
    public GameObject bulletPrefab5;
    public Transform[] positions;
    private Transform target;
    private int wavepointIndex = 0;
    int count = 0;
    public static int bossStage;
    public int stage = 1;
    Vector3 objHeight;

    
    void Start()
    {
       // bulletTime += Random.Range(0f,0.5f);
       objHeight.y = GetComponent<MeshRenderer>().bounds.size.y/2;
       bossStage = stage;
       if(bossStage == 0){
           bossStage = 1;
       }
       target = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Get into the scene
        if(counter){
           transform.Translate(0, -speed * Time.deltaTime, 0f);
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
        //shooting bullet 
        bulletTime -= Time.deltaTime;
        if(bulletTime < 0.0f){
           Bullet();
        }
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
   IEnumerator MoveObject(){
        yield return new WaitForSeconds(0.2f);
        if(bulletTime < 0f){
            yield return new WaitForSeconds(1f);
        }
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

    IEnumerator Move(){
        yield return new WaitForSeconds(0.2f);
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position,target.position) <= 0.4f){
           GetNextWaypoint();
        }
    }

     void GetNextWaypoint(){
       if(wavepointIndex >=  positions.Length - 1){
          wavepointIndex = 0;
       }
       wavepointIndex++;
       target = positions[wavepointIndex];
   }

    IEnumerator Shoot(){
            yield return new WaitForSeconds(0.2f);
            if(bossStage == 1 || bossStage == 3){
            GameObject bullet1 = (GameObject) Instantiate(bulletPrefab, transform.position - objHeight, Quaternion.identity);
            }
            else if(bossStage == 2){
             GameObject bullet4 = (GameObject) Instantiate(bulletPrefab3, transform.position - objHeight, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.05f);
            GameObject bullet2 = (GameObject) Instantiate(bulletPrefab1, transform.position - objHeight, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
            GameObject bullet3 = (GameObject) Instantiate(bulletPrefab2, transform.position - objHeight, Quaternion.identity);
            bulletTime = 2f;
            if(bossStage == 3){
            yield return new WaitForSeconds(0.05f);
            GameObject bullet5 = (GameObject) Instantiate(bulletPrefab4, transform.position - objHeight, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
            GameObject bullet6 = (GameObject) Instantiate(bulletPrefab5, transform.position - objHeight, Quaternion.identity);  
            }
            count = 0;
    }
    
}

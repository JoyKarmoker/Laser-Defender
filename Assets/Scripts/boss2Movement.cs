using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Movement : MonoBehaviour
{
    [Header("Stage1 Bullet")]
    public GameObject bulletPrefab;

    [Header("Stage2 Bullet")]
    public GameObject[] bulletPrefab1;

    [Header("Stage3 Bullet")]
    public GameObject bulletPrefab2;

    private Vector3 target1;
    private Vector3 target2;
    public float intialSpeed;
    private float speed;
    bool counter = true;
    bool switc = true;
    int count = 0;
    public GameObject target;

    //Start time for bullet spawning
    public float bulletTime = 5f;
    Vector3 objHeight;
    //for boss stage
    public static int boss1Stage;
    public int stage = 1;
    private int called;

    // Start is called before the first frame update
    void Start()
    {
        //Boss1 height measure
        objHeight.y = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        objHeight.y = objHeight.y - 0.1f;

        //taking random position for movement
        target1 = new Vector3(Random.Range(-2.5f, 2.5f), 3f, 0f);
        target2 = new Vector3(0f, 4.0f, 0f);
        speed = intialSpeed;
        boss1Stage = stage;
        called = 0;

        // if no target specified, assume the player
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player");
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
        if (counter)
        {
            transform.position = Vector3.Lerp(transform.position, target2, Time.deltaTime * speed);
           // transform.Translate(0f, -speed * Time.deltaTime , 0f);
        }
        if ( transform.position.y <= 4.5f && boss1Stage == 2)
        {
            counter = false;
            StartCoroutine(Move());
        }
        if (transform.position.y <= 4.5f && boss1Stage == 3)
        {
            counter = false;
            StartCoroutine(Move());
        }
        //shooting bullet 
        bulletTime -= Time.deltaTime;
        if (bulletTime <= 0.0f)
        {
            Bullet();
        }
    }

    void moveRight()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void moveLeft()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    void Bullet()
    {
        if (count == 0)
        {
            StartCoroutine(Shoot());
        }
        count++;
    }

    IEnumerator MoveObject()
    {
        yield return new WaitForSeconds(0.2f);
          if(bulletTime < 0f){
              yield return new WaitForSeconds(0.5f);
              speed = 0;
          }
        if (switc)
        {
            speed = intialSpeed;
            moveRight();
        }
        if (!switc)
        {
            speed = intialSpeed;
            moveLeft();
        }
        if (transform.position.x >= 3.0f)
        {
            switc = false;
        }
        if (transform.position.x <= -3.0f)
        {
            switc = true;
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(0.2f);
        if (bulletTime < 0f)
        {
            yield return new WaitForSeconds(1f);
        }
        if (Vector3.Distance(transform.position, target1) <= 1f)
        {
            positionChange();
            yield return new WaitForSeconds(2f);
        }
        transform.position = Vector3.Lerp(transform.position, target1, Time.deltaTime * speed);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        if (boss1Stage == 1)
        {
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position - objHeight, Quaternion.identity);
            bulletTime = 0.5f;
        }
        if(boss1Stage == 2)
        {
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab1[0], transform.position - objHeight, Quaternion.Euler(new Vector3(0f, 0f, -180f)));
            bullet1.GetComponent<Boss2BulletMovement>().SetTarget(target);
            GameObject bullet2 = (GameObject)Instantiate(bulletPrefab1[1], transform.position - objHeight, Quaternion.Euler(new Vector3(0f, 0f, -180f)));
            bullet2.GetComponent<Boss2BulletMovement>().SetTarget(target);
            GameObject bullet3 = (GameObject)Instantiate(bulletPrefab1[2], transform.position - objHeight, Quaternion.Euler(new Vector3(0f, 0f, -180f)));
            bullet3.GetComponent<Boss2BulletMovement>().SetTarget(target);
            bulletTime = 2f;
        }
        if(boss1Stage == 3)
        {
            called++;
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab2, transform.position - objHeight, Quaternion.identity);
            bullet1.GetComponent<boss2BulletStage3>().count(called);
            Destroy(bullet1, 2f);
            bulletTime = 3f;
        }
        Debug.Log(bulletTime);
        count = 0;
    }
}

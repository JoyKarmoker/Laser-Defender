using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2BulletMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [Header("General")]
    public float angel = 0f;
    public float speed;
    [Header("Stage1 Movement")]
    public float width = 0.0075f;
    public float height = 0.005f;
    [HideInInspector]
    public GameObject target;
    private Transform target1;
    [Header("Stage2 Movement")]
    public float bulletSpeedInX = 3f;
    public bool downstraight = true;
    public bool downrightSided = false;
    public bool downleftSided = false;
    private float cheasingTime;
    public float strength = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (boss2Movement.boss1Stage == 1)
        {
            rigidbody.velocity = new Vector2(0, -speed);
        }
        if (boss2Movement.boss1Stage == 2)
        {
            if (downstraight)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(0, -speed);
            }
            if (downrightSided)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(bulletSpeedInX, -speed);
            }
            if (downleftSided)
            {
                transform.eulerAngles = new Vector3(0f, 0f, -angel);
                rigidbody.velocity = new Vector2(-bulletSpeedInX, -speed);
            }
            cheasingTime = 0.5f;
        }
        if(boss2Movement.boss1Stage == 3)
        {
            if(angel < 90)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(bulletSpeedInX, -speed);
            }
            if(angel > 270)
            {
                transform.eulerAngles = new Vector3(0f, 0f, -(360 - angel));
                rigidbody.velocity = new Vector2(bulletSpeedInX, -speed);
            }
            if(angel>90 && angel <= 180)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(bulletSpeedInX, speed);
            }
            if(angel > 180 && angel < 270)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(bulletSpeedInX, speed);
            }
            if(angel == 90 || angel == 270)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angel);
                rigidbody.velocity = new Vector2(bulletSpeedInX, 0);
            }
        }

    }

    // Set the target of the chaser
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        target1 = target.GetComponent<Transform>();
    }

    public void SetAngle(float bulletAngel,float speedInX)
    {
        angel = bulletAngel;
        bulletSpeedInX = speedInX;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss2Movement.boss1Stage == 1)
        {
            transform.localScale += new Vector3(width, height, 0) * Time.deltaTime;
        }
        if (boss2Movement.boss1Stage == 2)
        {
            cheasingTime -= Time.deltaTime;
            if (cheasingTime <= 0f)
            {
                Chase();
            }
        }
    } 

    void Chase()
    {
        if(angel != 0)
        {
            Vector3 relativePos = target1.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            var str = Mathf.Min(strength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation,str);
        }

        float movementDistance = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target1.position, movementDistance);
    }
}

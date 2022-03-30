using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet1Movement : MonoBehaviour
{
    Rigidbody2D rigidbody;
   // public Transform target;
    public float rotateSpeed = 200f;
    public float rotateAmount;
    //Fire at a angel
    public float angel = 0f;
    public float bullet4Speed = 500;
    public float speed;
    public float bulletSpeedInX = 3f;
    public bool downstraight = true;
    public bool downrightSided = false;
    public bool downleftSided = false;
    public bool upstraight = false;
    public bool uprightSided = false;
    public bool upleftSided = false;
    public bool playerFollwer = false;
    private Vector3 targetPosition;
    private Vector3 direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (downstraight){
         transform.eulerAngles = new Vector3(0f, 0f, angel);
         rigidbody.velocity = new Vector2(0, -speed);
        }
        if (upstraight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, angel);
            rigidbody.velocity = new Vector2(0, speed);
        }
        if (downrightSided){
         transform.eulerAngles = new Vector3(0f, 0f, angel);
         rigidbody.velocity = new Vector2(bulletSpeedInX, -speed);  
       
        }
        if (uprightSided)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -angel);
            rigidbody.velocity = new Vector2(bulletSpeedInX, speed);

        }
        if (downleftSided){
         transform.eulerAngles = new Vector3(0f, 0f, -angel);
         rigidbody.velocity = new Vector2(-bulletSpeedInX, -speed);
        }
        if (upleftSided)
        {
            transform.eulerAngles = new Vector3(0f, 0f, angel);
            rigidbody.velocity = new Vector2(-bulletSpeedInX, speed);
        }
        if (playerFollwer)
        {
            rigidbody.AddForce(direction * speed);
        }

      //  target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //Vector3 position = transform.position;
        // position.y -= 1000;
        // moveDirection = Vector3.forward;
        //Vector3 direction = position - transform.position;
        // 
        // Debug.Log(transform.up);
        //  var direction = Quaternion.Euler(0f, angel , 0f) * (moveDirection - transform.position);
        //Vector3 direction1 = Quaternion.AngleAxis(angel, Vector3.forward) * Vector3.right;
        //  _targetPosition = transform.position + direction1;
        // _targetPosition = moveDirection;
        //  float xcomponent = Mathf.Cos(angel * Mathf.PI/180) * speed;
        //  float ycomponent = Mathf.Sin(angel * Mathf.PI/180) * speed;

        //   rigidbody.AddForce(direction1 * speed);
        // _targetPosition = moveDirection;
    }
    public void ShootAtPlayer(Transform target)
    {
        targetPosition = target.position;
        direction = targetPosition - transform.position;
    }

    void FixedUpdate()
    {
        if (bossMovement.bossStage == 4)
        {
           // Debug.Log(target.position);
            Vector2 direction = (Vector2)targetPosition - rigidbody.position;
            direction.Normalize();
            rotateAmount = Vector3.Cross(direction, transform.up).z;

            rigidbody.angularVelocity = -rotateAmount * rotateSpeed;
            rigidbody.velocity = transform.up * bullet4Speed * Time.deltaTime;
        }
    }


}

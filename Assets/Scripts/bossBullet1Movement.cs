using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet1Movement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    //Fire at a angel
    public float angel = 0f;
    public float speed;
    public float bulletSpeedInX = 3f;
    public bool straight = true;
    public bool rightSided = false;
    public bool leftSided = false;
 
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if(bossMovement.bossStage == 3){
            angel = angel - 10;
        }
        if(straight){
         transform.eulerAngles = new Vector3(0f, 0f, angel);
         rigidbody.velocity = new Vector2(0, -speed);
        }
        if(rightSided){
         transform.eulerAngles = new Vector3(0f, 0f, angel);
         rigidbody.velocity = new Vector2(bulletSpeedInX, -speed);  
       
        }
        if(leftSided){
         transform.eulerAngles = new Vector3(0f, 0f, -angel);
         rigidbody.velocity = new Vector2(-bulletSpeedInX, -speed);
        }

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
    // Update is called once per frame
   /*void Update()
    {
        
        
        //moveDirection = Vector3.up * -1;
        
        //Vector3 direction1 = Quaternion.AngleAxis(angel, -Vector3.forward) * direction;
       // 
       // _targetPosition.Normalize();
      // 
        transform.position = Vector2.Lerp(transform.position, _targetPosition, Time.deltaTime * speed);
       
    }
    void Update(){
        Vector2 position = transform.position;
        position.y -= speed;

        transform.position = position;
    }*/

    
    
}

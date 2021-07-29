using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet1Movement : MonoBehaviour
{
    Rigidbody rigidbody;
 //   Vector3 moveDirection;
   // public Transform moveDirection;
    public float angel = 0f;
    public float speed;
 //   public GameObject target;
 //   private Vector3 _targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if(bossMovement.bossStage == 3){
            angel = angel - 10;
        }
        //Vector3 position = transform.position;
       // position.y -= 1000;
       // moveDirection = Vector3.forward;
       //Vector3 direction = position - transform.position;
       // 
       // Debug.Log(transform.up);
      //  var direction = Quaternion.Euler(0f, angel , 0f) * (moveDirection - transform.position);
       Vector3 direction1 = Quaternion.AngleAxis(angel, Vector3.forward) * Vector3.right;
      //  _targetPosition = transform.position + direction1;
      // _targetPosition = moveDirection;
      //  float xcomponent = Mathf.Cos(angel * Mathf.PI/180) * speed;
      //  float ycomponent = Mathf.Sin(angel * Mathf.PI/180) * speed;

        rigidbody.AddForce(direction1 * speed);
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

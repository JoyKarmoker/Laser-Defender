using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1BulletStage2Movement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    private Vector3 targetPosition;
    private Vector3 direction;

    void Awake() {
       rigidbody = GetComponent<Rigidbody2D>(); 
    }
    
    void Update()
    {
       rigidbody.AddForce(direction * speed);
      // transform.Translate(direction * speed * Time.deltaTime , Space.World);
    }

    public void ShootAtPlayer(Transform target){
      targetPosition = target.position;
      direction = targetPosition - transform.position;
    }

}

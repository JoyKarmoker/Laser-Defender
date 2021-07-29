using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1BulletStage2Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    public GameObject target;
    public float speed;
    private Vector3 targetPosition;
    private Vector3 direction;

    void Awake() {
       rigidbody = GetComponent<Rigidbody>(); 
    }
    void Start()
    {
        targetPosition = target.transform.position ;
        direction = targetPosition - transform.position;
        //Debug.Log(target.transform.position);
        direction.Normalize();
     //   rigidbody.AddForce(targetPosition * speed, ForceMode.Impulse);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime , Space.World);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.000001f;
    [HideInInspector]
    // Start is called before the first frame update
    void Start()
    {

    }


    public void move(Vector3 pos){
      
       Vector2 direction = pos - transform.position ;

       direction.Normalize();

       Rigidbody2D rb = GetComponent<Rigidbody2D>();

       rb.AddForce(direction * speed);

    }
}

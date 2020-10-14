using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 500f;
    public Transform[] targetList;
    private Rigidbody2D rb;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //Have to find the target 
        int targetIndex = Random.Range(0, targetList.Length);
        //Debug.Log("target index " + targetIndex);
        target = targetList[targetIndex];
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount =  Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

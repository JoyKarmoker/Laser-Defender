using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    Transform target;
    GameObject player;
    public float speed = 5f;
    public float rotationSpeed = 200f, enemyCatcherRange = 5f;
    float shortestDistance = Mathf.Infinity;
    bool foundEnemy = false;

    Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed * Time.deltaTime;
        StartCoroutine(SetTargetToNull());
    }
    private void FixedUpdate()
    {
        if(target == null)
        {
            foundEnemy = false;
        }

        if (!foundEnemy)
        {
            target = FindClosestEnemy();
        }
        else
        {
            FollowTarget();
        }
    }

    private Transform FindClosestEnemy()
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, enemyCatcherRange,enemyLayer);

        if (enemyCollider != null)
        {
            target = enemyCollider.gameObject.transform;

            foundEnemy = true;
        }

        return target;
    }

    private void FollowTarget()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    IEnumerator SetTargetToNull()
    {
        yield return new WaitForSeconds(1f);
        target = null;
        StartCoroutine(SetTargetToNull());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, enemyCatcherRange);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActualHomingMissile : MonoBehaviour
{
    GameObject[] enemies;
    Transform target;
    GameObject player;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    float shortestDistance = Mathf.Infinity;

    Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        target = FindClosestEnemy();
        StartCoroutine(WaitToFindClosestTarget());
    }

    IEnumerator WaitToFindClosestTarget()
    {
        yield return new WaitForSeconds(0.5f);
        target = FindClosestEnemy();
        StartCoroutine(WaitToFindClosestTarget());
    }
    private void OnEnable()
    {
        StartCoroutine(WaitToFindClosestTarget());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void FixedUpdate()
    {

        FollowTarget(target);
    }

    private Transform FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        shortestDistance = Mathf.Infinity;
        //target = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                target = enemy.transform;
            }
        }
        return target;
    }

    private void FollowTarget(Transform target)
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.velocity = transform.up * speed;
    }
}

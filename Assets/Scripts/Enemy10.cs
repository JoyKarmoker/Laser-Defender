using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy10 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float singleFireDelay = 5f;
    [SerializeField] float brustFireDelay = 5f;
    [SerializeField] int howManyShootInASession = 4;

    int shootCounter;

    [SerializeField] GameObject projectilePrefabLeft;
    [SerializeField] GameObject projectilePrefabRight;

    [SerializeField] Transform firePointLeft;
    [SerializeField] Transform firePointRight;

    bool isShooting = true;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        StartCoroutine(ShootContinuous());
    }

    public IEnumerator ShootContinuous()
    {
        while (true)
        {
            if (isShooting)
            {
                yield return new WaitForSeconds(singleFireDelay);

                Instantiate(projectilePrefabLeft, firePointLeft.position, firePointLeft.rotation);
                Instantiate(projectilePrefabRight, firePointRight.position, firePointRight.rotation);

                shootCounter++;

                if (shootCounter == howManyShootInASession)
                    StartCoroutine(OffShootingForSec());
            }
            yield return null;
        }
        yield return 0;
    }

    IEnumerator OffShootingForSec()
    {
        shootCounter = 0;
        isShooting = false;

        yield return new WaitForSeconds(brustFireDelay);

        isShooting = true;
    }
}

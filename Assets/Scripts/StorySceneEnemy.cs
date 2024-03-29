﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneEnemy : MonoBehaviour
{

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 10f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Transform planet;
    ObjectPooler objectPooler;
    bool isFireOn = false;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.ObjectPullerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFireOn)
            CountDownAndShoot();
        //projectile.SetActive(false);
    }

    private void CountDownAndShoot()
    {
        shotCounter = shotCounter - Time.deltaTime;

        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        //GameObject laser = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        //laser.transform.localRotation = Quaternion.identity;
        GameObject laser = objectPooler.SpawnFromPool(projectile.ToString(), transform.position, transform.rotation);

        //laser.GetComponent<Rigidbody2D>().velocity = new Vector2(2*transform.forward.x, -2*transform.forward.z);

        Vector2 direction = planet.position - transform.position;

        direction.Normalize();

        laser.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        AudioManager.instance.play(AllStringConstants.ENEMY_LASER_1, false, true);
    }

    public void TurnOffFiring()
    {
        isFireOn = false;
    }
    public void TurnOnFiring()
    {
        isFireOn = true;
    }
}

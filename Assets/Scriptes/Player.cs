﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Configuration Parameters
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health;
    [SerializeField] AudioClip deathSEX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.05f;
    [SerializeField] float offsetFromY = 1.1f;


    Coroutine fireCouritine;
    GameSession gameSession;
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        gameSession = FindObjectOfType<GameSession>();
        health = gameSession.GetHealth();
      
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           fireCouritine = StartCoroutine(FireContinously());   
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCouritine);
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y+offsetFromY, 0), Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = Mathf.Clamp( transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health = health - damageDealer.GetDamage();
        damageDealer.Hit();
        gameSession.DecreaseHealth();
        health = gameSession.GetHealth();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<LevelLoader>().LoadLooseScene();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSEX, Camera.main.transform.position, deathSFXVolume);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}

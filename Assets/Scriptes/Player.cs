using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Configuration Parameters
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 1f;
     int health;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    float rateOfFire = 0.3f;
    float rateOfFirePointer;



    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.05f;
    [SerializeField] float offsetFromY = 1.1f;

    [Header("Panels")]
    [SerializeField] GameObject GameOverPanel;


    audio_Manager myAudioManager;
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
        FindObjectOfType<HealthBar>().setMaxHealth(3);
        gameSession = FindObjectOfType<GameSession>();
        myAudioManager = FindObjectOfType<audio_Manager>();
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
        if (Time.time > rateOfFirePointer)
        {
            GameObject laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + offsetFromY, 0), Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            myAudioManager.play("PlayerShootSFX");
            rateOfFirePointer = Time.time + rateOfFire;
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
        //FindObjectOfType<LevelLoader>().LoadLooseScene();
        Destroy(gameObject);
        GameObject explsion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explsion, durationOfExplosion);
        myAudioManager.play("PlayerDeathSFX");
        GameOverPanel.SetActive(true);
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

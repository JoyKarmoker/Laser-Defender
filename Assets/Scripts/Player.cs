using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Configuration Parameters
    [Header("Player")]
    public Sprite[] playerSpriteArray;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 2f;
     int health;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] int xpCapsuleToNextLevel = 2; //Numbers of Xp Capsule Needed for the player to go next level 
    private int XpCapsuleEatenByPlayer = 0;
    //float rateOfFire = 0.3f;
    //float rateOfFirePointer;



    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] float offsetFromY = 1.1f;

    [Header("Panels")]
    [SerializeField] GameObject GameOverPanel;


    ObjectPooler objectPooler;
    audio_Manager myAudioManager;
    Coroutine fireCouritine;
    GameSession gameSession;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    private SpriteRenderer spriteRenderer;
    int currentSpriteIndex = 0;
    int playerSpriteArraySize = 0;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        FindObjectOfType<HealthBar>().setMaxHealth(3);
        gameSession = FindObjectOfType<GameSession>();
        myAudioManager = FindObjectOfType<audio_Manager>();
        health = gameSession.GetHealth();
        objectPooler = ObjectPooler.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteArraySize = playerSpriteArray.Length;
        spriteRenderer.sprite = playerSpriteArray[currentSpriteIndex];
        XpCapsuleEatenByPlayer = 0;
      
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
            fireCouritine = StartCoroutine(FireCountinously());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCouritine);
        }

    }

    IEnumerator FireCountinously()
    {
        while(true)
        {
            //GameObject laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + offsetFromY, 0), Quaternion.identity) as GameObject;
            GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(transform.position.x, transform.position.y+offsetFromY), Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            myAudioManager.play("PlayerShootSFX");
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
        //FindObjectOfType<LevelLoader>().LoadLooseScene();
        Destroy(gameObject);
        GameObject explsion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explsion, durationOfExplosion);
        myAudioManager.play("PlayerDeathSFX");
        GameOverPanel.SetActive(true);
    }
    
    /*
    This Method is called by  the Xp Capsule When it hits the player
     if the number of xp capule eaten by player is more than the numbers of capsule needed to go to next level
     Player goes to next level
     */
    public void XpCapsuleEaten()
    {
        XpCapsuleEatenByPlayer = XpCapsuleEatenByPlayer + 1;
        if(XpCapsuleEatenByPlayer >= xpCapsuleToNextLevel)
        {
            
            if(HasNextSprite())
            {
                XpCapsuleEatenByPlayer = 0; //Now the capsule is eaten by player is 0, so it can restrat to calute when to go next level
                MoveToNextSprite();
            }
        }
    }

    public bool HasNextSprite()
    {
        if(currentSpriteIndex < (playerSpriteArraySize-1))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void MoveToNextSprite()
    {
        currentSpriteIndex = currentSpriteIndex+1;
        spriteRenderer.sprite  = playerSpriteArray[currentSpriteIndex];
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

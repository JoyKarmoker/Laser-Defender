﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public enum PlayerStates
    {
        FLY_TO_POS, //First State
        MOVEANDFIRE,//Second State
    }
    //Configuration Parameters
    [Header("Player")]   
    [SerializeField] Color flashColor;
    [SerializeField] Color spriteChangeColor;
    [SerializeField] int playerShipNumber;

    [HideInInspector] public PlayerStates playerStates;
    Vector2 destinationPos;
    [SerializeField] GameObject playerShipTwo;
    [SerializeField] GameObject playerShipThree;
    [SerializeField] float destinationPosY = -7f;
    [SerializeField] float playerInSpeed = 3f;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerTouchSpeed = 0.01f;
    [Tooltip("The amount of how many lvls does the ship has")]
    [SerializeField] int playerShipLevels;
    [SerializeField] float padding = 2f;    

    int health;

    [Header("Capsule Requirements")]
    [SerializeField] float minTimeShootingOff = 2f;
    [SerializeField] float maxTimeShootingOff = 10f;
    [SerializeField] float secProtectionCapsuleLasts = 3f;
    [SerializeField] int xpCapsuleToLeveldown = 5;
    [SerializeField] float minTimeHomingMissileLasts = 2f;
    [SerializeField] float maxTimeHomingMissileLasts = 10f;
    [SerializeField] float HomingMissileFiringPeriod = 0.5f;
    [SerializeField] GameObject HomingMissilePrefab;
    [SerializeField] float HomingMissileOffsetFromY = 1.1f;
    [SerializeField] Slider BelowBar;
    [SerializeField] Slider UpperBar;
    Slider currentWorkingBar;

    int maxValueOfBothSlider = 1;
    int xpCapsuleToNextLevel;    //Numbers of Xp Capsule Needed for the player to go next level 
    private int XpDownCapsuleEatenByPlayer = 0;
    private int xpCapsuleToLevelDownEatenByPlayer = 0;
    private bool protectionCapsuleEaten = false; // This is will be true if player eats protection capsule and after protection capsule effect is finished it will be false again
    bool normalFiringOff = false;
    bool HomingMissileOn = false;
    private Touch touch;

     [Header("UI Section")]
     //[SerializeField] GameObject xpSliders;
     Slider xpSlider1, xpSlider2;
     [SerializeField] GameObject resurrectionPanel;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    GameObject longLaserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] float offsetFromY = 1.1f;
    [SerializeField] float laserLastingTime = 5f;
    float laserTime;
    bool isLaserActive = false;

    [Header("Cinemachine management")]
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;

    PlayerBulletSpawner playerBulletSpawner;
    PlayerShipTwoBulletSpawner playershipTwoBulletSpawner;
    ObjectPooler objectPooler;
    SpriteFlash spriteFlash;
    Coroutine fireCouritine;
    GameSession gameSession;
    Animator animator;
    Rigidbody2D rb;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    int playerCurrentShipLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        //playerCurrentShipLevel = 1;
        playerStates = PlayerStates.FLY_TO_POS;
        destinationPos = new Vector2(0, destinationPosY);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteFlash = GetComponent<SpriteFlash>();

        SetUpMoveBoundaries();

        gameSession = FindObjectOfType<GameSession>();

        health = gameSession.GetHealth();
        objectPooler = ObjectPooler.ObjectPullerInstance;

        playerBulletSpawner = PlayerBulletSpawner.playerBulletSpawnerInstance;
        playershipTwoBulletSpawner = PlayerShipTwoBulletSpawner.playershipTwoBulletSpawnerInstance;
        xpCapsuleToLevelDownEatenByPlayer = 0;
        XpDownCapsuleEatenByPlayer = 0;
        protectionCapsuleEaten = false;
        normalFiringOff = false;
        HomingMissileOn = false;
        laserTime = laserLastingTime;
        longLaserPrefab = transform.GetChild(1).gameObject;
        xpCapsuleToNextLevel = maxValueOfBothSlider;
        currentWorkingBar = BelowBar;
        //xpSlider1 = xpSliders.transform.GetChild(0).GetComponent<Slider>();
        //xpSlider2 = xpSliders.transform.GetChild(1).GetComponent<Slider>();
      
    }

    // Update is called once per frame
    void Update()
    {

        switch(playerStates)
        {
            case PlayerStates.FLY_TO_POS:
                SafeForSeconds(secProtectionCapsuleLasts);
                GoToPoint(destinationPos);
                break;
            case PlayerStates.MOVEANDFIRE:
                Move();
                Fire();
                ChangePlayer();
                break;
        }
       // Move();
       // Fire();
    }

    //handles laser
    /*private void FixedUpdate()
    {
        if (isLaserActive)
            LaserLastingCounter();
    }*/

    void GoToPoint(Vector2 destinationPoint)
    {
        var distance = Vector2.Distance(transform.position, destinationPoint);
        if (distance >= 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinationPoint, playerInSpeed * Time.deltaTime);
        }
        else
        {
            playerStates = PlayerStates.MOVEANDFIRE;
        }
    }

    private void ChangePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 was pressed.");
            Instantiate(playerShipTwo);
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 was pressed.");
            Instantiate(playerShipThree);
            Destroy(this.gameObject);
        }
    }
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1") && !normalFiringOff)
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
        if (playerShipNumber == 1)
        {
            while (true)
            {
                playerBulletSpawner.SpawnBullet(this.gameObject.transform, playerCurrentShipLevel);
                if (playerCurrentShipLevel == 10 || playerCurrentShipLevel == 9)
                {
                    playerBulletSpawner.SpawnBot(transform);
                }
                /*GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(transform.position.x, transform.position.y+offsetFromY), Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);*/
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }
        else if (playerShipNumber == 2)
        {
            while (true)
            {
                playershipTwoBulletSpawner.SpawnBullet(this.gameObject.transform, playerCurrentShipLevel);
                if (playerCurrentShipLevel == 10)
                {
                    playershipTwoBulletSpawner.SpawnBot(transform);
                }

                /*GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(transform.position.x, transform.position.y+offsetFromY), Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);*/
                yield return new WaitForSeconds(projectileFiringPeriod);

            }
        }
    }


    private void Move()
    {

        //Button Input
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = Mathf.Clamp( transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

        //Touch Input
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(transform.position.x + touch.deltaPosition.x * playerTouchSpeed, transform.position.y + touch.deltaPosition.y* playerTouchSpeed);
            }
        }

    }


    #region Capsule section

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (damageDealer) //If player collides with anything that has damageDealer on it like enemy, laser or damage capsule
        {
            ProcessHit(damageDealer);
        }
        else if(other.tag == "HomingMissileCapsule")
        {
            Debug.Log("Homing Missile Hit");
            other.gameObject.SetActive(false);
            HomingMissileCapsuleEaten();

        }
        else if(other.tag == "PlayerShootingOffCapsule")
        {
            Debug.Log("Player Shooting Off Capsule Hit");
            other.gameObject.SetActive(false);
            float shootingOffTime = UnityEngine.Random.Range(minTimeShootingOff, maxTimeShootingOff);
            OffPlayerNormalShooting(shootingOffTime);
        }
        else if(other.tag == "XPDecreasingCapsule")
        {
            Debug.Log("Xp Decreasing Capsule Hit");
            XpDownCapsuleEaten();
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "LevelDownCapsule")
        {
            Debug.Log("Level Down capsule hit");
            if (playerCurrentShipLevel > 1) //If player can go previous level
            {
                MoveToPreviousLevel();
            }
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LaserBeamCapsule") //If player Collides with Laser Beam Capsule
        {
            normalFiringOff = true; //Turning normal firing off for a specific time
            Debug.Log("Laser Capsule Hit");
            other.gameObject.SetActive(false);
            //StopCoroutine(fireCouritine);

            //Activate laser
            longLaserPrefab.SetActive(true);
            isLaserActive = true;
            // laser can destroy every thing mind it
            //run a timer for laser
            StartCoroutine(LaserLastingCounter());
            
        }

        else if(other.tag == "XPCapsule") //If player Collides with Xp Cpsule
        {
            Debug.Log("Xp Capsule Hit");
            XpCapsuleEaten();
            other.gameObject.SetActive(false);
        }

        else if(other.tag == "ProtectionCapsule") //If player Collides with Protection Cpsule
        {
            Debug.Log("Protection Capsule Hit");
            SafeForSeconds(secProtectionCapsuleLasts);
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "LevelUpPlayerCapsule") //If player Collides with Level Up Player Cpsule
        {
            Debug.Log("Level Up Capsule Hit");
            if (HasNextLvl()) //If there is any next level
            {
                XpDownCapsuleEatenByPlayer = 0;
                MoveToNextLvl();
            }
            other.gameObject.SetActive(false);
        }

        return;
        
    }

    // this method is called when player eats laser beam capsule 
    IEnumerator LaserLastingCounter()
    {
        /*while(true)
        {
            laserLastingTime -= Time.deltaTime;
            if (laserLastingTime <= 0)
            {
                isLaserActive = false;
                laserLastingTime = laserTime;
                longLaserPrefab.SetActive(false);
                break;
            }

        }*/

        StopCoroutine(fireCouritine);
        yield return new WaitForSeconds(laserLastingTime);
        isLaserActive = false;
        normalFiringOff = false;
        longLaserPrefab.SetActive(false);
        

    }

    #endregion

    private void ProcessHit(DamageDealer damageDealer)
    {
        if(!protectionCapsuleEaten) //If there is no effect of protection capsule
        {
            //decrease health
            health = health - damageDealer.GetDamage();
            gameSession.DecreaseHealth();
            health = gameSession.GetHealth();

            if (health <= 0)
            {
                Die();
            }

            //shake screen
            if (health > 0)
                CinemachineShake.Instance.ShakeCamera(shakeIntensity,shakeTime);
            else
                CinemachineShake.Instance.ShakeCamera(shakeIntensity*2, shakeTime*5f);

            //flash sprite
            spriteFlash.Flash(flashColor);

            
        }       
        damageDealer.Hit();
    }

    private void Die()
    {
        float tmpSpeed = playerSpeed;
        ///Destroy(Game Object);
        animator.SetBool("Dead",true);
        /*
            This is for watching ads if the user sees add the game will resume from where it left off
            When the player dies set the collider to false and set the player speed to 0 and set normalFiringoff to true;
            if player watches the ad set the collider to true and set the player speed to where it was that means
            set the player speed to tmp spped and make normalFiringOff to false
        */
                playerSpeed = 0f;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        normalFiringOff = true;

    }
    public void OpenRessurectionPanel()
    {
        resurrectionPanel.SetActive(true);
        //Time.timeScale = 0f;
    }
    
    /*
        This Method is called when Xp Capsule hits the player.
        if the number of xp capule eaten by player is more than the numbers of capsule needed to go to next level
        Player goes to next level
     */
    public void XpCapsuleEaten()
    {
        Debug.Log("XP Capsule eaten by player");
        //XpCapsuleEatenByPlayer = XpCapsuleEatenByPlayer + 1;
        currentWorkingBar.value = (currentWorkingBar.value + 1);
        if (currentWorkingBar.value >= maxValueOfBothSlider)
        {
            
            if(HasNextLvl())
            {
                //XpCapsuleEatenByPlayer = 0; //Now the capsule is eaten by player is 0, so it can restrat to calute when to go next level
                MoveToNextLvl();
            }
        }
    }

    /*
    This Method is called when Xp Down Capsule hits the player.
    if the number of xp capule eaten by player is more than the numbers of capsule needed to go to  level down
    Player goes to previous level
 */
    public void XpDownCapsuleEaten()
    {
        if(playerCurrentShipLevel > 1)
        {
            //XpCapsuleEatenByPlayer = XpCapsuleEatenByPlayer - 1;
            XpDownCapsuleEatenByPlayer = XpDownCapsuleEatenByPlayer + 1;
            currentWorkingBar.value = currentWorkingBar.value - 1;
            if (XpDownCapsuleEatenByPlayer >= maxValueOfBothSlider) //If the bar reaches 0 that means it have to go the previous level
            {
                MoveToPreviousLevel();
                XpDownCapsuleEatenByPlayer = 0;
                
            }
        }

    }

    /*
    This Method is when  protection Capsule hits the player;
    When this method is called Player will be safe and wont take any damage for time passed
    in the argument   
    Here some vfx should be added to show that it is safe
    */
    public void SafeForSeconds(float time)
    {
        //Have to turn off taking damage for the given time in argument
        protectionCapsuleEaten = true;
        animator.SetBool("Protection On", true);
        Coroutine protectionCoroutine = StartCoroutine(ProtectionOnForPlayer(time));       
    }

    IEnumerator ProtectionOnForPlayer(float time)
    {
       
        yield return new WaitForSeconds(time);
        protectionCapsuleEaten = false;
        animator.SetBool("Protection On", false);
        
    }

    public bool HasNextLvl()
    {
        if(playerCurrentShipLevel < playerShipLevels)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void MoveToNextLvl()
    {
        //update current ship level
        playerCurrentShipLevel++;

        ManageXPBar(playerCurrentShipLevel);

        //lvl up in animator
        animator.SetInteger("Ship Level", playerCurrentShipLevel);

        // enable protection for 2 and half seconds
        SafeForSeconds(2.5f);

        //flash sprite
        spriteFlash.Flash(spriteChangeColor);
       
    }

    private void MoveToPreviousLevel()
    {
        //Set the animator to go previous level

        //update current ship level
        playerCurrentShipLevel--;

        ManageXPBar(playerCurrentShipLevel);
        
        //lvl up in animator
        animator.SetInteger("Ship Level", playerCurrentShipLevel);

        // enable protection for 2 and half seconds
        SafeForSeconds(2.5f);

        //flash sprite
        spriteFlash.Flash(spriteChangeColor);
    }

    private void ManageXPBar(int playerCurrentShipLevel)
    {
        //Updating the xpbar for current Level
        float exp = Mathf.Ceil(playerCurrentShipLevel / 2.0f);
        maxValueOfBothSlider = (int)Mathf.Pow(2, (exp - 1));
        Debug.Log("Maxium value of sliders " + maxValueOfBothSlider);
        BelowBar.maxValue = maxValueOfBothSlider;
        UpperBar.maxValue = maxValueOfBothSlider;

        if (playerCurrentShipLevel % 2 == 0) //If player is in even level like 2, 4, 6, 8, 10
        {
            BelowBar.value = maxValueOfBothSlider;
            UpperBar.value = UpperBar.minValue;
            currentWorkingBar = UpperBar;
        }

        else //if Player is in odd level 1, 3, 5, 7, 9
        {
            UpperBar.value = 0;
            BelowBar.value = BelowBar.minValue;
            currentWorkingBar = BelowBar;
        }
        //xpCapsuleToNextLevel = maxValueOfBothSlider;
    }
    void OffPlayerNormalShooting(float shootingOffTime)
    {
        normalFiringOff = true;
        StartCoroutine(PlayerShootingOff(shootingOffTime));
        
    }
    IEnumerator PlayerShootingOff(float shootingOffTime)
    {
        StopCoroutine(fireCouritine);
        yield return new WaitForSeconds(shootingOffTime);
        normalFiringOff = false;
    }

    void HomingMissileCapsuleEaten()
    {
        
        /*
            Now for HomingMissileLasts seconds Normal shooting for player will off and player will instantite 
            homing missile prefab which will have homing behaviour attached with it
            After HomingMissileLasts seconds,  homing missile will off and normal firing wil start;
         
        */
        //Stopping normal Shotting for player by calling themethod
        if(!normalFiringOff) //If only normal firing is off to check we dont call the method while it is running
        {
            StartHomingMissile();
        }
    }
    void StartHomingMissile()
    {
        normalFiringOff = true;
        //Debug.Log("Homing Firing Started");
        StartCoroutine(FireHomingMissile()); //Start Homing Missile
        StartCoroutine(StopNormalFiring()); //Stop Normal Firing

    }

    IEnumerator StopNormalFiring()
    {
        float HomingMissileLasts = UnityEngine.Random.Range(minTimeHomingMissileLasts, maxTimeHomingMissileLasts);
        //Debug.Log("Homing Missile Lasts for " + HomingMissileLasts + " second");
        StopCoroutine(fireCouritine);
        yield return new WaitForSeconds(HomingMissileLasts);
        StopCoroutine(FireHomingMissile());
        normalFiringOff = false;
        //Debug.Log("Homing Fire Off");
    }

    IEnumerator FireHomingMissile()
    {
        while(normalFiringOff)
        {
            //Debug.Log("Fire");
            GameObject HomingMissile = objectPooler.SpawnFromPool(HomingMissilePrefab.ToString(), new Vector2(transform.position.x, transform.position.y + HomingMissileOffsetFromY), Quaternion.identity);

           // HomingMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(HomingMissileFiringPeriod);
        }

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

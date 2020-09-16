using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipTwo : MonoBehaviour
{
    [Header("Player")]

    [SerializeField] Color flashColor;
    [SerializeField] Color spriteChangeColor;

    [SerializeField] float playerSpeed = 10f;
    [Tooltip("The amount of how many lvls does the ship has")]
    [SerializeField] float playerShipLevels;
    [SerializeField] float padding = 2f;

    int health;

    [Header("Capsule Requirements")]
    [SerializeField] float minTimeShootingOff = 2f;
    [SerializeField] float maxTimeShootingOff = 10f;
    [SerializeField] float secProtectionCapsuleLasts;
    [SerializeField] int xpCapsuleToLeveldown = 5;
    [SerializeField] float minTimeHomingMissileLasts = 2f;
    [SerializeField] float maxTimeHomingMissileLasts = 10f;
    [SerializeField] float homingMissileFiringPeriod = 0.5f;
    [SerializeField] GameObject homingMissilePrefab;
    [SerializeField] float homingMissileOffsetFromY = 1.1f;

    int xpCapsuleToNextLevel = 5;    //Numbers of Xp Capsule Needed for the player to go next level 
    private int XpCapsuleEatenByPlayer = 0;
    private int xpCapsuleToLevelDownEatenByPlayer = 0;
    private bool protectionCapsuleEaten = false; // This is will be true if player eats protection capsule and after protection capsule effect is finished it will be false again
    bool normalFiringOff = false;
    bool homingMissileOn = false;



    //TODO:
    /* [Header("UI Section")]
     [SerializeField] Slider xpSlider;*/

    [Header("Projectile")]
    [SerializeField] GameObject longLaserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.15f;
    [SerializeField] float offsetFromY = 1.1f;
    [SerializeField] float laserLastingTime = 5f;
    float laserTime;
    bool isLaserActive = false;

    [Header("Cinemachine management")]
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;

    PlayerShipTwoBulletSpawner playershipTwoBulletSpawner;
    //ObjectPooler objectPooler;
    SpriteFlash spriteFlash;
    audio_Manager myAudioManager;
    Coroutine fireCouritine;
    GameSession gameSession;
    Animator animator;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    int playerCurrentShipLevel = 10;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteFlash = GetComponent<SpriteFlash>();

        SetUpMoveBoundaries();

        gameSession = FindObjectOfType<GameSession>();
        myAudioManager = FindObjectOfType<audio_Manager>();

        health = gameSession.GetHealth();
        //objectPooler = ObjectPooler.ObjectPullerInstance;
        playershipTwoBulletSpawner = PlayerShipTwoBulletSpawner.playershipTwoBulletSpawnerInstance;

        xpCapsuleToLevelDownEatenByPlayer = 0;
        XpCapsuleEatenByPlayer = 0;
        protectionCapsuleEaten = false;
        normalFiringOff = false;
        homingMissileOn = false;
        laserTime = laserLastingTime;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    //handles laser
    private void FixedUpdate()
    {
        if (isLaserActive)
            LaserLastingCounter();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !normalFiringOff)
        {
            fireCouritine = StartCoroutine(FireCountinously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCouritine);
        }

    }

    IEnumerator FireCountinously()
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
            myAudioManager.play("PlayerShootSFX"); //The shotting sfx should be handeled in player bulle
            yield return new WaitForSeconds(projectileFiringPeriod);

        }

    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    #region Capsule section

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (damageDealer) //If player collides with anything that has damageDealer on it like enemy, laser or damage capsule
        {
            ProcessHit(damageDealer);
        }
        else if (other.tag == "HomingMissileCapsule")
        {
            Debug.Log("Homing Missile Hit");
            other.gameObject.SetActive(false);
            HomingMissileCapsuleEaten();

        }
        else if (other.tag == "PlayerShootingOffCapsule")
        {
            Debug.Log("Player Shooting Off Capsule Hit");
            other.gameObject.SetActive(false);
            float shootingOffTime = Random.Range(minTimeShootingOff, maxTimeShootingOff);
            OffPlayerNormalShooting(shootingOffTime);
        }
        else if (other.tag == "XPDecreasingCapsule")
        {
            Debug.Log("Xp Decreasing Capsule Hit");
            XpDownCapsuleEaten();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LevelDownCapsule")
        {
            Debug.Log("Level Down capsule hit");
            if (playerCurrentShipLevel > 0) //If player can go previous level
            {
                MoveToPreviousLevel();
            }
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LaserBeamCapsule") //If player Collides with Laser Beam Capsule
        {
            Debug.Log("Laser Capsule Hit");
            other.gameObject.SetActive(false);

            //TODO:
            //Activate laser
            //         laser can destroy every thing mind it
            //run a timer for laser
            longLaserPrefab.SetActive(true);
            isLaserActive = true;
        }

        else if (other.tag == "XPCapsule") //If player Collides with Xp Cpsule
        {
            Debug.Log("Xp Capsule Hit");
            XpCapsuleEaten();
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "ProtectionCapsule") //If player Collides with Protection Cpsule
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
                XpCapsuleEatenByPlayer = 0;
                //xpSlider.value = XpCapsuleEatenByPlayer;
                MoveToNextLvl();
            }
            other.gameObject.SetActive(false);
        }

        return;

    }

    // this method is called when player eats laser beam capsule 
    private void LaserLastingCounter()
    {
        laserLastingTime -= Time.deltaTime;

        if (laserLastingTime <= 0)
        {
            isLaserActive = false;
            laserLastingTime = laserTime;
            longLaserPrefab.SetActive(false);
        }
    }

    #endregion

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (!protectionCapsuleEaten) //If there is no effect of protection capsule
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
                CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
            else
                CinemachineShake.Instance.ShakeCamera(shakeIntensity * 2, shakeTime * 5f);

            //flash sprite
            spriteFlash.Flash(flashColor);


        }
        damageDealer.Hit();
    }

    private void Die()
    {
        float tmpSpeed = playerSpeed;
        ///Destroy(Game Object);
        animator.SetBool("Dead", true);
        /*
            This is for watching ads if the user sees add the game will resume from where it left off
            When the player dies set the collider to false and set the player speed to 0 and set normalFiringoff to true;
            if player watches the ad set the collider to true and set the player speed to where it was that means
            set the player speed to tmp spped and make normalFiringOff to false
        */
        playerSpeed = 0f;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        normalFiringOff = true;
        myAudioManager.play("PlayerDeathSFX");

    }

    /*
        This Method is called when Xp Capsule hits the player.
        if the number of xp capule eaten by player is more than the numbers of capsule needed to go to next level
        Player goes to next level
     */
    public void XpCapsuleEaten()
    {
        XpCapsuleEatenByPlayer = XpCapsuleEatenByPlayer + 1;
        //TODO:xpSlider.value = XpCapsuleEatenByPlayer;
        if (XpCapsuleEatenByPlayer >= xpCapsuleToNextLevel)
        {

            if (HasNextLvl())
            {
                XpCapsuleEatenByPlayer = 0; //Now the capsule is eaten by player is 0, so it can restrat to calute when to go next level
                //TODO:xpSlider.value = XpCapsuleEatenByPlayer;
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
        xpCapsuleToLevelDownEatenByPlayer = xpCapsuleToLevelDownEatenByPlayer + 1;
        //TODO:xpSlider.value = XpCapsuleEatenByPlayer;
        if (xpCapsuleToLevelDownEatenByPlayer >= xpCapsuleToLeveldown)
        {
            if (playerCurrentShipLevel > 0) //If player can go previous level
            {
                xpCapsuleToLevelDownEatenByPlayer = 0; //Now the capsule is eaten by player is 0, so it can restrat to calute when to go next level
                //TODO:xpSlider.value = XpCapsuleEatenByPlayer;
                MoveToPreviousLevel();
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
        //protectionLayer.SetActive(true);
        animator.SetBool("Protection On", true);
        Coroutine protectionCoroutine = StartCoroutine(ProtectionOnForPlayer(time));
        // StopCoroutine(protectionCoroutine);


    }

    IEnumerator ProtectionOnForPlayer(float time)
    {

        yield return new WaitForSeconds(time);
        protectionCapsuleEaten = false;
        animator.SetBool("Protection On", false);
        //protectionLayer.SetActive(false);

    }

    public bool HasNextLvl()
    {
        if (playerCurrentShipLevel < playerShipLevels)
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
        playerCurrentShipLevel--;
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
            Now for homingMissileLasts seconds Normal shooting for player will off and player will instantite 
            homing missile prefab which will have homing behaviour attached with it
            After homingMissileLasts seconds,  homing missile will off and normal firing wil start;
         
        */
        //Stopping normal Shotting for player by calling themethod
        if (!normalFiringOff) //If only normal firing is off to check we dont call the method while it is running
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
        float homingMissileLasts = Random.Range(minTimeHomingMissileLasts, maxTimeHomingMissileLasts);
        //Debug.Log("Homing Missile Lasts for " + homingMissileLasts + " second");
        StopCoroutine(fireCouritine);
        yield return new WaitForSeconds(homingMissileLasts);
        StopCoroutine(FireHomingMissile());
        normalFiringOff = false;
        //Debug.Log("Homing Fire Off");
    }

    IEnumerator FireHomingMissile()
    {
        while (normalFiringOff)
        {
            //Debug.Log("Fire");
            GameObject homingMissile = Instantiate(homingMissilePrefab, new Vector2(transform.position.x, transform.position.y + homingMissileOffsetFromY), Quaternion.identity);
            // homingMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            myAudioManager.play("PlayerShootSFX");
            yield return new WaitForSeconds(homingMissileFiringPeriod);
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

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private Touch touch;
    [Tooltip("The amount of how many lvls does the ship has")]
    [SerializeField] int playerShipLevels;
    [SerializeField] float padding = 2f;

    int health;

    [Header("Capsule Requirements")]
    [SerializeField] float minTimeShootingOff = 2f;
    [SerializeField] float maxTimeShootingOff = 10f;

    [SerializeField] float secProtectionCapsuleLasts = 3f;

    [SerializeField] float minTimeHomingMissileLasts = 2f;
    [SerializeField] float maxTimeHomingMissileLasts = 10f;
    [SerializeField] float HomingMissileFiringPeriod = 0.5f;
    [SerializeField] GameObject HomingMissilePrefab;
    [SerializeField] float HomingMissileOffsetFromY = 1.1f;

    int currentXPValue;
    int xpValueNeededForNextLevel;    //Numbers of Xp Capsule Needed for the player to go next level 

    private bool protectionCapsuleEaten = false; // This is will be true if player eats protection capsule and after protection capsule effect is finished it will be false again

    bool normalFiringOff = false;

    bool HomingMissileOn = false;

    [Header("UI Section")]
    [SerializeField] Slider BelowBar;
    [SerializeField] Slider UpperBar;

    Color UpperBarFillColor;
    Color BelowBarFillColor;

    Image UpperBarFillImage;
    Image BelowBarFillImage;

    //whitish
    Color32 UpperBar_UpgradeColor = new Color32(255,255,255,255);
    Color32 BelowBar_UpgradeColor = new Color32(255, 255, 255, 255);

    // redish
    Color32 UpperBar_DowngradeColor = new Color32(250, 40, 40, 255);
    Color32 BelowBar_DowngradeColor = new Color32(250, 40, 40, 255);

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

        protectionCapsuleEaten = false;
        normalFiringOff = false;
        HomingMissileOn = false;
        laserTime = laserLastingTime;
        longLaserPrefab = transform.GetChild(1).gameObject;


        #region XP Start Section

        UpperBar.maxValue = 2;
        BelowBar.maxValue = 2;
        BelowBar.value = 1;
        UpperBar.value = 0;

        UpperBarFillImage = UpperBar.transform.GetChild(0).GetComponent<Image>();
        BelowBarFillImage = BelowBar.transform.GetChild(0).GetComponent<Image>();

        UpperBarFillColor = UpperBarFillImage.color;
        BelowBarFillColor = BelowBarFillImage.color;

        #endregion

    }

    // Update is called once per frame
    void Update()
    {

        switch (playerStates)
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
            Instantiate(playerShipTwo);
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(playerShipThree);
            Destroy(this.gameObject);
        }
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
        //button input..
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

        //Touch Input
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector2(transform.position.x + touch.deltaPosition.x * playerTouchSpeed, transform.position.y + touch.deltaPosition.y * playerTouchSpeed);
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
        else if (other.tag == "HomingMissileCapsule")
        {
            other.gameObject.SetActive(false);
            HomingMissileCapsuleEaten();

        }
        else if (other.tag == "PlayerShootingOffCapsule")
        {
            other.gameObject.SetActive(false);
            float shootingOffTime = UnityEngine.Random.Range(minTimeShootingOff, maxTimeShootingOff);
            OffPlayerNormalShooting(shootingOffTime);
        }
       
        else if (other.tag == "LaserBeamCapsule") //If player Collides with Laser Beam Capsule
        {
            normalFiringOff = true; //Turning normal firing off for a specific time
            other.gameObject.SetActive(false);
            //StopCoroutine(fireCouritine);

            //Activate laser
            longLaserPrefab.SetActive(true);
            isLaserActive = true;
            // laser can destroy every thing mind it
            //run a timer for laser
            StartCoroutine(LaserLastingCounter());

        }

        else if (other.tag == "XPCapsule") //If player Collides with Xp Cpsule
        {
            XpCapsuleEaten();
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "XPDecreasingCapsule")
        {
            XpDownCapsuleEaten();
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "LevelDownCapsule")
        {
            LevelDownCapsuleEaten();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "LevelUpPlayerCapsule") //If player Collides with Level Up Player Cpsule
        {
            LevelUpCapsuleEaten();
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "ProtectionCapsule") //If player Collides with Protection Cpsule
        {
            SafeForSeconds(secProtectionCapsuleLasts);
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
        if (playerCurrentShipLevel < playerShipLevels) // looks for if ship is upgeadable or not.. 
        {
            currentXPValue += 1; // increase xp by one value

            int nextShipLevel = playerCurrentShipLevel + 1;

            xpValueNeededForNextLevel = nextShipLevel - ((nextShipLevel / 2) + (nextShipLevel % 2)); // that much xp is needed for next level..

            StartCoroutine(ManageXPBar(true));

            if (currentXPValue >= xpValueNeededForNextLevel)
            {
                if (HasNextLvl())
                {
                    MoveToNext_OR_PreviousLvl(true);
                }
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
        if (playerCurrentShipLevel > 1)
        {
            currentXPValue -= 1;

            xpValueNeededForNextLevel = playerCurrentShipLevel - ((playerCurrentShipLevel / 2) + (playerCurrentShipLevel % 2));

            StartCoroutine(ManageXPBar(false));
            
            if (currentXPValue < 0)
            {
                MoveToNext_OR_PreviousLvl(false);

                currentXPValue = xpValueNeededForNextLevel - 1;
            }

        }


    }

    IEnumerator ManageXPBar(bool doIncrement) // doIncrement true means xp up capsule eaten. else xp down
    {
        if (doIncrement)
        {
            // if current Ship level is odd then, update lower xp bar with currentXpValue 

            if ((playerCurrentShipLevel ) % 2 != 0)
            {
                // scale up and down a bit.. 
                BelowBar.transform.DORewind();
                BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .25f, 5, 0.3f);

                // applies hit color effect
                BelowBarFillImage.color = BelowBar_UpgradeColor;

                float currentTime = 0f;
                float currentBarValue = BelowBar.value;

                while (currentTime < 0.3f)
                {
                    currentTime += Time.deltaTime;

                    // updates xp bar value
                    BelowBar.value = Mathf.Clamp01( (currentTime/0.3f) ) + currentBarValue;

                    //backs to original fill color
                    BelowBarFillImage.color = Color.Lerp(BelowBar_UpgradeColor, BelowBarFillColor, (currentTime / 0.3f));

                    yield return null;
                }


                if (BelowBar.value >= BelowBar.maxValue)
                {
                    // scale up and down a bit.. 
                    UpperBar.transform.DORewind();
                    UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .25f, 5, 0.3f);

                    // applies hit color effect
                    UpperBarFillImage.color = UpperBar_UpgradeColor;

                    BelowBar.value = BelowBar.maxValue;
                    UpperBar.value = 0;

                    float tmpCurrentTime = 0f;
                    currentBarValue = UpperBar.value;

                    while (tmpCurrentTime < 0.3f)
                    {
                        tmpCurrentTime += Time.deltaTime;

                        // updates xp bar value
                        UpperBar.value = Mathf.Clamp01((tmpCurrentTime / 0.3f)) + currentBarValue;

                        //backs to original fill color
                        UpperBarFillImage.color = Color.Lerp(UpperBar_UpgradeColor, UpperBarFillColor, (tmpCurrentTime / 0.3f));

                        yield return null;
                    }
                   
                }

            }
            else // else if current Ship level is even then, update upper xp bar with currentXpValue 
            {
                UpperBar.transform.DORewind();
                UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .25f, 5, 0.3f);

                UpperBarFillImage.color = UpperBar_UpgradeColor;

                float currentTime = 0f;
                float currentBarValue = UpperBar.value;

                while (currentTime < 0.3f)
                {
                    currentTime += Time.deltaTime;

                    UpperBar.value = Mathf.Clamp01((currentTime / 0.3f)) + currentBarValue;

                    UpperBarFillImage.color = Color.Lerp(UpperBar_UpgradeColor, UpperBarFillColor, (currentTime / 0.3f));

                    yield return null;
                }
               
                if(UpperBar.value >= UpperBar.maxValue)
                {
                    BelowBar.transform.DORewind();
                    BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .25f, 5, 0.3f);

                    BelowBarFillImage.color = BelowBar_UpgradeColor;

                    UpperBar.maxValue += 1;
                    BelowBar.maxValue += 1;
                    UpperBar.value = 0;
                    BelowBar.value = 0;

                    float tmpCurrentTime = 0f;
                    currentBarValue = BelowBar.value;

                    while (tmpCurrentTime < 0.3f)
                    {
                        tmpCurrentTime += Time.deltaTime;
                        BelowBar.value = Mathf.Clamp01((tmpCurrentTime / 0.3f)) + currentBarValue;

                        BelowBarFillImage.color = Color.Lerp(BelowBar_UpgradeColor, BelowBarFillColor, (tmpCurrentTime / 0.3f));

                        yield return null;
                    }


                }

            }
        }
        else
        {
            // if current Ship level is odd then, update lower xp bar with currentXpValue 

            if ((playerCurrentShipLevel) % 2 != 0)
            {
                BelowBar.transform.DORewind();
                BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .25f, 5, 0.3f);

                BelowBarFillImage.color = BelowBar_DowngradeColor;

                float currentTime = 0f;
                float currentBarValue = BelowBar.value;

                while (currentTime < 0.3f)
                {
                    currentTime += Time.deltaTime;
                    BelowBar.value = currentBarValue - Mathf.Clamp01((currentTime / 0.3f));

                    BelowBarFillImage.color = Color.Lerp(BelowBar_DowngradeColor, BelowBarFillColor, (currentTime / 0.3f));

                    yield return null;
                }
                

                if(BelowBar.value <= 0)
                {
                    UpperBar.transform.DORewind();
                    UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .25f, 5, 0.3f);

                    UpperBarFillImage.color = UpperBar_DowngradeColor;

                    UpperBar.maxValue = xpValueNeededForNextLevel + 1;
                    BelowBar.maxValue = xpValueNeededForNextLevel + 1;
                    UpperBar.value = UpperBar.maxValue;
                    BelowBar.value = BelowBar.maxValue;

                    float tmpCurrentTime = 0f;
                    currentBarValue = UpperBar.value;

                    while (tmpCurrentTime < 0.3f)
                    {
                        tmpCurrentTime += Time.deltaTime;

                        UpperBar.value = currentBarValue - Mathf.Clamp01((tmpCurrentTime / 0.3f));

                        UpperBarFillImage.color = Color.Lerp(UpperBar_DowngradeColor, UpperBarFillColor, (tmpCurrentTime / 0.3f));

                        yield return null;
                    }
                    
                }

            }
            else // else if current Ship level is even then, update upper xp bar with currentXpValue 
            {
                UpperBar.transform.DORewind();
                UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .25f, 5, 0.3f);

                UpperBarFillImage.color = UpperBar_DowngradeColor;

                float currentTime = 0f;
                float currentBarValue = UpperBar.value;

                while (currentTime < 0.3f)
                {
                    currentTime += Time.deltaTime;

                    UpperBar.value = currentBarValue - Mathf.Clamp01((currentTime / 0.3f));

                    UpperBarFillImage.color = Color.Lerp(UpperBar_DowngradeColor, UpperBarFillColor, (currentTime / 0.3f));

                    yield return null;
                }
               

                if(UpperBar.value <= 0)
                {
                    BelowBar.transform.DORewind();
                    BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .25f, 5, 0.3f);

                    BelowBarFillImage.color = BelowBar_DowngradeColor;

                    UpperBar.value = 0;
                    BelowBar.value = BelowBar.maxValue;

                    float tmpCurrentTime = 0f;
                    currentBarValue = BelowBar.value;

                    while (tmpCurrentTime < 0.3f)
                    {
                        tmpCurrentTime += Time.deltaTime;
                        BelowBar.value = currentBarValue - Mathf.Clamp01((tmpCurrentTime / 0.3f));

                        BelowBarFillImage.color = Color.Lerp(BelowBar_DowngradeColor, BelowBarFillColor, (tmpCurrentTime / 0.3f));

                        yield return null;
                    }
                  
                }

            }
        }
        

    }

    private void LevelUpCapsuleEaten()
    {
        // update ship level
        if (HasNextLvl())
        {
            MoveToNext_OR_PreviousLvl(true);

            currentXPValue = 0;

            int nextShipLevel = playerCurrentShipLevel + 1;

            xpValueNeededForNextLevel = nextShipLevel - ((nextShipLevel / 2) + (nextShipLevel % 2));

            BelowBar.maxValue = xpValueNeededForNextLevel + 1;
            UpperBar.maxValue = xpValueNeededForNextLevel + 1;

            BelowBar.transform.DORewind();
            BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .2f, 7, 0.4f);

            UpperBar.transform.DORewind();
            UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .2f, 7, 0.4f);

            BelowBarFillImage.color = BelowBar_UpgradeColor;
            UpperBarFillImage.color = UpperBar_UpgradeColor;

            StartCoroutine(ApplyXPEffectForLevelUPDownCapsule(BelowBar_UpgradeColor, UpperBar_UpgradeColor));

            if (playerCurrentShipLevel % 2 == 0)
            {
                BelowBar.value = BelowBar.maxValue;
                UpperBar.value = 1;
            }
            else
            {
                UpperBar.value = 0;
                BelowBar.value = 1;
            }
        }

        
    }

    private void LevelDownCapsuleEaten()
    {

        xpValueNeededForNextLevel = playerCurrentShipLevel - ((playerCurrentShipLevel / 2) + (playerCurrentShipLevel % 2));

        if (playerCurrentShipLevel > 1)
        {
            // update ship level
            MoveToNext_OR_PreviousLvl(false);

            BelowBar.maxValue = xpValueNeededForNextLevel + 1;
            UpperBar.maxValue = xpValueNeededForNextLevel + 1;

            currentXPValue = xpValueNeededForNextLevel - 1;

            BelowBar.transform.DORewind();
            BelowBar.transform.DOPunchScale(new Vector3(-0.05f, 0.5075f, 1), .2f, 7, 0.4f);

            UpperBar.transform.DORewind();
            UpperBar.transform.DOPunchScale(new Vector3(-0.065f, 0.5875f, 1), .2f, 7, 0.4f);

            BelowBarFillImage.color = BelowBar_DowngradeColor;
            UpperBarFillImage.color = UpperBar_DowngradeColor;

            StartCoroutine(ApplyXPEffectForLevelUPDownCapsule(BelowBar_DowngradeColor, UpperBar_DowngradeColor));

            if (playerCurrentShipLevel % 2 == 0)
            {
                BelowBar.value = xpValueNeededForNextLevel + 1;
                UpperBar.value = xpValueNeededForNextLevel;
            }
            else
            {

                UpperBar.value = 0;
                BelowBar.value = xpValueNeededForNextLevel;
            }
        }

        
    }

    IEnumerator ApplyXPEffectForLevelUPDownCapsule(Color up_Down_color_below, Color up_Down_color_upper)
    {
        float currentTime = 0f;

        while (currentTime < 0.2f)
        {
            currentTime += Time.deltaTime;

            //backs to original fill color
            BelowBarFillImage.color = Color.Lerp(up_Down_color_below, BelowBarFillColor, (currentTime / 0.2f));
            UpperBarFillImage.color = Color.Lerp(up_Down_color_upper, UpperBarFillColor, (currentTime / 0.2f));

            yield return null;
        }
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

    public void MoveToNext_OR_PreviousLvl(bool doUpgrage)
    {
        if (doUpgrage)
        {
            //update current ship level
            playerCurrentShipLevel++;

            currentXPValue = 0;
        }
        else
        {
            //update current ship level
            playerCurrentShipLevel--;
        }

        //lvl up in animator
        animator.SetInteger("Ship Level", playerCurrentShipLevel);

        // enable protection for 2 and half seconds
        SafeForSeconds(2.5f);

        //flash sprite
        spriteFlash.Flash(spriteChangeColor);

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
        if (!normalFiringOff) //If only normal firing is off to check we dont call the method while it is running
        {
            StartHomingMissile();
        }
    }
    void StartHomingMissile()
    {
        normalFiringOff = true;
        StartCoroutine(FireHomingMissile()); //Start Homing Missile
        StartCoroutine(StopNormalFiring()); //Stop Normal Firing

    }

    IEnumerator StopNormalFiring()
    {
        float HomingMissileLasts = UnityEngine.Random.Range(minTimeHomingMissileLasts, maxTimeHomingMissileLasts);
        StopCoroutine(fireCouritine);
        yield return new WaitForSeconds(HomingMissileLasts);
        StopCoroutine(FireHomingMissile());
        normalFiringOff = false;
    }
    IEnumerator FireHomingMissile()
    {
        while (normalFiringOff)
        {
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
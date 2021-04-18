using UnityEngine;
using System;
using System.Collections;

public class TypeOneEnemy : MonoBehaviour
{
    [Header("Path Info")]
    [SerializeField] float reachDistance = 0.2f;
    [SerializeField] float rotationOffsetInPath = 90f; //Adjustment for rotation of enemy
    [SerializeField] float rotationOffsetInFormation = -90f; //Adjustment Of enemy rotation in Formation
    [SerializeField] bool useCurvedPath = true;

    float speed = 10f;
    float rotationSpeed = 10f;
    int currentWayPointId = 0;
    float distance; //current Distance to Next Point
    PathEnemy pathToFollow;

    [Header("Enemy")]
    [SerializeField] Color flashColor;
    [SerializeField] float health = 100;
    Animator animator;
    int scoreValue = 20;

    //Enemy  State
    public enum EnemyStates
    {
        FLY_IN, //First State
        TO_FORMATION,//Second State
        IDLE,
        DIVE
    }

    public EnemyStates enemyStates;
    int posInFormation;
    Formation formation;

    /*
    [Header("Projectile")]
    //[SerializeField] GameObject projectile;
    //[SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 10f;
    [SerializeField] float projectileSpeed = 10f;
    */

    [Header("VFX")]
    SpriteFlash spriteFlash;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationofExplotion = 1f;

    //Cached Ref
    GameSession gameSession;
    EnemySpawner enemySpawner;

    ObjectPooler objectPooler;
    CapsuleSpawner capsuleSpawner;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        spriteFlash = GetComponent<SpriteFlash>();
        //shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
        objectPooler = ObjectPooler.ObjectPullerInstance;
        capsuleSpawner = CapsuleSpawner.CapsuleSpawnerInstance;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (enemyStates)
        {
            case EnemyStates.FLY_IN:
                MoveOnPath(pathToFollow);
                break;
            case EnemyStates.TO_FORMATION:
                MoveToFormation();
                break;
            case EnemyStates.IDLE:
                break;

            /*case EnemyStates.DIVE:
                //MoveOnPath(pathToFollow);
                CountDownAndShoot();
                break;*/
        }

    }
    void MoveToFormation()
    {
        // Debug.Log("Pos in Formation " + posInFormation);
        transform.position = Vector2.MoveTowards(transform.position, formation.GetVector(posInFormation), speed * Time.deltaTime);
        //Rotation of enemy
        Vector2 direction = formation.GetVector(posInFormation) - (Vector2)transform.position;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffsetInFormation));
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        //
        if (Vector2.Distance(transform.position, formation.GetVector(posInFormation)) <= 0.0001f)
        {
            transform.SetParent(formation.gameObject.GetComponentInParent<Transform>());
            transform.eulerAngles = Vector2.zero; //Set rotation
            //Setting the spreading configs
            formation.enemyInThisFormation.Add(new Formation.FormationSpread(posInFormation, transform.localPosition.x, transform.localPosition.y, this.gameObject));

            enemyStates = EnemyStates.IDLE;
        }
    }

    void MoveOnPath(PathEnemy path)
    {
        if (useCurvedPath)
        {
            int totalPointsInPath = path.curvedPathPointList.Count;

            distance = Vector2.Distance(path.curvedPathPointList[currentWayPointId], transform.position);
            transform.position = Vector2.MoveTowards(transform.position, path.curvedPathPointList[currentWayPointId], speed * Time.deltaTime);


            //Rotation of enemy
            Vector2 direction = path.curvedPathPointList[currentWayPointId] - (Vector2)transform.position;

            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion target = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffsetInPath));
                transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
            }

            if (distance <= reachDistance)
            {
                currentWayPointId++;
            }

            if (currentWayPointId >= totalPointsInPath)
            {
                currentWayPointId = 0;
                enemyStates = EnemyStates.TO_FORMATION;
            }
        }
        else
        {
            distance = Vector2.Distance(path.straightPathPointList[currentWayPointId].position, transform.position);
            transform.position = Vector2.MoveTowards(transform.position, path.straightPathPointList[currentWayPointId].position, speed * Time.deltaTime);
            int totalPointsInPath = path.straightPathPointList.Count;

            //Rotation of enemy

            Vector2 direction = (Vector2)path.straightPathPointList[currentWayPointId].position - (Vector2)transform.position;

            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion target = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffsetInPath));
                transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
            }

            if (distance <= reachDistance)
            {
                currentWayPointId++;
            }

            if (currentWayPointId >= totalPointsInPath)
            {
                currentWayPointId = 0;
                enemyStates = EnemyStates.TO_FORMATION;
            }
        }
    }


    public void SpawnSetup(PathEnemy path, int pos, Formation formation, float speed, float rotationSpeed)
    {
        pathToFollow = path;
        posInFormation = pos;
        this.formation = formation;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        enemyStates = EnemyStates.FLY_IN;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {

            ProcessHit(damageDealer);
        }

        LaserDamage laserDamage = other.gameObject.GetComponent<LaserDamage>();
        if (laserDamage)
        {
            ProcessLagerHit(laserDamage);
        }

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        //flash sprite
        spriteFlash.Flash(flashColor);

        health = health - damageDealer.GetDamage();

        damageDealer.Hit();
        if (health <= 0)
        {
            //shake screen
            CinemachineShake.Instance.ShakeCamera(3f, 0.2f);
            Die();
        }
    }

    private void ProcessLagerHit(LaserDamage laserDamage)
    {
        StartCoroutine(ContinousDamage(laserDamage));

    }
    IEnumerator ContinousDamage(LaserDamage laserDamage)
    {
        int numberofSecondsforDamage = 10;
        for (int i = 0; i < numberofSecondsforDamage; i++)
        {
            //Debug.Log("Laser Damaging");
            health = health - laserDamage.GetDamage();
            if (health <= 0)
            {
                //shake screen
                CinemachineShake.Instance.ShakeCamera(3f, 0.2f);
                Die();
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
    private void Die()
    {
        //Report to formation to tell it that this enemy is dead
        for (int i = 0; i < formation.enemyInThisFormation.Count; i++)
        {
            if (formation.enemyInThisFormation[i].index == posInFormation)
            {
                formation.enemyInThisFormation.Remove(formation.enemyInThisFormation[i]);
            }
        }

        //Report to spawn Manager to tell that this enemy is dead
        for (int i = 0; i < enemySpawner.spawnedEnemys.Count; i++)
        {
            enemySpawner.spawnedEnemys.Remove(this.gameObject);
        }

        if (enemySpawner.spawnedEnemys.Count == 0)
        {
            //enemySpawner.StartSuperWave();
            float secToWait = enemySpawner.secAfterEnemyStartSpawn; //Time to wait for new super wave spawn
            enemySpawner.inFormation = false;
            enemySpawner.Invoke("StartSuperWave", secToWait);
            enemySpawner.Invoke("CheckEnemyStates", 1f);

        }

        //Set back Transform to  parrent to world if it the transform is a child of formation
        if (transform.parent != null)
        {
            transform.SetParent(transform.parent.parent);
        }

        gameSession.AddToScore(scoreValue);
        capsuleSpawner.SpawnCapsule(gameObject);

        //Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationofExplotion);
        this.gameObject.SetActive(false); //Setting this gameObject flase (Object Pooler Mechanism)
                                          //myAudioManager.play("EnemyDeathSFX");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    Path pathToFollow;

    [Header("Enemy")]
    [SerializeField] float health = 100;
    int scoreValue = 20;

    //Enemy  State
    public enum EnemyStates
    {
        FLY_IN, //First State
        TO_FORMATION,//Second State
        IDLE
    }
   
    EnemyStates enemyStates;
    int posInFormation;
    Formation formation;
    /*
    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 10f; 
    [SerializeField] float projectileSpeed = 10f;
    */
    /*
    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationofExplotion = 1f;
    
    */
    //Cached Ref
    GameSession gameSession;
    audio_Manager myAudioManager;

    ObjectPooler objectPooler;
    CapsuleSpawner capsuleSpawner;


    // Start is called before the first frame update
    void Start()
    {
        /*
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
        myAudioManager = FindObjectOfType<audio_Manager>();
        objectPooler = ObjectPooler.ObjectPullerInstance;
        //capsuleSpawnerScript = capsuleSpawner.GetComponent<CapsuleSpawner>();
        */
        //capsuleSpawner = CapsuleSpawner.CapsuleSpawnerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(enemyStates)
        {
            case EnemyStates.FLY_IN:
                MoveOnPath(pathToFollow);
                break;
            case EnemyStates.TO_FORMATION:
                MoveToFormation();
                break;
            case EnemyStates.IDLE:
                break;
        }
        
        //MoveOnPath(pathToFollow);


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
             transform.SetParent(formation.gameObject.gameObject.GetComponentInParent<Transform>());
            transform.eulerAngles = Vector2.zero; //Set rotation
             enemyStates = EnemyStates.IDLE;
         }
     }

    void MoveOnPath(Path path)
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

    
    public void SpawnSetup(Path path, int pos, Formation formation, float speed, float rotationSpeed)
    {
        pathToFollow = path;
        posInFormation = pos;
        this.formation = formation;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
    }
    
    /*private void CountDownAndShoot()
    {
        shotCounter = shotCounter - Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        //GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        GameObject laser = objectPooler.SpawnFromPool(projectile.ToString(), transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        //myAudioManager.play("EnemyShootSFX");
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
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddToScore(scoreValue);
        capsuleSpawner.SpawnCapsule(gameObject);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationofExplotion);
        myAudioManager.play("EnemyDeathSFX");
        
    }*/
}

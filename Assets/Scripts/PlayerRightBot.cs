using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightBot : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public enum BotStates
    {
        FLY_TO_POS, //First State
        FIRE,//Second State
    }

    [Header("Bot Requirments")]
    [HideInInspector] public BotStates botstates;
    [SerializeField] float speed = 2f;
    [SerializeField] float botOffSetfromX = 2f;
    [SerializeField] float botOffSetfromY = 2f;
    float reachDistance = 0.001f;

    [Header("Projectile Requirments")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] int numberOfProjectiles = 15;
    [SerializeField] float radius = 1f;


    ObjectPooler objectPooler;
    float tmp;
    Vector2 destinationPos;

    // Start is called before the first frame update
    void Start()
    {
        float xPos = transform.position.x + botOffSetfromX;
        float yPos = transform.position.y + botOffSetfromY;
        destinationPos = new Vector2(xPos, yPos);
        tmp = projectileFiringPeriod;
        objectPooler = ObjectPooler.ObjectPullerInstance;

    }

    // Update is called once per frame
    void Update()
    {
        switch (botstates)
        {
            case BotStates.FLY_TO_POS:
                GoToPoint(destinationPos);
                break;
            case BotStates.FIRE:
                Fire();
                break;
        }
    }

    void GoToPoint(Vector2 destinationPoint)
    {
        var distance = Vector2.Distance(transform.position, destinationPoint);
        if (distance >= reachDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinationPoint, speed * Time.deltaTime);
        }
        else
        {
            botstates = BotStates.FIRE;
        }
    }


    //Fire Here

    void Fire()
    {
        tmp = tmp - Time.deltaTime;
        if (tmp <= 0f)
        {
            Debug.Log("firing");
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfProjectiles;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector3 pos = transform.position + new Vector3(x, y, 0);
                float angleDegrees = -angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                //GameObject laser = Instantiate(laserPrefab, pos, rot) as GameObject;
                GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), pos, rot);
                float rotationinZ = (360 / numberOfProjectiles) * (i);
                laser.transform.eulerAngles = new Vector3(0f, 0f, rotationinZ + 90);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(x * projectileSpeed, y * projectileSpeed);
            }
            tmp = projectileFiringPeriod;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bot hit");
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (damageDealer) //If player collides with anything that has damageDealer on it like enemy, laser or damage capsule
        {
            //ProcessHit(damageDealer);
            PlayerBulletSpawner.rightBot = 0;
            PlayerShipTwoBulletSpawner.rightBot = 0;
            Destroy(gameObject);
            other.gameObject.SetActive(false);

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipTwoBulletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerShipTwoBulletSpawner playershipTwoBulletSpawnerInstance;
    ObjectPooler objectPooler;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject thikLaserPrefabForLvl5AndAbove;
    [SerializeField] GameObject diagonalLaserPrefab;
    [SerializeField] GameObject leftBotPrefab;
    [SerializeField] GameObject rightBotPrefab;
    [SerializeField] GameObject roundLaserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float level1OffsetFromX = 0.1f;
    [SerializeField] float level1OffsetFromY = 0.6f;
    [SerializeField] float level2OffsetFromX = 0.15f;
    [SerializeField] float level2OffsetFromY = 0.5f;
    [SerializeField] float level2RotationOfProjectileInZ = 10f;
    [SerializeField] float level2ProjectileSpeedInX = 3f;
    [SerializeField] float level4OffsetFromX = 0.2f;
    [SerializeField] float level5OffsetFromX = 0.5f;
    [SerializeField] float level5OffsetFromY = 1.5f;

    //Lasers
    GameObject leftLaser1;
    GameObject leftLaser2;
    GameObject leftLaser3;
    GameObject rightLaser1;
    GameObject rightLaser2;
    GameObject rightLaser3;
    GameObject centralLaser;

    public static int leftBot = 0;
    public static int rightBot = 0;

    private void Awake()
    {
        if (playershipTwoBulletSpawnerInstance == null)
        {
             playershipTwoBulletSpawnerInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.ObjectPullerInstance;
    }

    public void SpawnBullet(Transform playerPosition, int level)
    {
        switch (level)
        {
            case 1:
                GameObject leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level1OffsetFromX, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                GameObject rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level1OffsetFromX, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 2:
                leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level1OffsetFromX, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX - level4OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level1OffsetFromX, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX + level4OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 3:
                centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level1OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level1OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 4:
                centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level1OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX - level4OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level1OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX + level4OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;

            case 5:
                centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX - level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX + level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
                 case 6:
                    centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                    centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                    centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                    leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                    leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                    leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                    leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                    leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                    leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX - level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                    leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                    rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                    rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                    rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level1OffsetFromX), playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                    rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                    rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                    rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                    rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX + level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                    rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
                case 7:
                   GameObject laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                   laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                   laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   
                   laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + 2*level1OffsetFromY), Quaternion.identity);
                   laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                   laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   //Adjust the shooting sfx here currently it is handelded in player script
               
                    break;
                case 8:
                    //Top Row Bullets
                    laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                    laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + 2 * level1OffsetFromY), Quaternion.identity);
                    laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                    leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                    rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                    rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                break;

                case 9:
                //Top Row Bullets
                    laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                    laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + 2 * level1OffsetFromY), Quaternion.identity);
                    laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Left Side
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                    leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                    leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX - level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                    leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);

                //RightSide
                    rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                    rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);
                    rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX + level5OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                    rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                    rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);


                //Adjust the shooting sfx here currently it is handelded in player script
                break;

                case 10:
                     laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                     laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                     laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    laser = objectPooler.SpawnFromPool(roundLaserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + 2 * level1OffsetFromY), Quaternion.identity);
                    laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                     leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level2RotationOfProjectileInZ); //Setting the rotation
                     leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level2ProjectileSpeedInX, projectileSpeed);
                     rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                     rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level2RotationOfProjectileInZ); //Setting the rotation
                     rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level2ProjectileSpeedInX, projectileSpeed);                

                     //Circle bots


                     //Adjust the shooting sfx here currently it is handelded in player script
                     break;
        }


    }

    public void SpawnBot(Transform playerPos)
    {

        if (leftBot == 0)
        {
            //Spawn Left Bot
            Instantiate(leftBotPrefab, playerPos.position, Quaternion.identity);
            leftBot++;
        }

        if (rightBot == 0)
        {
            //Spawn Right Bot
            Instantiate(rightBotPrefab, playerPos.position, Quaternion.identity);
            rightBot++;

        }
    }
}

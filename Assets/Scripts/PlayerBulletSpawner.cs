using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    public static PlayerBulletSpawner playerBulletSpawnerInstance;
    ObjectPooler objectPooler;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject shipOnelevel1laserPrefab;
    [SerializeField] GameObject shipOnelevel2laserPrefab;
    [SerializeField] GameObject shipOnelevel3laserPrefab;
    [SerializeField] GameObject shipOnelevel4laserPrefab;
    [SerializeField] GameObject shipOnelevel5laserPrefab;
    [SerializeField] GameObject shipOnelevel6laserPrefab;
    [SerializeField] GameObject shipOnelevel7laserPrefab;
    [SerializeField] GameObject shipOnelevel8laserPrefab;
    [SerializeField] GameObject shipOnelevel9laserPrefab;
    [SerializeField] GameObject shipOnelevel10laserPrefab;
    [SerializeField] GameObject thikLaserPrefabForLvl5AndAbove;
    [SerializeField] GameObject diagonalLaserPrefab;
    [SerializeField] GameObject leftBotPrefab;
    [SerializeField] GameObject rightBotPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float level1OffsetFromY = 1.1f;
    [SerializeField] float level2OffsetFromX = 0.1f;
    [SerializeField] float level2OffsetFromY = 0.6f;
    [SerializeField] float level3OffsetFromX = 0.15f;
    [SerializeField] float level3OffsetFromY = 0.5f;
    [SerializeField] float level3RotationOfProjectileInZ = 10f;
    [SerializeField] float level3ProjectileSpeedInX = 3f;
    [SerializeField] float level4OffsetFromX = 0.2f;
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
        if(playerBulletSpawnerInstance == null)
        {
            playerBulletSpawnerInstance = this;
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
                //GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                GameObject laser = objectPooler.SpawnFromPool(shipOnelevel1laserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 2:
                //GameObject leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                GameObject leftLaser = objectPooler.SpawnFromPool(shipOnelevel2laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //GameObject rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                GameObject rightLaser = objectPooler.SpawnFromPool(shipOnelevel2laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 3:
                //leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1 = objectPooler.SpawnFromPool(shipOnelevel3laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2 = objectPooler.SpawnFromPool(shipOnelevel3laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);

                //rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1 = objectPooler.SpawnFromPool(shipOnelevel3laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2 = objectPooler.SpawnFromPool(shipOnelevel3laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 4:
                //leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);
                //leftLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX - level4OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser3 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX - level4OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser3.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);

                //rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //rightLaser3 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX + level4OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser3 = objectPooler.SpawnFromPool(shipOnelevel4laserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX + level4OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser3.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser3.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            
            case 5:
                centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3*level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel5laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel5laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 6:
                centralLaser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level5OffsetFromY), Quaternion.identity);
                centralLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1 = objectPooler.SpawnFromPool(shipOnelevel6laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2*level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2 = objectPooler.SpawnFromPool(shipOnelevel6laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);

                //rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1 = objectPooler.SpawnFromPool(shipOnelevel6laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2*level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2 = objectPooler.SpawnFromPool(shipOnelevel6laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 7:
                //Top Row Bullets
                GameObject centralLaser1 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (2*level5OffsetFromY)), Quaternion.identity);
                centralLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                
                //Middle Row Bullets
                GameObject centralLaser2 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (1*level5OffsetFromY)), Quaternion.identity);
                centralLaser2.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (1 * level5OffsetFromY)), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (1 * level5OffsetFromY)), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Bottom Row Bullets
                //laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                laser = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2*level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x - (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2*level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x + (2 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1 = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1 = objectPooler.SpawnFromPool(shipOnelevel7laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level2OffsetFromX), playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 8:
                //Top Row Bullets
                centralLaser1 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (3 * level5OffsetFromY)), Quaternion.identity);
                centralLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Middle Row Bullets
                centralLaser2 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                centralLaser2.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Bottom Row Bullets
                laser = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (4 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser1 = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x - (6 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (4 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser1 = objectPooler.SpawnFromPool(shipOnelevel8laserPrefab.ToString(), new Vector2(playerPosition.position.x + (6 * level2OffsetFromX), playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Diagonal Bullets
                 GameObject leftDiagonalLaser = objectPooler.SpawnFromPool(diagonalLaserPrefab.ToString(), new Vector2(playerPosition.position.x - (3*level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                 leftDiagonalLaser.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                 leftDiagonalLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);

                GameObject rightDiagaonalLaser = objectPooler.SpawnFromPool(diagonalLaserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level3OffsetFromX), playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightDiagaonalLaser.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightDiagaonalLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;

            case 9:
                //Top Row Bullets
                centralLaser1 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                centralLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                
                centralLaser2 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                centralLaser2.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Circle bots


                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 10:
                //Top row
                centralLaser1 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                centralLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel10laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel10laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (2 * level5OffsetFromY)), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Bottom Laser
                centralLaser2 = objectPooler.SpawnFromPool(thikLaserPrefabForLvl5AndAbove.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                centralLaser2.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                centralLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                leftLaser = objectPooler.SpawnFromPool(shipOnelevel10laserPrefab.ToString(), new Vector2(playerPosition.position.x - (3 * level2OffsetFromX), playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                rightLaser = objectPooler.SpawnFromPool(shipOnelevel10laserPrefab.ToString(), new Vector2(playerPosition.position.x + (3 * level2OffsetFromX), playerPosition.position.y + (level5OffsetFromY)), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                //Diagonal laser
                leftLaser2 = objectPooler.SpawnFromPool(diagonalLaserPrefab.ToString(), new Vector2(playerPosition.position.x - level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);
                rightLaser2 = objectPooler.SpawnFromPool(diagonalLaserPrefab.ToString(), new Vector2(playerPosition.position.x + level3OffsetFromX, playerPosition.position.y + level3OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);

                //Circle bots


                //Adjust the shooting sfx here currently it is handelded in player script
                break;
        }
        
        
    }

    public void SpawnBot(Transform playerPos)
    {
       
        if(leftBot == 0)
        {
            //Spawn Left Bot
            Instantiate(leftBotPrefab, playerPos.position, Quaternion.identity);
            leftBot++;
        }

        if(rightBot == 0)
        {
            //Spawn Right Bot
            Instantiate(rightBotPrefab, playerPos.position, Quaternion.identity);
            rightBot++;
            
        }
    }
}

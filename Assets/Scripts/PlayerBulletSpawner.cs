using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    public static PlayerBulletSpawner playerBulletSpawnerInstance;
    ObjectPooler objectPooler;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float level1OffsetFromY = 1.1f;
    [SerializeField] float level2OffsetFromX = 0.3f;
    [SerializeField] float level2OffsetFromY = 0.6f;
    [SerializeField] float level3RotationOfProjectileInZ = 20f;
    [SerializeField] float level3ProjectileSpeedInX = 8f;

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
                GameObject laser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x, playerPosition.position.y + level1OffsetFromY), Quaternion.identity);
                laser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotation to 0
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 2:
                GameObject leftLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                GameObject rightLaser = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
            case 3:
                GameObject leftLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                leftLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                GameObject leftLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x - level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                leftLaser2.transform.eulerAngles = new Vector3(0f, 0f, level3RotationOfProjectileInZ); //Setting the rotation
                leftLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-level3ProjectileSpeedInX, projectileSpeed);

                GameObject rightLaser1 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser1.transform.eulerAngles = new Vector3(0f, 0f, 0f); //Setting the rotatio to 0
                rightLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                GameObject rightLaser2 = objectPooler.SpawnFromPool(laserPrefab.ToString(), new Vector2(playerPosition.position.x + level2OffsetFromX, playerPosition.position.y + level2OffsetFromY), Quaternion.identity);
                rightLaser2.transform.eulerAngles = new Vector3(0f, 0f, -level3RotationOfProjectileInZ); //Setting the rotation
                rightLaser2.GetComponent<Rigidbody2D>().velocity = new Vector2(level3ProjectileSpeedInX, projectileSpeed);
                //Adjust the shooting sfx here currently it is handelded in player script
                break;
        }
        
        
    }
}

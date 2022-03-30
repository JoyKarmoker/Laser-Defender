using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2BulletStage3 : MonoBehaviour
{
    public GameObject bulletPrefab2;
    int called;
    // Start is called before the first frame update
    void Start()
    {
        BulletMove();
    }
    public void count(int call)
    {
        called = call;
    }

    void BulletMove()
    {
        called++;
        int i = 0;
        if (called % 2 == 0)
        {
            float angel = 0;
            float speedInX = 0.0f;
            while (i < 10 && angel <= 360)
            { 
                GameObject bullet1 = (GameObject)Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                if (angel < 180)
                    bullet1.GetComponent<Boss2BulletMovement>().SetAngle(angel, speedInX);
                if(angel == 0 || angel == 180 || angel == 360)
                    bullet1.GetComponent<Boss2BulletMovement>().SetAngle(angel, speedInX - speedInX);
                if (angel > 180)
                    bullet1.GetComponent<Boss2BulletMovement>().SetAngle(angel, -(speedInX - 12));
                angel += 60;
                speedInX += 4.0f;
                i++;
            }
        }
        else if (called % 2 != 0)
        {
            float angel = 30f;
            float speedInX = 3.0f;
            while (i < 10 && angel <= 360)
            {
                GameObject bullet1 = (GameObject)Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                if (angel <= 180)
                    bullet1.GetComponent<Boss2BulletMovement>().SetAngle(angel, speedInX);
                if (angel > 180)
                    bullet1.GetComponent<Boss2BulletMovement>().SetAngle(angel, -(speedInX - 7));
                angel += 60;
                speedInX += 2.0f;
                i++;
            }
        }
    }
}

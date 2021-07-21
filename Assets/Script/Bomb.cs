using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombBlustTime;
    public GameObject bomb;
    public GameObject[] bullets;
    // Start is called before the first frame update
    void Start()
    {
        bombBlustTime = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(bombBlustTime < 0){
            Destroy(bomb);
            Instantiate(bullets[0], transform.position, transform.rotation);
            Instantiate(bullets[1], transform.position, transform.rotation);
            Instantiate(bullets[2], transform.position, transform.rotation);
            Instantiate(bullets[3], transform.position, transform.rotation);
        }

        bombBlustTime -= Time.deltaTime;
    }
}

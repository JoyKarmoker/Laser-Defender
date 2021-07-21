using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9 : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Vector2 moveVector;
    public Transform target;
    private float burstTime;
    public GameObject bullet;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        burstTime = 2f;
        // if no target specified, assume the player
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);

        if(burstTime < 0){
            position = target.position;
            StartCoroutine(Fire());
            burstTime = 5f;
        }
        
        burstTime -= Time.deltaTime;
    }

    // Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

   IEnumerator Fire(){
        Bullet bull= Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        bull.move(position);
        yield return new WaitForSeconds(0.3f);
        Bullet bull1= Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        bull1.move(position);
        yield return new WaitForSeconds(0.3f);
        Bullet bull2= Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        bull2.move(position);
        yield return new WaitForSeconds(0.3f);
    }
}

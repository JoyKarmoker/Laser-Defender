using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] Vector3 scaleChange = new Vector3(0.05f, 0.05f, 0.05f);
    [SerializeField] float scaleDownCounter = 0.01f;

    float tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = scaleDownCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ontrigger enter");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CountDownAndScaleDown(collision);

    }

    private void CountDownAndScaleDown(Collider2D collision)
    {
        scaleDownCounter = scaleDownCounter - Time.deltaTime;
        if (scaleDownCounter <= 0f)
        {
            //Debug.Log("On trigger stay");
            collision.GetComponent<Transform>().localScale = collision.GetComponent<Transform>().localScale - scaleChange;
            scaleDownCounter = tmp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }
}

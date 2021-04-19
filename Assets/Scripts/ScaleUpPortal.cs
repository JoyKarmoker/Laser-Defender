﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpPortal : MonoBehaviour
{
    //[SerializeField] Vector3 scaleChange = new Vector3(0.05f, 0.05f, 0.05f);
    [SerializeField] float percentScaleChange = 10f;
    [SerializeField] float scaleUpCounter = 0.1f;

    float tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = scaleUpCounter;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CountDownAndScaleDown(collision);

    }

    private void CountDownAndScaleDown(Collider2D collision)
    {
        Vector3 objectSize = collision.GetComponent<Transform>().localScale;
        scaleUpCounter = scaleUpCounter - Time.deltaTime; //Scale down every 'scaleDownCounter' second by 'percentScaleChange' percent
        if (scaleUpCounter <= 0f)
        {
            //Debug.Log("On trigger stay");
            if (objectSize.x < 1)
            {
                //objectSize = objectSize - scaleChange;
                objectSize = objectSize + objectSize * (percentScaleChange / 100);
                if (objectSize.x >= 0f && objectSize.x <= 1f)
                {
                    collision.GetComponent<Transform>().localScale = objectSize;
                }

            }

            scaleUpCounter = tmp;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(GradualScaleUp(collision));
        //collision.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator GradualScaleUp(Collider2D collision)
    {
        Vector3 objectSize = collision.GetComponent<Transform>().localScale; //Scale up every 'scaleDownCounter' second by 'percentScaleChange' percent
        while (objectSize.x <= 1)
        {
            objectSize = objectSize + objectSize * (percentScaleChange / 100);
            collision.GetComponent<Transform>().localScale = objectSize;
            yield return new WaitForSeconds(scaleDownCounter);
        }
    }*/
}
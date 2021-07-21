using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounderies : MonoBehaviour
{
    public GameObject bg;
    public Camera maincamera;

    private Vector2 screenBounds;
    
    private float bgWidth;
    private float bgHeight;
 
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = maincamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, maincamera.transform.position.z));

        bgWidth = bg.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        bgHeight = bg.GetComponent<SpriteRenderer>().bounds.size.y /2;
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
      if(screenBounds.x < bgWidth){
       transform.position = new Vector3(Mathf.Clamp(transform.position.x, -screenBounds.x , screenBounds.x),
                                        Mathf.Clamp(transform.position.y, -screenBounds.y , screenBounds.y),transform.position.z);
        }
        else if( screenBounds.x > bgWidth){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bgWidth, bgWidth),
                                        Mathf.Clamp(transform.position.y, -screenBounds.y , screenBounds.y),transform.position.z);

        }
    }
    
}

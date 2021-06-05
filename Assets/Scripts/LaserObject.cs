using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;
    [SerializeField] LayerMask enemyLayer;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    [SerializeField] float sourceLaserHealth = 10f;
    DamageDealer damageDealer;

    // Define an "infinite" size, not too big but enough to go off screen
    [SerializeField] float maxLaserSize = 20f;
    [SerializeField] float tmpMiddle = 0f;
    [SerializeField] float tmpEnd = 0f;

    [SerializeField] LaserObject[] otherLaserObjects;

    [SerializeField] Vector3 raycastOffset;

/*    public enum WhichLaserState
    { 
        right,
        up,
        right_up
    }; 
    public WhichLaserState laserState;*/


    private void Start()
    {
        damageDealer = GetComponent<DamageDealer>();

        start = Instantiate(laserStart) as GameObject;
        middle = Instantiate(laserMiddle) as GameObject;
        end = Instantiate(laserEnd) as GameObject;

       
        
        start.SetActive(false);
        middle.SetActive(false);
        end.SetActive(false);



        if(gameObject.tag != "SourceLaser")
        {
            this.enabled = false;
        }

    }

    private void Update()
    {
        start.transform.localRotation = middle.transform.localRotation = end.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (gameObject.tag == "SourceLaser" && sourceLaserHealth <= 0)
        {
            for (int i = 0; i < otherLaserObjects.Length; i++)
            {
                otherLaserObjects[i].enabled = false;
            }

            Destroy(gameObject);
        }

       
    }
    void FixedUpdate()
    {
        //Create the laser start from the prefab
        if (start.activeSelf == false)
        {
            //start = Instantiate(laserStart) as GameObject;
            start.SetActive(true);

            start.transform.parent = this.transform;
            start.transform.localPosition = Vector2.zero;
        }

        // Laser middle
        if (middle.activeSelf == false)
        {
            //middle = Instantiate(laserMiddle) as GameObject;
            middle.SetActive(true);

            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
        }

        float currentLaserSize = maxLaserSize;

        // Raycast at the top as our sprite has been design for that
        Vector2 laserDirection = this.transform.up;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + raycastOffset, laserDirection, maxLaserSize, enemyLayer);

        Debug.DrawRay(transform.position + raycastOffset, laserDirection, Color.red);

        if (hit.collider != null)
        {
            // We touched something!
            // -- Get the laser length
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            // -- Create the end sprite
            if (end.activeSelf == false)
            {
                //end = Instantiate(laserEnd) as GameObject;
                end.SetActive(true);
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
            }

            //turn hit object on..
            hit.collider.gameObject.GetComponent<LaserObject>().enabled = true;

        }
        else
        {
            // Nothing hit
            // -- No more end
            if (end.activeSelf == true)
                end.SetActive(false);

            // Destroy(end);
        }

        // Place things
        // -- Gather some data
        float startSpriteHeight = start.GetComponent<Renderer>().bounds.size.y;
        float endSpriteHeight = 0f;
        if (end != null)
            endSpriteHeight = end.GetComponent<Renderer>().bounds.size.y;


        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        middle.transform.localScale = new Vector3(middle.transform.localScale.x, currentLaserSize - startSpriteHeight, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2(0f, (currentLaserSize / 2f) - tmpMiddle);

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(0f, currentLaserSize - tmpEnd);
        }
    }

   
}

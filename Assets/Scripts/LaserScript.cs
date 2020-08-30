using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    DamageDealer damageDealer;

    public float blah;

    // Define an "infinite" size, not too big but enough to go off screen
    [SerializeField] float maxLaserSize = 20f;

    private void Start()
    {
        damageDealer = GetComponent<DamageDealer>();

        start = Instantiate(laserStart) as GameObject;
        middle = Instantiate(laserMiddle) as GameObject;
        end = Instantiate(laserEnd) as GameObject;

        start.SetActive(false);
        middle.SetActive(false);
        end.SetActive(false);

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

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.up;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + new Vector3(0,1,0), laserDirection, maxLaserSize);


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

            //TODO: Deal Damage to hit object
            
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
        middle.transform.localPosition = new Vector2(0f,(currentLaserSize / 2f));

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(0f, currentLaserSize-0.166f);
        }

        // scaling section
        if (start != null && start.activeSelf)
            StartCoroutine(ScaleLaserStart(start));
        if (middle != null && middle.activeSelf)
            StartCoroutine(ScaleLaserMiddle(middle));
        if (end != null && end.activeSelf)
            StartCoroutine(ScaleLaserEnd(end));

    }

    IEnumerator ScaleLaserStart(GameObject obj)
    {
        obj.transform.localScale = new Vector2(1.3f, obj.transform.localScale.y);
        yield return new WaitForSeconds(0.8f);
        if(obj != null)
            obj.transform.localScale = new Vector2(1f, obj.transform.localScale.y);

    }
    IEnumerator ScaleLaserMiddle(GameObject obj)
    {
        obj.transform.localScale = new Vector2(1.3f, obj.transform.localScale.y);
        yield return new WaitForSeconds(0.8f);
        if(obj != null)
            obj.transform.localScale = new Vector2(1f, obj.transform.localScale.y);

    }
    IEnumerator ScaleLaserEnd(GameObject obj)
    {
        obj.transform.localScale = new Vector2(1.3f, obj.transform.localScale.y);
        yield return new WaitForSeconds(0.8f);
        if(obj != null)
            obj.transform.localScale = new Vector2(1f, obj.transform.localScale.y);

    }
}

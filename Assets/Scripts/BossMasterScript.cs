using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMasterScript : MonoBehaviour
{
    [SerializeField] GameObject firePrefab;
    public Vector3 offset;


    /// <summary>
    /// All the methods of map 1 bosses is below
    /// </summary>

    //Map1 Boss 1
    public void Map1_B1_A1()
    {
        // this fires straight downwards...
        GameObject fire = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-100f * Time.deltaTime);
    }
    public void Map1_B1_A2()
    {
        //this finds player, get direction and throw fire...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection =  player.transform.position - transform.position ;

        GameObject fire1 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire1.GetComponent<Rigidbody2D>().velocity = playerDirection;

        GameObject fire2 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire2.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(2,0,0);

        GameObject fire3 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire3.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(-2, 0, 0);
    }
    public void Map1_B1_A3()
    {
        //this finds player, get direction and fire towards the direction...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection = player.transform.position - transform.position;
        GameObject fire = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire.GetComponent<Rigidbody2D>().velocity = playerDirection;
    }

    //Map1 Boss 2
    public void Map1_B2_A1()
    {
        //this finds player, get direction and throw fire...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection = player.transform.position - transform.position;

        GameObject fire1 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire1.GetComponent<Rigidbody2D>().velocity = playerDirection;

        GameObject fire2 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire2.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(2, 0, 0);

        GameObject fire3 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire3.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(-2, 0, 0);
    }
    public void Map1_B2_A2()
    {

    }            
    public void Map1_B2_A3()
    {

    }

    //Map1 Boss 3
    public void Map1_B3_A1()
    {
        //this finds player, get direction and fire towards the direction...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection = player.transform.position - transform.position;
        GameObject fire = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire.GetComponent<Rigidbody2D>().velocity = playerDirection;
    }
    public void Map1_B3_A2()
    {
        //this finds player, get direction and fire towards the direction...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection = player.transform.position - transform.position;
        GameObject fire = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire.transform.localScale = new Vector3(5,5,5);
        fire.GetComponent<Rigidbody2D>().velocity = playerDirection;
    }
    public void Map1_B3_A3()
    {
        //this finds player, get direction and throw fire...
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerDirection = player.transform.position - transform.position;

        GameObject fire1 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire1.GetComponent<Rigidbody2D>().velocity = playerDirection;

        GameObject fire2 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire2.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(2, 0, 0);

        GameObject fire3 = Instantiate(firePrefab, transform.position + offset, Quaternion.identity);
        fire3.GetComponent<Rigidbody2D>().velocity = playerDirection + new Vector3(-2, 0, 0);

    }

    //Map1 Boss 4
    public void Map1_B4_A1()
    {

    }
    public void Map1_B4_A2()
    {

    }
    public void Map1_B4_A3()
    {

    }

    //Map1 Boss 5
    public void Map1_B5_A1()
    {

    }
    public void Map1_B5_A2()
    {

    }
    public void Map1_B5_A3()
    {

    }

    /// <summary>
    /// All the methods of map 2 bosses is below
    /// </summary>

    //Map2 Boss 1
    public void Map2_B1_A1()
    {

    }
    public void Map2_B1_A2()
    {

    }
    public void Map2_B1_A3()
    {

    }
}

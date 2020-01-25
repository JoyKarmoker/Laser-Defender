﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = transform.position.x + deltaX;

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
    }
}

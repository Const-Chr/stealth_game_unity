using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Laser_Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float moveDistance = 10.0f;
    private Vector3 startPosition;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current movement distance
        float currentMoveDistance = Vector3.Distance(startPosition, transform.position);

        // Check if the laser has reached the defined distance
        if (currentMoveDistance >= moveDistance)
        {
            // Reverse the movement direction
            movingUp = !movingUp;
            startPosition = transform.position;
        }

        // Move the laser
        Vector3 direction;
        if (movingUp)
        {
            direction = Vector3.forward;
        }
        else
        {
            direction = Vector3.back;
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
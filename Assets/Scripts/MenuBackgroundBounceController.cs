using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundBounceController : MonoBehaviour
{
    private GameObject Background;
    private float moveSpeed = 0.5f;  // Speed of the background's movement
    private float minX = -2f;        // Minimum X position
    private float maxX = 2f;         // Maximum X position
    private float currentX;          // Current X position of the background
    private bool movingLeft = true;  // Direction flag for movement

    void Start()
    {
        Background = GameObject.Find("Background");
        currentX = Background.transform.position.x;  // Initialize current X position
    }

    void Update()
    {
        // Get the current position of the background
        Vector3 backgroundPos = Background.transform.position;

        // Move the background left or right gradually
        if (movingLeft)
        {
            currentX -= moveSpeed * Time.deltaTime;  // Move left over time
            if (currentX <= minX)  // If the background reaches minX, start moving right
            {
                movingLeft = false;
            }
        }
        else
        {
            currentX += moveSpeed * Time.deltaTime;  // Move right over time
            if (currentX >= maxX)  // If the background reaches maxX, start moving left
            {
                movingLeft = true;
            }
        }

        // Update the position of the background
        Background.transform.position = new Vector3(currentX, backgroundPos.y, backgroundPos.z);
    }
}

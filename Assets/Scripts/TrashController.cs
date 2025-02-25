using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TrashController : MonoBehaviour {
    private GameObject GameController;
    private Rigidbody2D rb;
    private Vector3 offset;
    private float zDistance;
    // Track if the object is being dragged
    private bool isBeingDragged = false;
    // Store the original position to return to if needed

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameController = GameObject.Find("GameController");
    }

    void Update() {
    
        // Clamp x position to prevent moving outside bounds
        if (transform.position.x >= 2.5f) {
            transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -2.5f) {
            transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        }

        // Clamp y position to prevent moving outside bounds
        if (transform.position.y >= 3.5f) {
            transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
        }
        if (transform.position.y <= -4.5f) {
            transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
        }
    }

    // This function is called when the user clicks on the object.
    private void OnMouseDown() {
        // Only allow dragging if the object is not destroyed or moved back
        isBeingDragged = true;

        // Calculate the offset between the object's position and the mouse position
        zDistance = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
    }

    // This function is called while the user drags the object.
    private void OnMouseDrag() {
        // Update the object's position based on the mouse's current position.
        if (isBeingDragged) {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    // Helper function to get the mouse position in world space
    private Vector3 GetMouseWorldPosition() {
        // Get the mouse position in screen space, and convert it to world space.
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = zDistance; // Set the distance to the camera from the object.
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    // Called when the user releases the mouse button
    private void OnMouseUp() {
        // When dragging ends, set isBeingDragged to false
        isBeingDragged = false;
    }
}

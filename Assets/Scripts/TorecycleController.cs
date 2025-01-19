using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TorecyleController : MonoBehaviour {
    private GameObject GameController;
    private Rigidbody2D rb;

    // Track if the object is being dragged
    public bool isFound = false;
    // Store the original position to return to if needed

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameController = GameObject.Find("GameController");
    }

    void Update() {

    }

    // This function is called when the user clicks on the object.
    private void OnMouseDown() {
        // Only allow dragging if the object is not destroyed or moved back
        GameController.GetComponent<ClutterGameController>().objectfound = true;
    }

}

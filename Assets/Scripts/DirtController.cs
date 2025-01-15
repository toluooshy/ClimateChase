using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class DirtController : MonoBehaviour
{
    private GameObject GameController;
    private Rigidbody2D rb;
    private Vector3 offset;
    private float zDistance;
    private string Cloth = "Cloth";
    private bool colliding = true; // temporarily forces true for this second part because I am not sure if needed

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameController = GameObject.Find("GameController");
    }
    void Update ()
    {
        
    }

    // Called when the object enters a trigger (collision area)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (colliding || collision.tag == Cloth) {
            // If it's colliding with the washcloth, increment sorted items and destroy the object
            GameController.GetComponent<SolarGameController>().destroyedDirt += 1;
            Destroy(this.gameObject);
        }
    }
}



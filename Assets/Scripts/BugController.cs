using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BugController : MonoBehaviour
{
    private GameObject GameController;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameController = GameObject.Find("GameController");

        // Rotate the object randomly around the Z-axis
        float randomZRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomZRotation);
    }

    void OnMouseDown() {
        // Optional: Check if clicking over UI so we don’t destroy when clicking buttons, etc.
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // Increment captured bugs counter and destroy this GameObject
        GameController.GetComponent<CaptureGameController>().capturedBugs += 1;
        Destroy(this.gameObject);
    }
}

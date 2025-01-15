using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcecapController : MonoBehaviour {
    private GameObject GameController;
    public GameObject Icecap;
    private SpriteRenderer icecapSpriteRenderer;  // SpriteRenderer reference

    // Start is called before the first frame update
    void Start() {
        GameController = GameObject.Find("GameController");
        icecapSpriteRenderer = Icecap.GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component
        Color currentColor = icecapSpriteRenderer.color;  // Get the current color of the sprite
        currentColor.a = 0f;  // Set alpha to 0 (fully transparent)
        icecapSpriteRenderer.color = currentColor;  // Apply the color with 0 alpha
    }

    // Update is called once per frame
    void Update() {
        float soundMagnitude = GameController.GetComponent<GlaciersGameController>().soundMagnitude;
        float soundThreshold = GameController.GetComponent<GlaciersGameController>().soundThreshold;

        // Calculate the opacity based on sound magnitude and threshold
        float opacity = soundMagnitude / soundThreshold;

        // Clamp the opacity between 0 and 1
        opacity = Mathf.Clamp(opacity, 0f, 1f);

        // Adjust the alpha value of the sprite
        Color currentColor = icecapSpriteRenderer.color;
        currentColor.a = opacity;  // Set alpha to the calculated opacity
        icecapSpriteRenderer.color = currentColor;  // Apply the new color with updated alpha
    }
}

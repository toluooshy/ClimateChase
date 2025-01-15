using UnityEngine;

public class FireController : MonoBehaviour {
    private GameObject GameController;
    public GameObject Fire;
    private SpriteRenderer fireSpriteRenderer;  // SpriteRenderer reference

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameController = GameObject.Find("GameController");
        fireSpriteRenderer = Fire.GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component
        Color currentColor = fireSpriteRenderer.color;  // Get the current color of the sprite
        currentColor.a = 0f;  // Set alpha to 0 (fully transparent)
        fireSpriteRenderer.color = currentColor;  // Apply the color with 0 alpha
    }

    // Update is called once per frame
    void Update() {
        float fireDamage = GameController.GetComponent<ForestFireGameController>().fireDamage;
        float fireStrength = GameController.GetComponent<ForestFireGameController>().fireStrength;

        // Calculate the opacity based on sound magnitude and threshold
        float opacity = 1 - (fireDamage / fireStrength);

        // Clamp the opacity between 0 and 1
        opacity = Mathf.Clamp(opacity, 0f, 1f);

        // Adjust the alpha value of the sprite
        Color currentColor = fireSpriteRenderer.color;
        currentColor.a = opacity;  // Set alpha to the calculated opacity
        fireSpriteRenderer.color = currentColor;  // Apply the new color with updated alpha
    }

    // Called when the object enters a trigger (collision area)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Raindrop") {
            GameController.GetComponent<ForestFireGameController>().fireDamage += 1;
            Destroy(collision.gameObject);
        }
    }
}

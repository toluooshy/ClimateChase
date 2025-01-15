using UnityEngine;

public class AnimalController : MonoBehaviour {
    private GameObject GameController;
    public GameObject Animal;
    private SpriteRenderer animalSpriteRenderer;  // SpriteRenderer reference
    public Sprite sweatySprite;  // Reference to the sweaty sprite
    public Sprite chillSprite;   // Reference to the chill sprite
    public float brightnessThreshold = 0.5f;  // Threshold for switching between sprites

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameController = GameObject.Find("GameController");
        animalSpriteRenderer = Animal.GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component

        // Ensure the initial sprite is set correctly based on brightness at the start
        UpdateSpriteBasedOnBrightness(1f);
    }

    // Update is called once per frame
    void Update() {
        // Get the current brightness value from the GameController
        float brightness = GameController.GetComponent<SavannaShadeGameController>().brightness;

        // Update the sprite based on the current brightness
        UpdateSpriteBasedOnBrightness(brightness);
    }

    // Method to update the sprite based on the brightness
    void UpdateSpriteBasedOnBrightness(float brightness) {
        if (brightness < brightnessThreshold && GameController.GetComponent<BaseGameController>().isStarted) {
            // Set the sprite to "chill" if the brightness is above the threshold
            animalSpriteRenderer.sprite = chillSprite;
        } else {
            // Set the sprite to "sweaty" if the brightness is below the threshold
            animalSpriteRenderer.sprite = sweatySprite;
        }
    }
}

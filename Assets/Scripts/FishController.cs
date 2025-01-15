using UnityEngine;

public class FishController : MonoBehaviour {
    private GameObject GameController;
    public GameObject Fish;

    // Movement speed variables
    public float moveSpeed = 5f;  // Speed for horizontal movement
    public float verticalSpeed = 5f;  // Speed for upward movement
    public float minX = -1.75f;  // Min X position
    public float maxX = 1.75f;   // Max X position

    // Camera follow variables
    public float cameraOffsetY = 0f;  // Offset for the camera on the Y-axis
    public float cameraOffsetZ = -10f;  // Offset for the camera on the Z-axis (so it's not too close to the fish)

    // For detecting collisions
    private Rigidbody2D rb;  // Rigidbody for movement and collision

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameController = GameObject.Find("GameController");
        rb = Fish.GetComponent<Rigidbody2D>();  // Get the Rigidbody2D for collision detection
        if (rb == null) {
            Debug.LogError("Rigidbody2D component not found on Fish GameObject!");
        }
    }

    // Update is called once per frame
    void Update() {
        // Constant upward movement (along the Y axis)
        Vector3 currentPosition = Fish.transform.position;
        currentPosition.y += verticalSpeed * Time.deltaTime;  // Move upwards continuously
        Fish.transform.position = currentPosition;

        // On mobile, use device tilt (accelerometer) to move the fish horizontally
        float tiltInput = Input.acceleration.x;  // Get the tilt along the X axis
        currentPosition.x += tiltInput * moveSpeed * Time.deltaTime;

        // On desktop, use the arrow keys to move horizontally
        if (Input.GetKey(KeyCode.LeftArrow)) {
            currentPosition.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            currentPosition.x += moveSpeed * Time.deltaTime;
        }

        // Constrain the X position to be within the defined range
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        // Apply the new position to the fish
        Fish.transform.position = currentPosition;

        // Make the camera follow the fish
        FollowFishWithCamera();
    }

    // Makes the main camera follow the fish
    void FollowFishWithCamera() {
        // Get the camera's current position
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            // Set the camera's position to follow the fish, but with the Y and Z offsets
            Vector3 cameraPosition = new Vector3(0, Fish.transform.position.y + cameraOffsetY, cameraOffsetZ);
            mainCamera.transform.position = cameraPosition;
        }
    }

    // Collision detection for the "Bag" object
    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the fish has collided with an object tagged "Bag"
        if (collision.tag == "Bag") {
            // Stop the fish's horizontal movement
            rb.linearVelocity = Vector2.zero;  // Set the velocity to zero, stopping all movement
            rb.angularVelocity = 0;  // Set the velocity to zero, stopping all movement
        }
        else if (collision.tag == "Waterfall") {
            GameController.GetComponent<RiverGameController>().goalReached = true;
        }
    }
}

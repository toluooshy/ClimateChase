using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClutterGameController : BaseGameController {
    public GameObject Obstacle;
    public GameObject Torecycle;
    public int torecycleItems = 1;
    public int obstaclesItems = 6;
    public bool objectfound=false;

    // Start is called before the first frame update
    void Start() {
        ActiveDisplay.GetComponent<Canvas>().enabled = true;
        GameoverDisplay.GetComponent<Canvas>().enabled = false;
        startTime = Time.time;
        StartCoroutine(StartGameAfterDelay(introTime));  // Start the coroutine to wait for introTime seconds
    }

    // Coroutine to wait for introTime seconds before starting the game
    IEnumerator StartGameAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay time
        isStarted = true;  // Set isStarted to true after the delay

        InitItem(Torecycle);  // Spawn the selected item
        // Now spawn items
        for (int i = 0; i < obstaclesItems; i++) {
            SpawnItem(Obstacle);  // Spawn the selected item
        }
    }

    // Update is called once per frame
    void Update() {
        if (isStarted && remainingTime > 0) {
            // Countdown the remaining time in milliseconds
            remainingTime -= Time.deltaTime;  // Subtract deltaTime in milliseconds

            if (remainingTime <= 0) {
                isOver = true;  // The game ends when remainingTime reaches 0
                ActiveDisplay.GetComponent<Canvas>().enabled = false;
                GameoverDisplay.GetComponent<Canvas>().enabled = true;
            }
        }

        // If the game has been completed
        if (objectfound) {
            // Pick a random scene from the list and load it
            string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
            SceneManager.LoadScene(randomScene);
        }
    }

    // Spawns the item
    void SpawnItem(GameObject Item) {
        float X = UnityEngine.Random.Range(-1f, 1f);
        float Y = UnityEngine.Random.Range( 1f, 3f);

        Instantiate(Item, new Vector3(X, Y, 0), transform.rotation);
    }

    void InitItem(GameObject Item) {
        float X = 0;
        float Y = 0;
        Instantiate(Item, new Vector3(X, Y, 0), transform.rotation);
    }
}

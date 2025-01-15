using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RiverGameController : BaseGameController {
    
    public int numBags;
    public bool goalReached;
    
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

        // Integrate the microphone loudness detection and print the loudness
        if (isStarted) {
            // If the game has been completed
            if (goalReached) {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                SceneManager.LoadScene(randomScene);
            }
        }
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindmillGameController : BaseGameController {
    public float curry;
    public int spindegree;
    public float distance = 0;
    public float distancethreshhold = 100;
    
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
            // Countdown the remaining time in seconds
            remainingTime -= Time.deltaTime;  // Subtract deltaTime in seconds

            if (remainingTime <= 0) {
                isOver = true;  // The game ends when remainingTime reaches 0
                ActiveDisplay.GetComponent<Canvas>().enabled = false;
                GameoverDisplay.GetComponent<Canvas>().enabled = true;
            }
        }

        // check camera coverage
        if (isStarted) {

            // If the game has been completed (based on sound threshold)
            if (distance >= distancethreshhold) {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                SceneManager.LoadScene(randomScene);
            }
        }
    }
}

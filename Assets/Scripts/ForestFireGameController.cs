using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ForestFireGameController : BaseGameController {
    public int fireStrength = 20;
    public int fireDamage = 0;
    public float shakeMagnitude;
    private Vector3 lastAcceleration;    // Store the last frame's acceleration for shake calculation
    private Vector3 currentAcceleration; // Current frame's acceleration
    public TextMeshProUGUI RunningScoreAText;
    public TextMeshProUGUI RunningScoreBText;
    public TextMeshProUGUI HighScoreText;  

    // Start is called before the first frame update
    void Start() {
        RunningScoreAText = RunningScoreA.GetComponent<TextMeshProUGUI>();
        RunningScoreBText= RunningScoreB.GetComponent<TextMeshProUGUI>();
        HighScoreText = HighScore.GetComponent<TextMeshProUGUI>();
        introTime = PlayerPrefs.GetFloat("IntroTime", 1.5f);
        totalTime = PlayerPrefs.GetFloat("TotalTime", 9f);
        remainingTime = PlayerPrefs.GetFloat("TotalTime", 9f);
        score = PlayerPrefs.GetInt("RunningScore", 0);
        RunningScoreAText.text = score.ToString();

        ActiveDisplay.GetComponent<Canvas>().enabled = true;
        GameoverDisplay.GetComponent<Canvas>().enabled = false;
        startTime = Time.time;
        lastAcceleration = Input.acceleration;  // Initialize the lastAcceleration
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
                int highscore = PlayerPrefs.GetInt("HighScore", 0);
                if (score > highscore) {
                    highscore = score;
                    PlayerPrefs.SetInt("HighScore", score);
                }
                ActiveDisplay.GetComponent<Canvas>().enabled = false;
                GameoverDisplay.GetComponent<Canvas>().enabled = true;
                RunningScoreBText.text = score.ToString();
                HighScoreText.text = highscore.ToString();
            }
        }

        // Implement the microphone loudness detection and print the loudness
        if (isStarted) {
            shakeMagnitude = 0;
            float keyboardShankiness = 0;
            // On desktop, use the arrow keys to shake
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
                keyboardShankiness = .5f;
            }

            shakeMagnitude = DetectShake() + keyboardShankiness;
            
            // Check if the shake magnitude exceeds the threshold
            if (fireDamage >= fireStrength) {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                PlayerPrefs.SetInt("RunningScore", score + 1);
                PlayerPrefs.SetFloat("IntroTime", introTime - .005f);
                PlayerPrefs.SetFloat("TotalTime", totalTime - .0075f);
                SceneManager.LoadScene(randomScene);
            }
        }

        // Method to detect device shake using accelerometer
        float DetectShake() {
            // Get the current acceleration
            currentAcceleration = Input.acceleration;

            // Calculate the difference in acceleration between this frame and the last frame
            float deltaAcceleration = (currentAcceleration - lastAcceleration).magnitude;

            // Store the current acceleration for the next frame
            lastAcceleration = currentAcceleration;

            // Update shake magnitude if it is substantial enough
            return deltaAcceleration > 2 ? deltaAcceleration : 0;
        }
    }
}

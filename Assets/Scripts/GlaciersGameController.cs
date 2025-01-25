using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GlaciersGameController : BaseGameController {
    public float soundThreshold = .225f;
    public float soundMagnitude;
    private AudioClip microphoneClip;
    private int sampleWindow = 128;  // Number of samples to average for the loudness
    private string microphoneDevice;
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
        StartCoroutine(StartGameAfterDelay(introTime));  // Start the coroutine to wait for introTime seconds

        // Start the microphone input when the game starts
        StartMicrophone();
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

        // Integrate the microphone loudness detection and print the loudness
        if (isStarted) {
            soundMagnitude = GetLoudnessFromMicrophone();
            // If the game has been completed
            if (soundMagnitude >= soundThreshold) {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                PlayerPrefs.SetInt("RunningScore", score + 1);
                PlayerPrefs.SetFloat("IntroTime", introTime - .005f);
                PlayerPrefs.SetFloat("TotalTime", totalTime - .0075f);
                SceneManager.LoadScene(randomScene);
            }
        }
    }

    // Start the microphone recording
    void StartMicrophone() {
        // Check if a microphone is connected
        if (Microphone.devices.Length > 0) {
            // Get the first microphone device
            microphoneDevice = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphoneDevice, true, 1, 44100); // Start recording

            // Wait until the microphone has started recording (it may take a little time)
            while (Microphone.GetPosition(microphoneDevice) <= 0) {
                // Wait until the microphone is ready
            }
        } else {
            Debug.LogError("No microphone detected.");
        }
    }

    // Get the loudness from the microphone input
    float GetLoudnessFromMicrophone() {
        float[] samples = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(microphoneDevice) - sampleWindow + 1;

        if (micPosition < 0) return 0;  // If the mic hasn't reached this sample window yet

        microphoneClip.GetData(samples, micPosition); // Get the microphone data for the current window

        // Calculate the average loudness (RMS value)
        float sum = 0f;
        foreach (float sample in samples) {
            sum += Mathf.Abs(sample); // Sum of absolute values for the average loudness
        }

        return sum / sampleWindow; // Return the average loudness
    }
}

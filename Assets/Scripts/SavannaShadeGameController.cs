using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SavannaShadeGameController : BaseGameController {
    public float cameraCoverage = 0f;  // Camera coverage threshold, as a fraction (0 to 1)
    public float brightness = 1f; 
    private WebCamTexture webcamTexture;  // WebCamTexture for the device camera
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

        // Start the camera input when the game starts
        StartCamera();
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

        // check camera coverage
        if (isStarted) {
            // Check the current light level through the camera (or webcam)
            float coverage = MeasureLightCoverage();
            // Debug.Log("Coverage: " + coverage);

            // If the game has been completed (based on sound threshold)
            if (coverage >= .7) {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                PlayerPrefs.SetInt("RunningScore", score + 1);
                PlayerPrefs.SetFloat("IntroTime", introTime - .005f);
                PlayerPrefs.SetFloat("TotalTime", totalTime - .0075f);
                SceneManager.LoadScene(randomScene);
            }
        }
    }

    // Start the camera light checking
    void StartCamera() {
        if (Application.isMobilePlatform) {
            // Mobile device: Use the front-facing camera (if available)
            WebCamDevice[] devices = WebCamTexture.devices;
            foreach (var device in devices) {
                if (device.isFrontFacing) {
                    webcamTexture = new WebCamTexture(device.name);
                    webcamTexture.Play();  // Start the camera feed
                    return;
                }
            }
            Debug.LogWarning("No front-facing camera found on this mobile device.");
        } else {
            // Desktop or non-mobile platform: Use the webcam (first available device)
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length > 0) {
                webcamTexture = new WebCamTexture(devices[0].name);  // Use the first available webcam
                webcamTexture.Play();  // Start the webcam feed
            } else {
                Debug.LogWarning("No webcam found on this desktop device.");
            }
        }
    }

    // Measure how much light is being blocked by the user's hand
    float MeasureLightCoverage() {
        if (webcamTexture == null) {
            Debug.LogWarning("WebCamTexture not initialized!");
            return 0f;
        }

        // Create a texture to read the pixels from the webcam feed
        Texture2D cameraTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        cameraTexture.SetPixels(webcamTexture.GetPixels());
        cameraTexture.Apply();

        // Calculate the average brightness from the texture
        Color[] pixels = cameraTexture.GetPixels();
        foreach (Color pixel in pixels) {
            brightness += pixel.grayscale;  // Use grayscale for a basic brightness measure
        }

        brightness /= pixels.Length;

        // If the brightness is below the threshold (e.g., camera is covered), then return the coverage value
        return 1 - brightness;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClutterGameController : BaseGameController {
    public GameObject Obstacle;
    public GameObject Torecycle;
    public int torecycleItems = 1;
    public int obstaclesItems = 10;
    public bool objectfound=false;
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

        // If the game has been completed
        if (objectfound) {
            // Pick a random scene from the list and load it
            string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
            PlayerPrefs.SetInt("RunningScore", score + 1);
            PlayerPrefs.SetFloat("IntroTime", introTime - .005f);
            PlayerPrefs.SetFloat("TotalTime", totalTime - .0075f);
            SceneManager.LoadScene(randomScene);
        }
    }

    // Spawns the item
    void SpawnItem(GameObject Item) {
        float X = UnityEngine.Random.Range(-1.5f, 1.5f);
        float Y = UnityEngine.Random.Range(-3.5f, 3.5f);

        Instantiate(Item, new Vector3(X, Y, -1), transform.rotation);
    }

    void InitItem(GameObject Item) {
        float X = UnityEngine.Random.Range(-1f, 1f);
        float Y = UnityEngine.Random.Range(-3f, 3f);
        Instantiate(Item, new Vector3(X, Y, 0), transform.rotation);
    }
}

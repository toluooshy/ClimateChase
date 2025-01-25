using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SolarGameController : BaseGameController {
    public int destroyedDirt = 0; // variable to keep track of how much dirt is gone
    public int dirtTotal = 40; // how many dirts there should be
    public GameObject Dirt; 
    public GameObject Cloth;
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
       

        for(int i = 0; i < dirtTotal; i++)
        {
            SpawnItem(Dirt);
        }
         Instantiate(Cloth, new Vector3(0f, 0f, -1f), transform.rotation);
       // create a loop to instantiate the dirts based on how many there are to clean, and instantiate each
        // instantiate one washcloth

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

            else if (destroyedDirt >= dirtTotal)
            {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
                PlayerPrefs.SetInt("RunningScore", score + 1);
                PlayerPrefs.SetFloat("IntroTime", introTime - .005f);
                PlayerPrefs.SetFloat("TotalTime", totalTime - .0075f);
                SceneManager.LoadScene(randomScene);
            }

        }

      
    }
    void SpawnItem(GameObject Item) {

        float X = UnityEngine.Random.Range(-2.5f, 2.5f);
        float Y = UnityEngine.Random.Range(3.5f, -4.5f);

        Instantiate(Item, new Vector3(X, Y, 0), transform.rotation);
    }


}

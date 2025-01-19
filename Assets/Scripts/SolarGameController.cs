using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SolarGameController : BaseGameController {
    public int destroyedDirt = 0; // variable to keep track of how much dirt is gone
    public int dirtTotal = 0; // how many dirts there should be
    public GameObject Dirt; 
    public GameObject Cloth;

    
    // Start is called before the first frame update
    void Start() {
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
                ActiveDisplay.GetComponent<Canvas>().enabled = false;
                GameoverDisplay.GetComponent<Canvas>().enabled = true;
            }

            else if (destroyedDirt >= dirtTotal)
            {
                string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
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

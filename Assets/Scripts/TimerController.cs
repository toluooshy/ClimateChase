using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {
    private GameObject GameController;
    public GameObject Timer;
    public GameObject Bar;

    // Start is called before the first frame update
    void Start() {
        GameController = GameObject.Find("GameController");
        Timer.GetComponent<Canvas>().enabled = true;
    }

    // Update is called once per frame
    void Update() {
        float totalTime = GameController.GetComponent<BaseGameController>().totalTime;
        float remainingTime = GameController.GetComponent<BaseGameController>().remainingTime;

        // Adjust the TimerBar's RectTransform to scale the width
        Bar.GetComponent<RectTransform>().offsetMax = new Vector2(1055f * remainingTime/ totalTime, 40f); // Shrink the width
    }
}

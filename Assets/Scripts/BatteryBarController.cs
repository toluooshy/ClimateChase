using UnityEngine;

public class BatteryBarController : MonoBehaviour
{
    private GameObject GameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController = GameObject.Find("GameController");
        transform.localScale = new Vector3(1, 0, 1);
    }

    void Update() {
        float height = GameController.GetComponent<WindmillGameController>().distance;
        float maxHeight = GameController.GetComponent<WindmillGameController>().distancethreshhold;

        // Make sure maxHeight is not zero to avoid division by zero
        if (maxHeight != 0) {
            transform.localScale = new Vector3(1, height / maxHeight, 1);
        }
    }
}

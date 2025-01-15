using UnityEngine;

public class BladeController : MonoBehaviour
{
    private GameObject GameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController = GameObject.Find("GameController");
    }

    void Update() {
        float zdegree = GameController.GetComponent<WindmillGameController>().distance;
        transform.rotation = Quaternion.Euler(0,0,zdegree * -10);
    }
}

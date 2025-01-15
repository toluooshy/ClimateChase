using UnityEngine;

public class RaindropGenerator : MonoBehaviour {
    private GameObject GameController;
    public GameObject Raindrop;
    private float TimeBetweenSpawn = .005f;
    private float SpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update() {
        float shakeMagnitude = GameController.GetComponent<ForestFireGameController>().shakeMagnitude;
        if (Time.time > SpawnTime) {
            if (shakeMagnitude > 0) {
                SpawnItem(Raindrop);
            }
            SpawnTime = Time.time + TimeBetweenSpawn;
        }
        
    }

    // Spawns the item
    void SpawnItem(GameObject Item) {
        float X = UnityEngine.Random.Range(-1.5f, 1.5f);

        Instantiate(Item, new Vector3(X, 2.75f, 0), transform.rotation);
    }
}

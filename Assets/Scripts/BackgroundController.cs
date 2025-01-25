using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject Looseleaf;
    private Camera mainCamera;

    private GameObject spawnedLooseleaf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;

        // Spawn the Looseleaf object
        SpawnItem(Looseleaf);
    }

    void SpawnItem(GameObject Item) {
        // Instantiate the Looseleaf at the specified position
        spawnedLooseleaf = Instantiate(Item, new Vector3(0, 0, .5f), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure spawnedLooseleaf exists
        if (spawnedLooseleaf != null)
        {
            // Update the position of Looseleaf to follow the camera
            spawnedLooseleaf.transform.position = mainCamera.transform.position + new Vector3(0, 0, 10.5f);
        }
    }
}

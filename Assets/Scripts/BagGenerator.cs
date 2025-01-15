using UnityEngine;

public class BagGenerator : MonoBehaviour {
    private GameObject GameController;
    public GameObject Bag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GameController = GameObject.Find("GameController");
        int numBags = GameController.GetComponent<RiverGameController>().numBags;
        
        // Create a 2D array to mark occupied spots
        bool[,] occupiedSpots = new bool[36, 4]; // Grid of size 36x4
        
        for (int i = 0; i < numBags; i++) {
            // Find a random spot that is not adjacent to already occupied spots
            Vector2 spawnPos = GetRandomSpawnPosition(occupiedSpots);

            // Assuming SpawnItem is a method that spawns the bag at given coordinates
            SpawnItem(Bag, (int) spawnPos.x, (int) spawnPos.y);

            // Mark the spot as occupied
            int x = Mathf.FloorToInt(spawnPos.x);
            int y = Mathf.FloorToInt(spawnPos.y);
            occupiedSpots[x, y] = true;

            // Optionally, also mark adjacent spots as occupied to prevent adjacent items
            MarkAdjacentSpotsAsOccupied(occupiedSpots, x, y);
        }
    }

    Vector2 GetRandomSpawnPosition(bool[,] occupiedSpots) {
        // Try up to 100 times to find a valid spot
        for (int attempts = 0; attempts < 100; attempts++) {
            // Generate random x and y positions within the grid (36 x 4)
            int x = Random.Range(0, 36);
            int y = Random.Range(0, 4);
            
            // Check if the spot is occupied or adjacent spots are occupied
            if (!IsAdjacentOccupied(occupiedSpots, x, y)) {
                return new Vector2(x, y); // Return the found position
            }
        }

        // If no valid spot is found after 100 attempts, return the first available spot (fallback)
        for (int x = 0; x < 36; x++) {
            for (int y = 0; y < 4; y++) {
                if (!occupiedSpots[x, y]) {
                    return new Vector2(x, y);
                }
            }
        }

        return Vector2.zero; // Fallback to 0,0 (ideally won't happen)
    }

    bool IsAdjacentOccupied(bool[,] occupiedSpots, int x, int y) {
        // Check the 8 adjacent cells (including diagonals) for occupation
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                int newX = x + i;
                int newY = y + j;
                // Skip checking the current cell
                if (i == 0 && j == 0) continue;
                // Make sure the indices are within bounds
                if (newX >= 0 && newX < 36 && newY >= 0 && newY < 4) {
                    if (occupiedSpots[newX, newY]) {
                        return true; // Found an adjacent occupied spot
                    }
                }
            }
        }
        return false; // No adjacent spots are occupied
    }

    void MarkAdjacentSpotsAsOccupied(bool[,] occupiedSpots, int x, int y) {
        // Mark the adjacent cells as occupied to prevent future placement near this spot
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                int newX = x + i;
                int newY = y + j;
                // Skip the current cell
                if (i == 0 && j == 0) continue;
                // Make sure the indices are within bounds
                if (newX >= 0 && newX < 36 && newY >= 0 && newY < 4) {
                    occupiedSpots[newX, newY] = true;
                }
            }
        }
    }

    // Spawns the item
    void SpawnItem(GameObject Item, int xPos, int yPos) {
        // We swap coordinates here because the 2d array needs to be transposed to match our map.
        Instantiate(Item, new Vector3(yPos - 1.5f, (xPos - 6.5f) * .9f, -1), transform.rotation); 
    }
}

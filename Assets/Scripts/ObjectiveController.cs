using System.Collections;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {
    private GameObject GameController;
    private RectTransform rectTransform;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start() {
        // Get references
        GameController = GameObject.Find("GameController");
        rectTransform = GetComponent<RectTransform>(); // Assuming the object is a UI element
        startPos = rectTransform.position; // Store the initial position (centered)
        
        // End position: move off the screen, assuming screen height is used for the Y value
        endPos = new Vector3(startPos.x, startPos.y + 1500f, startPos.z); // Change 1500f to be off the screen

        // Start the coroutine to handle the slide-up animation
        StartCoroutine(SlideText());
    }

    IEnumerator SlideText() {
        float elapsedTime = 0f;
        float introTime = GameController.GetComponent<BaseGameController>().introTime;

        // 1/3 of introTime: keep the object centered
        yield return new WaitForSeconds(introTime / 3f);

        // 2/3 of introTime: Slide the object off the screen
        while (elapsedTime < introTime * 2f / 3f)
        {
            elapsedTime += Time.deltaTime;
            // Smoothly move the object using Lerp over time
            rectTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / (introTime * 2f / 3f));
            yield return null;  // Wait for the next frame
        }

        // Ensure the object is at the final position after the loop
        rectTransform.position = endPos;
    }
}

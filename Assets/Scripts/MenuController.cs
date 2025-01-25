using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private GameObject Modal;
    private GameObject Mission;
    private GameObject HowToPlay;
    private GameObject Credits;
    private GameObject Sound;
    private GameObject X;
    private GameObject MissionText;
    private GameObject HowToPlayText;
    private GameObject CreditsText;
    
    private bool activemusic = true;  // Assuming sound starts active

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find game objects by name and assign them to private variables
        Modal = GameObject.Find("Modal");
        Mission = GameObject.Find("Mission");
        HowToPlay = GameObject.Find("HowToPlay");
        Credits = GameObject.Find("Credits");
        Sound = GameObject.Find("Sound");
        X = GameObject.Find("X");
        MissionText = GameObject.Find("MissionText");
        HowToPlayText = GameObject.Find("HowToPlayText");
        CreditsText = GameObject.Find("CreditsText");

        // Initialize modal to be inactive by default
        MissionText.SetActive(true);
        HowToPlayText.SetActive(false);
        CreditsText.SetActive(false);

        // Reset opacity for all modal buttons
        SetButtonOpacity(Mission, 255);
        SetButtonOpacity(HowToPlay, 128);
        SetButtonOpacity(Credits, 128);
        
        CloseModal();
        activemusic = PlayerPrefs.GetInt("activemusic", 1) == 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (activemusic) {
            PlayerPrefs.SetInt("activemusic", 1);
            Sound.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/unmute");
        } else {
            PlayerPrefs.SetInt("activemusic", 0);
            Sound.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/mute");
        }
    }

    // Opens the modal window
    public void OpenModal()
    {
        Modal.SetActive(true);
    }

    // Closes the modal window
    public void CloseModal()
    {
        Modal.SetActive(false);
    }

    // Sets the appropriate modal view based on the provided argument
    public void SetModalView(string view)
    {
        // First, deactivate all text views
        MissionText.SetActive(false);
        HowToPlayText.SetActive(false);
        CreditsText.SetActive(false);

        // Reset opacity for all modal buttons
        SetButtonOpacity(Mission, 128);
        SetButtonOpacity(HowToPlay, 128);
        SetButtonOpacity(Credits, 128);

        // Set the view based on the argument
        switch (view)
        {
            case "Mission":
                MissionText.SetActive(true);
                SetButtonOpacity(Mission, 255);
                break;
            case "HowToPlay":
                HowToPlayText.SetActive(true);
                SetButtonOpacity(HowToPlay, 255);
                break;
            case "Credits":
                CreditsText.SetActive(true);
                SetButtonOpacity(Credits, 255);
                break;
            default:
                Debug.LogWarning("Unknown view type: " + view);
                break;
        }
    }

    // Helper method to set opacity of a button
    private void SetButtonOpacity(GameObject button, int opacity)
    {
        // Assuming the button has an image component that we can modify the color of
        var buttonImage = button.GetComponent<UnityEngine.UI.Image>();
        if (buttonImage != null)
        {
            Color currentColor = buttonImage.color;
            buttonImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, opacity / 255f);
        }
    }

    // Toggles the sound (playing music if active, stopping if not)
    public void ToggleSound()
    {
        activemusic = !activemusic;

    }
}

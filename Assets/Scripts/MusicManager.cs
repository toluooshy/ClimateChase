using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public GameObject Jukebox;
    public AudioSource src;
    public AudioClip song1;

    public static MusicManager instance;

    private bool playingmusic = true;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(Jukebox);
            DontDestroyOnLoad(src);
        } else {
            Destroy(Jukebox);
            Destroy(src);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("activemusic", 1) == 1) {
            src.clip = song1;
            src.loop = true;
            src.Play();
        }
        else {
            // If music is turned off, make sure to stop it and set the volume to 0
            src.Stop();
            src.volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int musicStatus = PlayerPrefs.GetInt("activemusic", 1);

        if (musicStatus == 1) {
            // Music is active, ensure it's playing and volume is set back to normal
            if (!src.isPlaying) {
                src.clip = song1;
                src.loop = true;
                src.Play();
            }
            src.volume = 1f;  // Ensure the volume is back to normal
        } 
        else {
            // Music is muted, stop the music and set the volume to 0
            if (src.isPlaying) {
                src.Stop();
            }
            src.volume = 0f;  // Mute the audio completely
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    // List of scene names for random loading
   private string[] scenes = new string[] {
        "ForestFireScene",
        "GlaciersScene",
        "SavannaShadeScene",
        "RiverScene",
        "RecycleScene",
        "ClutterScene",
        "SolarScene"
    }; 
   

    // Replay - load a random scene from the list
    public void Replay() {
        // Pick a random scene from the list and load it
        string randomScene = scenes[UnityEngine.Random.Range(0, scenes.Length)];
        SceneManager.LoadScene(randomScene);
    }

    // Load the Menu scene
    public void LoadMenu() {
        SceneManager.LoadScene("MenuScene");  // Load the MenuScene
    }

    // Exit the application
    public void Exit() {
        Application.Quit();
    }
}

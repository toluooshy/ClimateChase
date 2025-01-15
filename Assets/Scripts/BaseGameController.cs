using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGameController : MonoBehaviour {
    public GameObject ActiveDisplay;
    public GameObject GameoverDisplay;
    public bool isStarted = false;
    public bool isCompleted = false;
    public bool isOver = false;
    public float introTime = 1f;
    public float totalTime = 8f;
    public float remainingTime = 8f;
    public float startTime;
    public string[] scenes = new string[] {
        "ForestFireScene",
        "GlaciersScene",
        "SavannaShadeScene",
        "RiverScene",
        "RecycleScene"
    };
}
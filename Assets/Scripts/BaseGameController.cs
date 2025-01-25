using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGameController : MonoBehaviour {
    public GameObject ActiveDisplay;
    public GameObject GameoverDisplay;
    public GameObject RunningScoreA;
    public GameObject RunningScoreB;
    public GameObject HighScore;
    public bool isStarted = false;
    public bool isCompleted = false;
    public bool isOver = false;
    public float introTime;
    public float totalTime;
    public float remainingTime;
    public float startTime;
    public int score;
    public string[] scenes = new string[] {
        "ForestFireScene",
        "GlaciersScene",
        "SavannaShadeScene",
        "RiverScene",
        "RecycleScene",
        "WindmillScene",
        "ClutterScene",
        "SolarScene"
    };
}
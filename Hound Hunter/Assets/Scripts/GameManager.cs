using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : Monobehaviour
{
    public int score;
    public int highScore;
    public GameObject player;
    public GameObject hounds;
    public GameManager dogs;

    // UI
    public GameObject gameOver;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    // Singlton
    public static GameManager instance;

    private Awake() {
        
    }

    private void Start() 
    {
        
    }
}
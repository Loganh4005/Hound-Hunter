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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start() 
    {
        score = 0;
        //PlayerPrefs.SetInt("highScore", highScore);
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreText = highScore;
    }
    public void SpawnHound() {
        
    }
    public void SpawnDog() {

    }
    public void HoundKilled() {

    }
    public void DogKilled() {

    }
    public void GameOver() {

    }
}
using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : Monobehaviour
{
    public int score;
    public int highScore;
    public int wave = 1;
    public GameObject player;
    public GameObject houndPrefab;
    public GameManager dogPrefab;

    public Dictionary<int, Hound> hounds = new Dictionary<int, Hound>();
    public Dictionary<int, Dog> dogs = new Dictionary<int, Dog>();
    
    public int nextDog;
    public int nextHound;
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
    private void Update() {
        if (score > 0) {
            GameOver();
        }
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        if (dogs.Count = 0) {
            GameOver();
        }
        if (hounds.Count = 0) {
            NewWave();
        }
    }
    public void NewWave() {
        wave++;
        for (int i = wave * 5; i <= 0; i--) {
            SpawnHound(); 
        }
        for (int i = wave * 2; i <= 0; i--) {
            SpawnDog();
        }
    }
    public void SpawnHound() {
        Hound hound = Instantiate(houndPrefab); // Add position at home!
        hounds.Add(nextHound, hound);
        hound.id = nextHound;
        nextHound++;
    }
    public void SpawnDog() {
        Dog dog = Instantiate(dogPrefab);
        dogs.Add(nextDog, dog);
        dog.id = nextDog;
        nextDog++;
        
    }
    public void HoundKilled(int _id) {
        score += 3;
        scoreText.Text = "Score: " + score;
    }
    public void DogKilled(int _id) {
        score += -6;
        scoreText.Text = "Score: " + score;
    }
    public void GameOver() {
        gameOver.Active(true);
    }
}
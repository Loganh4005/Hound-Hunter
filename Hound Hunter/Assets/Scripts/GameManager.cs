using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public int wave = 0;
    public int highWave;
    public GameObject player;
    public GameObject houndPrefab;
    public GameObject dogPrefab;

    public Dictionary<int, Hound> hounds = new Dictionary<int, Hound>();
    public Dictionary<int, Dog> dogs = new Dictionary<int, Dog>();
    
    public int nextDog;
    public int nextHound;

    public LayerMask dontSpawnLayer;
        
    // UI
    
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public Collider2D boundsCollider;
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
        highScoreText.text = "High Score: " + highScore;
        highWave = PlayerPrefs.GetInt("highWave");
        NewWave();
    }
    private void Update() {
        if (score < 0) {
            GameOver();
        }
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            //if (PlayerPrefs.GetString("username") == "")
            //{
                Leaderboard.instance.SubmitScore(score, 0);
            //}
            highScoreText.text = "High Score: " + highScore;

        }
        if (wave < 0)
        {
            GameOver();
        }
        if (wave > highWave)
        {
            highWave = wave;
            PlayerPrefs.SetInt("highWave", highWave);
            //if (PlayerPrefs.GetString("username") == "")
            //{
            Leaderboard.instance.SubmitScore(wave, 1);
            //}

        }
        if (dogs.Count == 0) {
            GameOver();
        }
        if (hounds.Count == 0) {
            NewWave();
        }
    }
    public void NewWave() {
        wave++;
        Debug.Log("Wave: " + wave);
        for (int i = wave * 2; i >= 0; i--) {
            SpawnHound();
            Debug.Log("Hound Spawned yay");
        }
        for (int i = wave * 1; i >= 0; i--) {
            SpawnDog();
            Debug.Log("Dogs are cutebut cats are cute");
        }
    }
    public void SpawnHound() {
        
        Hound hound = Instantiate(houndPrefab).GetComponent<Hound>(); // Add position at home!
        hounds.Add(nextHound, hound);
        hound.id = nextHound;
        hound.gameObject.transform.position = GetRandomPosition();
        nextHound++;
    }
    public void SpawnDog() {
        Dog dog = Instantiate(dogPrefab).GetComponent<Dog>();
        dogs.Add(nextDog, dog);
        dog.id = nextDog;
        dog.gameObject.transform.position = GetRandomPosition();
        nextDog++;
        
    }
    public void HoundKilled(int _id) {
        score += 3;
        scoreText.text = "Score: " + score;
        hounds.Remove(_id);
    }
    public void DogKilled(int _id) {
        score += -6;
        scoreText.text = "Score: " + score;
        dogs.Remove(_id);
    }
    public void GameOver() {
        SceneManager.LoadScene(2);
    }
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(boundsCollider.bounds.center.x - boundsCollider.bounds.extents.x, boundsCollider.bounds.center.x + boundsCollider.bounds.extents.x);
        float y = Random.Range(boundsCollider.bounds.center.y - boundsCollider.bounds.extents.y, boundsCollider.bounds.center.y + boundsCollider.bounds.extents.y);
        float z = Random.Range(boundsCollider.bounds.center.z - boundsCollider.bounds.extents.z, boundsCollider.bounds.center.z + boundsCollider.bounds.extents.z);

        Vector3 randomPos = new Vector3(x, y, z);

        // Use Physics.Raycast to check for collisions with objects on the "Obstacles" layer
        RaycastHit hit;
        if (Physics.Raycast(randomPos, Vector3.down, out hit, Mathf.Infinity, dontSpawnLayer))
        {
            // If a collision is detected, try again with a new random position
            return GetRandomPosition();
        }
        else
        {
            // If no collision is detected, return the random position
            return randomPos;
        }
    }
   

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    
    
    public LeaderboardList[] leaderboards;
    public int currentLeaderboard;
    int MaxScores;
    public TMP_Text[] leaderboardText;
    public TMP_Text leaderboardTitle;

    // Start is called before the first frame update

    public static Leaderboard instance;

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
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Connected To Leaderboard");
                AfterLogin();
            } else
            {
                Debug.LogError($"Failed to connect to Leaderboard (LootLocker). Error Code: {response.statusCode}");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubmitScore(int _score, int _id)
    {
        if (PlayerPrefs.GetString("username") == "")
            return;
        LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("username"), _score, leaderboards[_id].scoreboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log($"Submitted score {_score} to the leaderboard!");
            }
            else
            {
                Debug.LogError($"Failed to submit score to Leaderboard (LootLocker). Error Code: {response.statusCode}");
            }
        });
    }

    private void AfterLogin()
    {
        if (PlayerPrefs.GetString("username") == "")
            return;
        LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("username"), (response) =>
        {

        });
        GetScore();
    }
    private void GetScore()
    {
        if (leaderboardText.Length == 0)
            return;

        LootLockerSDKManager.GetScoreList(leaderboards[currentLeaderboard].scoreboardID, leaderboardText.Length, (leaderboard) =>
        {
            if (leaderboard.success)
            {
                Debug.Log($"Leaderboard Downloaded Successfully");

                for (int i = 0; i < leaderboard.items.Length; i++)
                {
                    leaderboardText[i].text = i + 1 + ". " + leaderboard.items[i].player.name + ", " + leaderboards[currentLeaderboard].Type + ": " + leaderboard.items[i].score;
                }
                leaderboardTitle.text = leaderboards[currentLeaderboard].Name;
            }
            else
            {
                Debug.LogError($"Failed to submit score to Leaderboard (LootLocker). Error Code: {leaderboard.statusCode}");
            }
        });
    }
    public void NextLeaderboard()
    {
        currentLeaderboard += 1;
        if (leaderboards.Length == currentLeaderboard)
        {
            currentLeaderboard = 0;
        }
        GetScore();
    }
    public void PreviousLeaderboard()
    {
        currentLeaderboard -= 1;
        if (-1 == currentLeaderboard)
        {
            currentLeaderboard = leaderboards.Length - 1;
        }
        GetScore();
    }
}
[System.Serializable]
public class LeaderboardList
{
    public string scoreboardID;
    public string Name;
    public string Type;

}

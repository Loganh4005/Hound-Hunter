using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField usernameText;

    public void Start()
    {
        usernameText.text = PlayerPrefs.GetString("username");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void UpdateUsername()
    {
        PlayerPrefs.SetString("username", usernameText.text);
    }
    
}

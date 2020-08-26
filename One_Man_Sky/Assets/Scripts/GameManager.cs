using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }

       else if (instance != this)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Restart Scene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void Tutor()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void Buggy()
    {
        SceneManager.LoadScene("stage 1v2");
    }

    public void ChooseGame()
    {
        SceneManager.LoadScene("Select Level");
    }
}

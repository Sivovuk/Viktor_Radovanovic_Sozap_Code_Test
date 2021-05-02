using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool isMainMenu = false;
    public bool isMainGame = false;

    private void Start()
    {
        if (isMainGame)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.mainGame);
            AudioManager.Instance.StopAudio(AudioManager.Instance.mainMenu);
        }
        else if (isMainMenu)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.mainMenu);
            AudioManager.Instance.StopAudio(AudioManager.Instance.mainGame);
        }
    }

    public void LoadScene(int sceneIdex) 
    {
        SceneManager.LoadScene(sceneIdex);
    }

    public void RestartScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}

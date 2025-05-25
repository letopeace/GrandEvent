using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    public int sceneNumber = 1;
    public void PlayButton()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}

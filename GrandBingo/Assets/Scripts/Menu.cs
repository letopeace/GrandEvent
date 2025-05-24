using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

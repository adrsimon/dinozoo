using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PLayGame()
    {
        // Load the next scene in the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

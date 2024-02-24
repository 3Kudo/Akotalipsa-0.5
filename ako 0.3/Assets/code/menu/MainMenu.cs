using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private static GameObject menu;

    public void GameBegin()
    {
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitApp()
    {
        MouseControle.instance.Default();
        Application.Quit();
        Debug.Log("Application has quit.");
    }

    public void Credits()
    {

    }
}
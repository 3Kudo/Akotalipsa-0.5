using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameObject menu;
    private GameObject koniec;

    public void GameBegin()
    {
        MouseControle.instance.Default();
        koniec = GameObject.Find("Panel");
        koniec.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitApp()
    {
        MouseControle.instance.Default();
        Application.Quit();
        Debug.Log("Application has quit.");
    }
}
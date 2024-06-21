using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuPostaci : MonoBehaviour
{

    private GameObject menu;
    private GameObject koniec;

    int order;

    public void MenuChooseCharacter()
    {
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ChoosingMole()
    {
        order = 1;
        StaticData.value = order;
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ChoosingShark()
    {
        order = 2;
        StaticData.value = order;
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ChoosingFrog()
    {
        order = 3;
        StaticData.value = order;
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ChoosingDuck()
    {
        order = 4;
        StaticData.value = order;
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackToMainMenu()
    {
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
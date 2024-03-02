using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    private  GameObject panel;

    private  GameObject koniec;

    public void GameRestart()
    {
        MouseControle.instance.Default();
        koniec = GameObject.Find("Panel");
        koniec.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene("Assets/Scenes/scena z dŸwiêkiem.unity");
    }

    public void GameToMenu()
    {
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}

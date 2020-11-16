using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Canvas loadingScreen;
    public TextMeshProUGUI tmp;
    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        //transform.children
        HideMenu();
        StartCoroutine(LoadSceneAsync());
    }

    private void HideMenu()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void ShowMenu()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowSettings(Canvas canvas)
    {
        HideMenu();
        canvas.gameObject.SetActive(true);

    }

    public void HideSettings(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
        ShowMenu();
    }




    IEnumerator LoadSceneAsync()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync(1);
        while(gameScene.progress < 1)
        {
            tmp.text = (gameScene.progress * 100).ToString("F0") + "%";
            yield return new WaitForEndOfFrame();
        }
    }
}

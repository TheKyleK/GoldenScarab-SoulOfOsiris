using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSettingManager : MonoBehaviour
{
    public Canvas settingMenu;
    bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

            paused = !paused;
        }

    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        settingMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        settingMenu.gameObject.SetActive(false);
    }
}

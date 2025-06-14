using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button SettingButton;

    public Button exitButton;

    public SettingManager settingManager;

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
        }
        if (SettingButton != null)
        {
            SettingButton.onClick.AddListener(OpenSettingsMenu);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(QuitGame);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("level0");

    }
   public void OpenSettingsMenu()
    {
        if (settingManager != null)
        { Debug.Log("尝试打开设置菜单，当前时间缩放: " + Time.timeScale);
            settingManager.OpenSettings();
        }
    }
   
       public void QuitGame()
    {
        Application.Quit();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button SettingButton;

    public Button exitButton;

    public SettingManager settingManager;

    private SaveData saveData;

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
        SceneManager.LoadScene(1);
        settingManager.CloseSettings();
    }
    public void OpenSettingsMenu()
    {
        if (settingManager != null)
        {
            Debug.Log("尝试打开设置菜单，当前时间缩放: " + Time.timeScale);
            settingManager.OpenSettings();
        }
    }

    public void QuitGame()
    {
        SaveSystem.SaveGame(GameManager.Instance.crrentLevel, GameManager.Instance.lastPosition);
        Application.Quit();

    }

    public void ContiuneGame()
    {
        saveData = SaveSystem.loadGame();
        Debug.Log("保存的关卡序号" + saveData.unlockedLevel);
        settingManager.CloseSettings();
        SceneManager.LoadScene(saveData.unlockedLevel);

      //  GameManager.Instance.ResetPosition();
     }
}

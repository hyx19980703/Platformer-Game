using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SettingManager : MonoBehaviour
{
    public GameObject settingPanel;
    public Button closedButton;
    public Button retrunMenu;

    public Slider volumnSlider;

    public TMP_Dropdown qualityDropdown;

    public bool isSettingOpen;

    void Start()
    {
        volumnSlider.value = AudioListener.volume;
        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.onValueChanged.AddListener(SetQuality);

        }
        if (volumnSlider != null)
            volumnSlider.onValueChanged.AddListener(SetVolume);


        settingPanel.SetActive(false);

        if (closedButton != null)
        {
            closedButton.onClick.AddListener(CloseSettings);
        }

        if (retrunMenu != null)
        {
            retrunMenu.onClick.AddListener(RetrunMenu);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSetting();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            OpenSettings();
        }

    }

    public void ToggleSetting()
    {
        isSettingOpen = !isSettingOpen;
        settingPanel.SetActive(isSettingOpen);

        if (isSettingOpen)
        {
            Time.timeScale = 0; //时间停止
        }
        else Time.timeScale = 1; //恢复时间
    }

    public void CloseSettings()
    {
        Debug.Log("关闭菜单前: isSettingOpen=" + isSettingOpen + ", 面板状态=" + settingPanel.activeSelf);
        isSettingOpen = false;
        settingPanel.SetActive(false);
        Time.timeScale = 1; // 恢复游戏时间
        Debug.Log("关闭菜单后: isSettingOpen=" + isSettingOpen + ", 面板状态=" + settingPanel.activeSelf);
    }
    // 设置音量
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // 设置图形质量
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void RetrunMenu()
    {
        SceneManager.LoadScene("Mnue");
    }

public void OpenSettings()
{
    Debug.Log("准备打开菜单");
    isSettingOpen = true;
    settingPanel.SetActive(true);
    

}



}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private LevelButton levelButtonPrefab; // 关卡按钮预制体
    [SerializeField] private Transform buttonContainer; // 按钮容器（如GridLayoutGroup）
    [SerializeField] private Button backButton; // 返回主菜单按钮

    private List<LevelButton> levelButtons = new List<LevelButton>();

    private void Start()
    {
        // 生成关卡按钮
        GenerateLevelButtons();

        // 绑定返回按钮事件
        backButton.onClick.AddListener(BackToMenu);
    }

    private void GenerateLevelButtons()
    {
        // 清除现有按钮
        foreach (var btn in levelButtons)
            Destroy(btn.gameObject);
        levelButtons.Clear();

        // 从LevelData获取所有关卡信息并生成按钮
        int levelCount = LevelData.Instance.GetLevelCount();
        for (int i = 0; i < levelCount; i++)
        {
            var levelInfo = LevelData.Instance.GetLevelInfo(i);
            if (levelInfo == null) continue;

            // 实例化按钮
            var buttonObj = Instantiate(levelButtonPrefab, buttonContainer);
            buttonObj.Init(i, levelInfo);
            levelButtons.Add(buttonObj);
        }
    }

    private void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mnue"); // 返回主菜单场景
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [Header("UI References")]
    public LevelButton levelButtonPrefab; // 在这里拖入你的 LevelButton 预制件
    public Transform[] buttonContainer;     // 在这里拖入一个空的 UI GameObject，作为所有关卡按钮的父对象

    // Start is called before the first frame update
    void Start()
    {
        // 检查 LevelData 是否存在
        if (LevelData.Instance == null)
        {
            Debug.LogError("LevelData instance not found! Make sure it's in the scene.");
            return;
        }

        // 清空容器中可能存在的旧按钮（例如，在返回主菜单再进入时）
        ClearExistingButtons();

        // 从 LevelData 实例中获取总关卡数
        int totalLevels = LevelData.Instance.GetLevelCount();

        // 遍历所有关卡数据
        for (int i = 0; i < totalLevels; i++)
        {
            // 从 LevelData 获取第 i 个关卡的信息
            LevelInfo levelData = LevelData.Instance.GetLevelInfo(i);

            if (levelData != null)
            {
                // 实例化一个按钮
                LevelButton newButton = Instantiate(levelButtonPrefab, levelData.levelBottonPosition);
                // 调用按钮的 Init 方法来初始化它
                // 注意：关卡索引通常从 1 开始显示给玩家，所以传入 i + 1
                Debug.Log(levelData.sceneName);

                //实例化关卡
                Debug.Log($"当前传入关卡数{i+1}");
                newButton.Init(i + 1, levelData);
            }
        }
    }

    // 清空现有的按钮，防止重复创建
    private void ClearExistingButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelInfo
{
    public string levelName; // 关卡名称（如"Level 1"）
    public string sceneName; // 对应场景文件名（如"level1"）
    public Sprite levelIcon; // 关卡图标

    public Sprite levelImage; //关卡缩略图
    public bool isUnlocked; // 是否解锁

    public Transform  levelBottonPosition;
  //  public int highScore; // 最高分（可选）
}

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;
    [SerializeField] private LevelInfo[] allLevels; // 在Inspector中配置所有关卡

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        // 初始化：默认解锁第一关，其他关卡从存档读取
        LoadLevelStatus();
    }

    // 从存档加载关卡解锁状态
    private void LoadLevelStatus()
{
    var saveData = SaveSystem.loadGame();

    if (saveData != null && saveData.unlockedLevel > 0) // 增加一个判断，确保解锁的关卡数大于0
    {
        int levelsToUnlock = saveData.unlockedLevel;
        // 为了保险起见，确保解锁的关卡数不超过总关卡数
        levelsToUnlock = Mathf.Min(levelsToUnlock, allLevels.Length);

        for (int i = 0; i < allLevels.Length; i++)
        {
            // 如果 i < levelsToUnlock - 1，是为了保持 "unlockedLevel" 表示"下一个待解锁关卡的索引"的逻辑
            // 例如，如果 unlockedLevel = 2，那么索引 0 和 1 (关卡1和2) 被解锁
            allLevels[i].isUnlocked = i < levelsToUnlock - 1; 
            bool ifUnlock() =>(allLevels[i].isUnlocked = i <= levelsToUnlock - 1) ;
                 
            Debug.Log($"关卡索引：{i},解锁关卡数:{levelsToUnlock}");
            Debug.Log($"关卡解锁状态{ifUnlock()}");
        }
    }
    else
    {
        // 如果存档不存在，或者存档中的解锁关卡数无效，则默认解锁第一关
        Debug.Log("未找到有效存档，默认解锁第一关。");
        for (int i = 0; i < allLevels.Length; i++)
        {
            allLevels[i].isUnlocked = (i == 0); // 只有索引为0的关卡（第一关）解锁
        }
    }
}

    // 获取关卡信息
    public LevelInfo GetLevelInfo(int index)
    {
        if (index >= 0 && index < allLevels.Length)
            return allLevels[index];
        return null;
        
    }

    // 获取总关卡数
    public int GetLevelCount() => allLevels.Length;


}
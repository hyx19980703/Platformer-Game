using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// 这是一个关卡选择按钮的脚本，通过读取关卡管理器中的关卡信息，以及读取配置的关卡图标来构建一个关卡按钮
/// </summary>
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private Image levelIcon;  //关卡数：1，2，3，4

    [SerializeField] private Image lockIcon; // 锁定图标
    [SerializeField] private Button button;

    private int levelIndex;
    private string sceneName;

    private string levelname;

    // 初始化按钮
    public void Init(int index, LevelInfo info)
    {
        
        levelIndex = index; //关卡数
        sceneName = info.sceneName;  //场景名称
        levelNameText.text = info.levelName; //关卡名称
        levelIcon.sprite = info.levelImage; //关卡数 1，2，3，4
        lockIcon.sprite = info.levelIcon;
        // 设置锁定/解锁状态
        SetUnlocked(info.isUnlocked);
    }
    //解锁关卡
    private void SetUnlocked(bool unlocked)
    {   Debug.Log("解锁关卡");
        button.interactable = unlocked;
        lockIcon.gameObject.SetActive(unlocked);
        levelIcon.color = unlocked ? Color.white : Color.gray;
    }

    // 按钮点击事件
    public void OnLevelSelect()
    {      Debug.Log("OnLevelSelect 被调用，当前 sceneName: " + sceneName); // 添加这行
        if (!string.IsNullOrEmpty(sceneName))
        {  
            SceneManager.LoadScene(sceneName);
            // 记录当前关卡（用于存档）
            Debug.Log($"当前存档关卡{levelIndex}");
            
        }
    }
}
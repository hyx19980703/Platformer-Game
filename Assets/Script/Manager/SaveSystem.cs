using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string savePath = Application.persistentDataPath + "/save.dat"; // 存储路径

    /// <summary>
    /// 用于存储游戏关卡数和最后的存档位置
    /// </summary>
    /// <param name="level"></param>
    /// <param name="lastPosition"></param>
    public static void SaveGame(int level, Vector3 lastPosition)
    {
        SaveData savedata = new SaveData(level, lastPosition);
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(savePath, json); // 持久化存储
    }
    /// <summary>
    /// 加载游戏，提供游戏目前所在的关卡和提供最后的存储位置
    /// </summary>
    /// <returns></returns>
    public static SaveData loadGame()
    {
        if (File.Exists(savePath))
        {
         string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        else
        {
            Debug.Log("存档不存在");
            return new SaveData(GameManager.Instance.crrentLevel, GameManager.Instance.lastPosition);
        }
    }
}

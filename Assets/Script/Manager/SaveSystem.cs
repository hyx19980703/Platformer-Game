using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string savePath = Application.persistentDataPath + "/save.dat";
    public static void SaveGame(int level, Vector3 lastPosition)
    {
        SaveData savedata = new SaveData(level, lastPosition);
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(savePath, json);
    }

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

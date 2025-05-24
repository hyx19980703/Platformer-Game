using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string savePath = Application.persistentDataPath + "./save.dat";
    public static void SaveGame(int level, Vector3 lastPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);
        SaveData savedata = new SaveData(level, lastPosition);
        formatter.Serialize(stream, savedata);
        stream.Close();
    }

    public static SaveData loadGame()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
        }

        else
        {
            Debug.Log("存档不存在");
            return new SaveData(GameManager.Instance.crrentLevel, GameManager.Instance.lastPosition);
        }
    }
}

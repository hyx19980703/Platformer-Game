using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public int unlockedLevel = 1;
    public Vector2 lastCheckPoint;

    public SaveData(int _unlockedLevel, Vector2 _lastCheckPoint)
    {
        this.unlockedLevel = _unlockedLevel;
        this.lastCheckPoint = _lastCheckPoint;
    }
}
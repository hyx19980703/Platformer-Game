using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class SaveData 
{
    public int unlockedLevel = 1;
    public Vector3 lastCheckPoint ;

    public SaveData(int _unlockedLevel, Vector3 _lastCheckPoint)
    {
        this.unlockedLevel = _unlockedLevel;
        this.lastCheckPoint = _lastCheckPoint;
    }
}
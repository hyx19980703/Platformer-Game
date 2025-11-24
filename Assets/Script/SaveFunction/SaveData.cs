using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class SaveData 
{
    public int unlockedLevel =1; //默认解锁关卡
    public Vector3 lastCheckPoint ; //最后存档位置

    public SaveData(int _unlockedLevel, Vector3 _lastCheckPoint)
    {
        this.unlockedLevel = _unlockedLevel;  //传入未解锁关卡
        this.lastCheckPoint = _lastCheckPoint; //最后存档位置
    }
}
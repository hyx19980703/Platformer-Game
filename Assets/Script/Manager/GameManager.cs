using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int currentScore = 0;
    int charactorHealth;

    public Vector2 lastPosition;


    public int crrentLevel;

    [SerializeField] private Transform deathLine;
    [SerializeField] private Charator charator;  // todo 移除charator，不在这里获取charator


    void Awake()
    {
       // charator = GameObject.FindWithTag("player").GetComponent<Charator>();
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        charactorHealth = 3;

    }

    void Update()
    {
        if (charator.transform.position.y < deathLine.position.y) //玩家死亡位置检测
            StartCoroutine("RespwanPlayer");
            //RespwanPlayer();
    }

    public void AddSource(int value)
    {
        currentScore += value;
        UIManager.instance.UpdateScore(currentScore);
    }

    public void HealthLess()   //todo： 掉血功能拆分
    {
        charactorHealth--;
        UIManager.instance.UpdateHealth(charactorHealth);
    }

    public void SetCheckPoint(Vector2 _lastPosition, int _currentLevel)
    {
        lastPosition = _lastPosition;
        crrentLevel = _currentLevel;
    }

    IEnumerator RespwanPlayer()  // todo： 重生功能拆分
    { 
        charator.rb.simulated = false; //禁用物理模拟
        charator.stateMachine.StateChange(charator.deathState); //进入死亡状态
        yield return new WaitForSeconds(1f); //1s后玩家回归正常位置
        GameObject.FindWithTag("Player").transform.position = lastPosition; //玩家恢复正常位置
        charator.rb.simulated = true; //恢复物理模拟
        
    }

    void OnDrawGizmos()   //绘制死亡线位置 todo：整合到重生功能里
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(deathLine.position, deathLine.position+Vector3.right*100);
    }

}

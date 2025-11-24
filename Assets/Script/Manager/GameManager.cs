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

    public Transform deathLine;
    [SerializeField] private Charator charator;


    void Awake()
    {
        // charator = GameObject.FindWithTag("player").GetComponent<Charator>();
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        charactorHealth = 3;

    }

    void Start()
    {
        SoundManager.instance.PlayMusic();
        
    }

    /// <summary>
    /// 记录分数
    /// </summary>
    /// <param name="value"></param>
    public void AddSource(int value)
    {
        currentScore += value;
        UIManager.instance.UpdateScore(currentScore);
    }

    /// <summary>
    /// 记录血量
    /// </summary>
    public void HealthLess()   
    {
        charactorHealth--;
        UIManager.instance.UpdateHealth(charactorHealth);
    }

    /// <summary>
    /// 设置检查点
    /// </summary>
    /// <param name="_lastPosition"></param>
    /// <param name="_currentLevel"></param>

    public void SetCheckPoint(Vector2 _lastPosition, int _currentLevel)
    {
        lastPosition = _lastPosition;
        crrentLevel = _currentLevel;
    }


    /// <summary>
    /// 角色位置返回最后检查点
    /// </summary>
    public void ResetPosition()
    {
    GameObject.FindWithTag("Player").transform.position = lastPosition;
    }


    /// <summary>
    /// 绘制死亡线
    /// </summary>
    void OnDrawGizmos()   //绘制死亡线位置 todo：整合到重生功能里
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(deathLine.position, deathLine.position+Vector3.right*100);
    }

}

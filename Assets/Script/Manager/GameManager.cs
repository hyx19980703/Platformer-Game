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

    [SerializeField] private Transform deathLine;
    [SerializeField] private Transform charator;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        charactorHealth = 3;

    }

    void Update()
    {
        if (charator.position.y < deathLine.position.y) //玩家死亡位置检测
            RespwanPlayer();
    }

    public void AddSource(int value)
    {
        currentScore += value;
        UIManager.instance.UpdateScore(currentScore);
    }

    public void HealthLess()
    {
        charactorHealth--;
        UIManager.instance.UpdateHealth(charactorHealth);
    }

    public void SetCheckPoint(Vector2 _lastPosition)
    {
        lastPosition = _lastPosition;
    }

    public void RespwanPlayer()
    {
        GameObject.FindWithTag("Player").transform.position = lastPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(deathLine.position, deathLine.position+Vector3.right*100);
    }

}

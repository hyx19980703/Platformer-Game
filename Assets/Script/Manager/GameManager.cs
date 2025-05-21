using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int currentScore = 0;
    int charactorHealth;

    public Vector2 lastPosition;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        charactorHealth = 3;

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

}

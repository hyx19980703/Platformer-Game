using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int currentScore = 0;
    int charactorHealth;

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
    
        public void  HealthLess()
    {
        charactorHealth --;
        UIManager.instance.UpdateHealth(charactorHealth);
    }

}

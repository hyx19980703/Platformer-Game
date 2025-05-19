using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;


        }
        else Destroy(gameObject);

       // scoreText = GetComponentInChildren<TextMeshProUGUI>();
       // healthText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void UpdateScore(int _score)
    {
        scoreText.text = $"score:{_score}";
    }

    public void UpdateHealth(int _health)
    {
        healthText.text = $"health:{_health}";
    }
}

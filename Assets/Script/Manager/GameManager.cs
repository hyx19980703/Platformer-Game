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
    [SerializeField] private Charator charator;


    void Awake()
    {
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

    public void HealthLess()
    {
        charactorHealth--;
        UIManager.instance.UpdateHealth(charactorHealth);
    }

    public void SetCheckPoint(Vector2 _lastPosition, int _currentLevel)
    {
        lastPosition = _lastPosition;
        crrentLevel = _currentLevel;
    }

    IEnumerator RespwanPlayer()
    {
        charator.rb.simulated = false;
        charator.stateMachine.StateChange(charator.deathState);
        charator.GetComponent<Charator>().enabled = false;
        yield return new WaitForSeconds(1f);
       // charator.stateMachine.StateChange(charator.respwanState);
        GameObject.FindWithTag("Player").transform.position = lastPosition;
        charator.GetComponent<Charator>().enabled = true;
        charator.rb.simulated = true;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(deathLine.position, deathLine.position+Vector3.right*100);
    }

}

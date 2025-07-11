using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class CharactorHealth : MonoBehaviour,Ideath
{
    [SerializeField] private int health = 100;
    private Charator player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Charator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && player.ReturnTimer < 0)
        {
            Dead();
            player.ReturnTimer = player.ReturnTime;
            health = 100;
        }
        if (isUnderDeathLine() && player.ReturnTimer < 0)
        {
            player.ReturnTimer = player.ReturnTime;
            Dead();
        }

    }
    void OnEnable()
    {
        EventManager.OnExplosionHurt += TakeDamage;

    }
    void OnDisable()
    {
        EventManager.OnExplosionHurt -= TakeDamage;
    }

    public void TakeDamage()
    {
        health=0;
    }
    public void Dead()
    {
        EventManager.PlayDeathEvent();
        player.ReturnTimer = player.ReturnTime;
        player.stateMachine.StateChange(player.deathState);
        Debug.Log("Dead!");
    }
    public void RespwanPlayer()
    {

    }
    public bool isUnderDeathLine()
    {
        if (this.transform.position.y <= GameManager.Instance.deathLine.position.y)
            return true;
        else
            return false;
    }


}

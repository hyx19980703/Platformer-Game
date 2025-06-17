using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CharactorHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Charator player;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.ExplosionHurt += TakeDamage;
        player = GetComponent<Charator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Dead();
            health = 100;
        }
    }

    public void TakeDamage()
    {
        health=0;
    }
    public void Dead()
    {
        if(player.ReturnTimer <= 0)
        {
            player.ReturnTimer = player.ReturnTime;
            player.stateMachine.StateChange(player.deathState);
            Debug.Log("Dead!");
        }
    }

}

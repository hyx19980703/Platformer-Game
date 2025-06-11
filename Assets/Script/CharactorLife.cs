using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorLife : MonoBehaviour
{
    [SerializeField] private float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeHealth()
    {
        health = 10;
    }
    public void GetHeal()
    {
        health++;
    }
    public void TakeDamage()
    {
        health-=5;
    }
}

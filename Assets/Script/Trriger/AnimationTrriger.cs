using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrriger : MonoBehaviour
{
    private Charator charator;
    private Animator animator;

    void Awake()
    {
        charator = GetComponentInParent<Charator>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespwanStart()
    {
        charator.stateMachine.StateChange(charator.respwanState);

    }
    
        public void ReturnNormal()
    {
        Debug.Log("复活");
        charator.stateMachine.StateChange(charator.IdleState);

    }
}

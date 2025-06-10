using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionDuration = 1.5f;
    private Animator explosionAnim;
    public Transform effectTransform;
    public GameObject boomPosition;
    // Start is called before the first frame update
    void Start()
    {
        explosionAnim = GetComponent<Animator>();
        explosionAnim.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        effectTransform = this.transform;
    }


    public void HideEffect()
    {
        gameObject.SetActive(false);
    }
    public void OnExplosionFinished()
    {
        // ��������ʱִ�е��߼�
        gameObject.SetActive(false); // ���ö��󣨻���������أ�
    }
}

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
        // 动画结束时执行的逻辑
        gameObject.SetActive(false); // 禁用对象（回收至对象池）
    }
}

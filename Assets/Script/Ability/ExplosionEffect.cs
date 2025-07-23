using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour,IPooledObject
{
    public float explosionDuration = 1.5f;
    private Animator explosionAnim;
    public Transform effectTransform;
    public GameObject boomPosition;
    public string explosionTag = "ExplosionAirWave";
    //public GameObject airwave;
    // Start is called before the first frame update
    void Start()
    {
        explosionAnim = GetComponent<Animator>();
        explosionAnim.enabled = true;
        transform.localScale = new Vector3(2f, 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        effectTransform = this.transform;
    }
    public void ExplosionAirWave()
    {
        ObjectPool.Instance.GetFromPool(this.explosionTag,transform.position,transform.rotation);
        SoundManager.instance.PlaySound("explosion");
    }
    public void OnExplosionFinished()
    {
        ObjectPool.Instance.ReturnToPool("ExplosionEffect", gameObject);
    }
    public void OnObjectSpawn()
    {
        if (explosionAnim != null)
        {
            explosionAnim.Play("boom400ppppp", 0);
        }
        //Debug.Log("已重置");
    }
    public void OnObjectReturn()
    {

    }
}

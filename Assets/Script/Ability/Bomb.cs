using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering;
using UnityEngine;

public class Bomb : MonoBehaviour,IPooledObject
{
    [Header("BombSet")]
    [SerializeField] private float bombFuse;
    [SerializeField] private float upwardModifier = 0.3f;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionHurtRadius;
    [SerializeField] private float explosionForce;

    private float remainingTime;

    [Header("TriggerSet")]
    [SerializeField] private float detonationDistance;
    [SerializeField] private LayerMask ground;


    [SerializeField] private float bombActiveTime;
    [SerializeField] private Rigidbody2D rb;

    private bool isExploded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * 2f;
        }

        if ( bombActiveTime >= bombFuse && !isExploded )
        {
            Explosion();
            RecycleBomb();
        }
        if ( ( CheckNearGroundOrWall() || remainingTime <= 0) && !isExploded )
        {
            Explosion();
            RecycleBomb();
            UnityEngine.Debug.Log("是否重置：" + remainingTime);
        }
    }

    private void RecycleBomb()
    {
        if (ObjectPool.Instance == null || !gameObject.activeSelf)
            return;
        ObjectPool.Instance.ReturnToPool("Bomb", gameObject);
    }

    private void CalculateRemainingTime(float holdingTime)
    {
        bombActiveTime = holdingTime;
        remainingTime = bombFuse - holdingTime;
        EventManager.GetDate -= CalculateRemainingTime;  //保证只调用一次就取消订阅，避免remainingTime被丢炸弹动作不断重置。
    }

    public void OnObjectSpawn()
    {
        isExploded = false;
        EventManager.GetDate += CalculateRemainingTime;
        //EventManager.OnPlayerDeath += RecycleBomb;
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(2f, 2f, 2f);
        this.bombActiveTime = 0f;
    }

    public void OnObjectReturn()
    {
        //EventManager.OnPlayerDeath -= RecycleBomb;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;
        rb.isKinematic = false;
        this.bombActiveTime = 0f;
        //Debug.Log("是否重置：" + bombActiveTime);
    }

    private bool CheckNearGroundOrWall() => Physics2D.OverlapCircle(transform.position, detonationDistance, ground);

    private void Explosion()
    {
        #region 消除炸弹物理
        if (rb != null)
        {
            rb.velocity = Vector2.zero;   // 清除速度
            rb.gravityScale = 0f;         // 取消重力影响
            rb.isKinematic = true;        // 改为不受物理影响
        }
        #endregion

        #region 炸弹物理效果
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
            if (hitRb != null && hit.CompareTag("Player"))
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                direction = (direction + Vector2.up * upwardModifier).normalized;
                hitRb.velocity = direction * explosionForce;
            }
        }
        #endregion

        #region 炸弹伤害效果
        Collider2D[] hurtColliders = Physics2D.OverlapCircleAll(transform.position, explosionHurtRadius);
        foreach (Collider2D hit in hurtColliders)
        {
            Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
            if (hitRb != null && hit.CompareTag("Player"))
            {
                EventManager.ExplosionHurtEvent();
            }
        }
        #endregion

        #region 从池中获取炸弹特效
        ObjectPool.Instance.GetFromPool("ExplosionEffect", transform.position, transform.rotation);
        #endregion

        #region 通知爆炸
        isExploded = true;
        EventManager.ExplosionEvent();
        #endregion
    }
}

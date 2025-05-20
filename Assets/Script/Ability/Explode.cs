using UnityEngine;

public class Explode : MonoBehaviour
{
    [Header("explode")]
    [SerializeField] private float explodeForce;    
    [SerializeField] private float explodeRadius;    
    [SerializeField] private float detonationDistance = 0.1f;
    [SerializeField] private LayerMask ground;            
    [SerializeField] private float upwardModifier = 0.3f;
    
    private Rigidbody2D rb;
    private bool hasExploded = false; // 防止重复爆炸

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasExploded && CheckNearGroundOrWall())
        {
            StopBoomMovement(); // 先停止炸弹移动
            Explosion();        // 再触发爆炸
            hasExploded = true; // 标记已爆炸，避免重复执行
        }
    }

    void StopBoomMovement()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;   // 清除速度
            rb.gravityScale = 0f;         // 取消重力影响
            rb.isKinematic = true;        // 改为不受物理影响
        }
    }

    void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
            if (hitRb != null && hit.CompareTag("Player"))
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                direction = (direction + Vector2.up * upwardModifier).normalized;
                hitRb.velocity = direction * explodeForce;
            }
        }
        Destroy(gameObject, 0.1f);
    }

    public bool CheckNearGroundOrWall() => Physics2D.OverlapCircle(transform.position, detonationDistance, ground);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detonationDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
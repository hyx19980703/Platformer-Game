using UnityEngine;

public class Explode : MonoBehaviour
{
    [Header("explode")]
    [SerializeField] private float explodeForce ;    // 冲击波力度
    [SerializeField] private float explodeRadius ;    // 爆炸范围
    [SerializeField] private float detonationDistance = 0.1f; // 触发爆炸距离
    [SerializeField] private LayerMask ground;            // 地面/墙壁层
    [SerializeField] private float upwardModifier = 0.3f; // 垂直方向修正

    void Update()
    {
        if (CheckNearGroundOrWall())
            Explosion();
    }

    void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null && hit.CompareTag("Player"))
            {
                // 计算从炸弹指向玩家的方向
                Vector2 direction = (Vector2)(hit.transform.position - transform.position);

                // 添加可控的垂直分量（保留原始方向）
                direction = (direction.normalized + Vector2.up * upwardModifier).normalized;
                Debug.DrawRay(transform.position, direction * 5, Color.green, 1f);
                //rb.AddForce(direction * explodeForce , ForceMode2D.Impulse);  addforce会导致下落的时候会有一个抵消的效果，可能直接获得一个速度会更好？
                rb.velocity = new Vector2(direction.x * explodeForce, direction.y * explodeForce);
            }
        }
        Destroy(gameObject, 0.1f);
    }

    bool CheckNearGroundOrWall()
    {
        return Physics2D.OverlapCircle(transform.position, detonationDistance, ground);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detonationDistance); // 触发范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);      // 爆炸范围
    }
    
    
}
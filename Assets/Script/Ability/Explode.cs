using UnityEngine;

public class Explode : MonoBehaviour
{
    [Header("explode")]
    [SerializeField] private float explodeForce;    
    [SerializeField] private float explodeRadius;    
    [SerializeField] private float detonationDistance = 0.1f;
    [SerializeField] private LayerMask ground;            
    [SerializeField] private float upwardModifier = 0.3f;
    private float remainingTime;
    private float bombFuseTime;

    private Rigidbody2D rb;
    private GameObject player;
    private Charator playerScript;
    private GameObject currentExplosion; // 当前激活的爆炸特效
    private bool hasExploded = false; // 防止重复爆炸

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
     void OnEnable()
    {
        bombFuseTime = 1.5f;
        player = GameObject.Find("Charactor");
        if (player != null)
        {
            playerScript = player.GetComponent<Charator>();
            remainingTime = bombFuseTime - playerScript.holdBombTimer;
        }
    }


    void Update()
    {

        //Debug.Log("手持时长：" + playerScript.holdBombTimer);

        if (playerScript != null && playerScript.holdBombTimer >= playerScript.maxHoldTime)
        {
            TriggerExplosionEffect(transform.position); // 触发爆炸特效
            Explosion();        // 再触发爆炸
            playerScript.holdBombTimer = 0f;//重置人物手持炸弹冷却
            PrefabList.prefabList.RetrunObject(gameObject);
        }
        if ( remainingTime> 0)
        {
             remainingTime -= Time.deltaTime*2;
        }
        if (!hasExploded && (remainingTime <= 0||CheckNearGroundOrWall()))
        {
            StopBoomMovement(); // 先停止炸弹移动
            TriggerExplosionEffect(transform.position); // 触发爆炸特效
            Explosion();        // 再触发爆炸
            hasExploded = true; // 标记已爆炸，避免重复执行
            playerScript.holdBombTimer = 0f;    
            PrefabList.prefabList.RetrunObject(gameObject);
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
        


        //transform.localScale = new Vector3(5, 5, 5);

    }
    //public void SetHoldTime(float holdTime)
    //{
    //    remainingTime = 1.5f - holdTime;
    //}
    public void TriggerExplosionEffect(Vector2 explosionPosition)
    {
        // 从对象池获取特效
        currentExplosion = ExplosionPoolManager.instance.GetExplosion();

        if (currentExplosion != null)
        {
            currentExplosion.transform.position = explosionPosition;
            currentExplosion.transform.rotation = Quaternion.identity;
            currentExplosion.SetActive(true);

            // 获取Animator组件并播放动画
            Animator animator = currentExplosion.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("boom400ppppp");

                // 计算动画时长并在结束时回收特效
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("ReturnExplosionToPool", animationLength);
            }
        }
    }
    void ReturnExplosionToPool()
    {
        if (currentExplosion != null)
        {
            ExplosionPoolManager.instance.ReturnExplosion(currentExplosion);
        }
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
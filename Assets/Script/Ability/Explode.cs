using UnityEngine;

public class Explode : MonoBehaviour,IPooledObject
{
    [Header("explode")]
    [SerializeField] private float explodeForce;    
    [SerializeField] private float explodeRadius;
    [SerializeField] private float explodeHurtRadius;
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
    private bool hurt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    // void OnEnable()
    //{
    //}


    void Update()
    {
        if(hurt)
        {
            //EventManager.OtherEvent();
            hurt = false;
        }

        if (playerScript != null && playerScript.holdBombTimer >= playerScript.maxHoldTime)
        {
            TriggerExplosionEffect(transform.position); // 触发爆炸特效
            ExplosionHurt();
            Explosion();        // 再触发爆炸
            //EventManager.ExplosionInfEvent(transform.position, 2, 0.2f);
            playerScript.holdBombTimer = 0f;//重置人物手持炸弹冷却
            //PrefabList.prefabList.RetrunObject(gameObject);
            ObjectPool.Instance.ReturnToPool("Bomb", gameObject); 
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
            ExplosionHurt();   //触发爆炸伤害
            //EventManager.ExplosionInfEvent(transform.position,1, 0.2f);
            hasExploded = true; // 标记已爆炸，避免重复执行
            playerScript.holdBombTimer = 0f;
            //PrefabList.prefabList.RetrunObject(gameObject);
            ObjectPool.Instance.ReturnToPool("Bomb", gameObject);

        }
        //Debug.Log("手持时长：" + playerScript.holdBombTimer);
        Debug.Log("是否造成伤害：" + hurt);
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explodeRadius);
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
    void ExplosionHurt()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeHurtRadius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D hitRb = hit.GetComponent<Rigidbody2D>();
            if (hitRb != null && hit.CompareTag("Player"))
            {
                hurt = true;
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
        //currentExplosion = ExplosionPoolManager.instance.GetExplosion();
        currentExplosion = ObjectPool.Instance.GetFromPool("ExplosionEffect", explosionPosition, transform.rotation);
        //currentExplosion.SetActive(true);
    }
    public bool CheckNearGroundOrWall() => Physics2D.OverlapCircle(transform.position, detonationDistance, ground);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detonationDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

    private void OnDisable()
    {
        
    }
    public void OnObjectSpawn()
    {
        hurt = false;
        bombFuseTime = 1.5f;
        player = GameObject.Find("Charactor");
        if (player != null)
        {
            playerScript = player.GetComponent<Charator>();
            remainingTime = bombFuseTime - playerScript.holdBombTimer;
        }
    }

    public void OnObjectReturn()
    {
        hasExploded = false;
    }
}
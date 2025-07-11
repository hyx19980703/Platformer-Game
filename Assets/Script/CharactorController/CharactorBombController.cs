using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharactorBombController : MonoBehaviour
{
   
    public Charator charactor;
    private GameObject currentBomb;
    private bool isHoldingBomb = false;  // 是否持有炸弹，作用是补全逻辑，这样代码不容易出错，这里也能删除，但是建议保留
    private float holdingBombTimer = 0;

    [Header("References")]
    [SerializeField] private Transform bombHoldPosition; // 手持炸弹的位置
    [SerializeField] private float throwSpeed = 30f;     // 投掷速度

    [Header("Bomb Settings")]
    [SerializeField] private float bombHoldHeight = 0.5f; // 手持时炸弹的高度偏移

    void Start()
    {
        charactor = GetComponent<Charator>();
        EventManager.OnExplosion += RefreshHoldingState;
        EventManager.OnPlayerDeath += RefreshHoldingState;
    }

    void Update()
    {
        HandleBombInput();
        //Debug.Log("手持炸弹时间：" + holdingBombTimer);
    }

    void HandleBombInput()
    {
        // 按下鼠标左键时拿出炸弹
        if (Input.GetMouseButtonDown(0) && !isHoldingBomb)
        {
            TakeBombFromPool();
        }

        // 按住鼠标时调整炸弹位置（跟随手部）
        if (isHoldingBomb && Input.GetMouseButton(0))
        {
            holdingBombTimer += Time.deltaTime;
            charactor.anim.SetLayerWeight(1, 1);
            UpdateBombPosition();
            if (holdingBombTimer >= 1.5f)
            {
                EventManager.SentDate(holdingBombTimer);
                currentBomb.SetActive(true);
                isHoldingBomb = false;
                holdingBombTimer = 0f;

            }
        }

        // 松开鼠标时投掷炸弹
        if (Input.GetMouseButtonUp(0) && isHoldingBomb)
        {
            EventManager.SentDate(holdingBombTimer);
            charactor.anim.SetLayerWeight(1, 0);
            ThrowBomb(MousePositon.instance.mousePos);
        }
    }

    void TakeBombFromPool()
    {
        #region 从池中获取炸弹
        currentBomb = ObjectPool.Instance.GetFromPool("Bomb", transform.position, transform.rotation);
        currentBomb.transform.position = bombHoldPosition.position + Vector3.up * bombHoldHeight;
        currentBomb.transform.rotation = Quaternion.identity;
        #endregion

        #region 暂时不激活炸弹
        //Rigidbody2D bombRb = currentBomb.GetComponent<Rigidbody2D>();
        //if (bombRb != null)
        //{
        //    bombRb.isKinematic = true;
        //}
        currentBomb.SetActive(false);
        #endregion

        #region 更新人物持炸弹状态
        isHoldingBomb = true;
        #endregion
    }

    void UpdateBombPosition()
    {
        if (currentBomb == null) return;
        // 让炸弹跟随手持位置
        currentBomb.transform.position = bombHoldPosition.position + Vector3.up * bombHoldHeight;
    }
    void ThrowBomb(Vector2 _mousePositon)
    {
        #region 安全检查
        if (currentBomb == null) return;
        #endregion

        #region 激活炸弹
        currentBomb.SetActive(true);
        //currentBomb.GetComponent<SpriteRenderer>().enabled = true;
        //currentBomb.transform.position = transform.position;
        //if (bombRb != null)
        //{
        //    bombRb.isKinematic = false;
        //}
        #endregion

        #region 人物丢炸弹的方法以及进入扔炸弹状态
        Rigidbody2D bombRb = currentBomb.GetComponent<Rigidbody2D>();
        charactor.stateMachine.StateChange(charactor.throwState);
        Vector2 thrownDir = (_mousePositon - charactor.rb.position).normalized;
        bombRb.velocity = thrownDir * throwSpeed;
        #endregion

        #region 丢炸弹时应用人物翻转
        if ((_mousePositon.x > charactor.rb.position.x) && !charactor.isFacingRight)
        {
            charactor.facingDir = charactor.facingDir * -1;
            transform.Rotate(0, 180, 0);
            charactor.isFacingRight = true;
        }
        else if ((_mousePositon.x < charactor.rb.position.x) && charactor.isFacingRight)
        {
            charactor.facingDir = charactor.facingDir * -1;
            transform.Rotate(0, 180, 0);
            charactor.isFacingRight = false;
        }
        #endregion

        #region 重置人物手持炸弹状态
        currentBomb = null;
        isHoldingBomb = false;
        #endregion
    }
    void RefreshHoldingState()//爆炸时重置持炸弹状态
    {
        holdingBombTimer = 0f;
        //currentBomb = null;
        //isHoldingBomb = false;
        //Debug.Log("炸弹是否重置"+isHoldingBomb);
    }
    
}

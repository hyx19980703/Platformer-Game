using UnityEngine;

public class PoolObjectID : MonoBehaviour
{
    public string poolTag;
    private bool isInitialized = false;
    public int instanceId; // 新增实例ID字段

    void Awake()
    {
        // 延迟到下一帧才允许自动回收
        StartCoroutine(DelayedInitialization());
    }

    System.Collections.IEnumerator DelayedInitialization()
    {
        yield return null; // 等待一帧
        isInitialized = true;
    }

    void OnDisable()
    {
        if (!isInitialized) return;

        if (ObjectPool.Instance != null && !string.IsNullOrEmpty(poolTag))
        {
            ObjectPool.Instance.ReturnToPool(poolTag, gameObject);
        }
    }
}
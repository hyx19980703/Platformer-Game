using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPoolManager : MonoBehaviour
{
    public static ExplosionPoolManager instance;

    [SerializeField] private GameObject explosionPrefab; // 爆炸特效预制体
    [SerializeField] private int initialPoolSize = 5;    // 初始池大小

    private List<GameObject> explosionPool = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    // 初始化对象池
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewExplosion();
        }
    }

    // 创建新的爆炸特效实例
    private GameObject CreateNewExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.SetActive(false);
        explosion.transform.SetParent(transform); // 设为管理器的子对象
        explosionPool.Add(explosion);
        return explosion;
    }

    // 从池中获取特效
    public GameObject GetExplosion()
    {
        // 查找未激活的对象
        foreach (GameObject explosion in explosionPool)
        {
            if (!explosion.activeInHierarchy)
            {
                return explosion;
            }
        }

        // 如果池已满，创建新对象
        return CreateNewExplosion();
    }

    // 回收特效到池中
    public void ReturnExplosion(GameObject explosion)
    {
        explosion.SetActive(false);
    }
}
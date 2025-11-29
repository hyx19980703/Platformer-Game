using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // 单例模式

    [System.Serializable]
    public class Pool
    {
        public string tag; // 对象标识
        public GameObject prefab; // 预制体
        public int size; // 池大小
    }

    public List<Pool> pools; // 多个对象池
    public Dictionary<string, Queue<GameObject>> poolDictionary{get; set;}// 对象池字典

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    // 初始化所有对象池
    private void InitializePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                var poolID = obj.AddComponent<PoolObjectID>();
                poolID.poolTag = pool.tag;
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // 从池中获取对象（主要方法）
    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"没有找到标签为 {tag} 的对象池");
            return null;
        }

        // 如果池空了，自动扩展（可选）
        if (poolDictionary[tag].Count == 0)
        {
            Debug.LogWarning($"池中没有对象了，请等待回收");

            ExpandPool(tag, 1); // 扩展5个新实例
        }



        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // 调用对象的OnSpawn方法（如果有）
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }

    // 扩展对象池
    private void ExpandPool(string tag, int amount)
    {
        Pool pool = pools.Find(p => p.tag == tag);
        if (pool == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            var poolID = obj.AddComponent<PoolObjectID>();
            obj.transform.SetParent(transform);
            poolID.poolTag = pool.tag;
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
    // 在ObjectPool类中添加这个方法
    public void DestroyFromPool(string tag, GameObject objToDestroy)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"没有找到标签为 {tag} 的对象池");
            Destroy(objToDestroy);
            return;
        }
        poolDictionary[tag].Clear();
        Destroy(objToDestroy);
    }

    // 将对象返回池中
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        var poolID = objectToReturn.GetComponent<PoolObjectID>();
        if (poolID == null)
        {
            Destroy(objectToReturn);
            return;
        }

        objectToReturn.SetActive(false);

        IPooledObject pooledObject = objectToReturn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectReturn();
        }


        poolDictionary[tag].Enqueue(objectToReturn);
    }

    public int GetAvailableObjectCount(string tag)
{
    if (poolDictionary.ContainsKey(tag))
    {
        // 队列的 Count 就是当前可用的对象数量
        return poolDictionary[tag].Count;
    }
    Debug.LogWarning($"没有找到标签为 {tag} 的对象池");
    return -1;
}
}


// 对象接口，用于初始化
public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectReturn();
}
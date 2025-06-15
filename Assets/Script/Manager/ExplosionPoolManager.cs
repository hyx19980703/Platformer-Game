using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPoolManager : MonoBehaviour
{
    public static ExplosionPoolManager instance;

    [SerializeField] private GameObject explosionPrefab; // ��ը��ЧԤ����
    [SerializeField] private int initialPoolSize = 5;    // ��ʼ�ش�С

    private List<GameObject> explosionPool = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        InitializePool();
    }

    // ��ʼ�������
    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewExplosion();
        }
    }

    // �����µı�ը��Чʵ��
    private GameObject CreateNewExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.SetActive(false);
        explosion.transform.SetParent(transform); // ��Ϊ���������Ӷ���
        explosionPool.Add(explosion);
        return explosion;
    }

    // �ӳ��л�ȡ��Ч
    public GameObject GetExplosion()
    {
        // ����δ����Ķ���
        foreach (GameObject explosion in explosionPool)
        {
            if (!explosion.activeInHierarchy)
            {
                return explosion;
            }
        }

        // ����������������¶���
        return CreateNewExplosion();
    }

    // ������Ч������
    public void ReturnExplosion(GameObject explosion)
    {
        explosion.SetActive(false);
    }
}
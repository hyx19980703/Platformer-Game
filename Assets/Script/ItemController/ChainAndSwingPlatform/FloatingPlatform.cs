using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class FloatingPlatform : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatHeight = 0.5f; // 浮动高度
    public float floatSpeed = 1f; // 浮动速度
    public float rotationAmount = 2f; // 旋转幅度

    [Header("Chain Connection Points")]
    public Transform[] chainConnectionPoints; // 铁链连接点数组

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float randomOffset;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        randomOffset = Random.Range(0f, 2f * Mathf.PI); // 随机偏移使平台不同步

        // 如果没有设置连接点，自动使用平台四角
        if (chainConnectionPoints == null || chainConnectionPoints.Length == 0)
        {
            AutoSetupConnectionPoints();
        }
    }

    private void AutoSetupConnectionPoints()
    {
        // 获取Tilemap边界
        var bounds = GetComponent<TilemapCollider2D>().bounds;

        // 创建四个角的连接点
        chainConnectionPoints = new Transform[4];
        chainConnectionPoints[0] = CreateConnectionPoint(bounds.min.x, bounds.min.y);
        chainConnectionPoints[1] = CreateConnectionPoint(bounds.max.x, bounds.min.y);
        chainConnectionPoints[2] = CreateConnectionPoint(bounds.min.x, bounds.max.y);
        chainConnectionPoints[3] = CreateConnectionPoint(bounds.max.x, bounds.max.y);
    }

    private Transform CreateConnectionPoint(float x, float y)
    {
        GameObject point = new GameObject("ChainConnectionPoint");
        point.transform.SetParent(transform);
        point.transform.position = new Vector3(x, y, 0);
        return point.transform;
    }

    private void Update()
    {
        // 基础浮动效果
        float yOffset = Mathf.Sin((Time.time + randomOffset) * floatSpeed) * floatHeight;
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        // 轻微旋转
        float rotation = Mathf.Sin((Time.time + randomOffset) * floatSpeed * 0.7f) * rotationAmount;
        transform.rotation = startRotation * Quaternion.Euler(0, 0, rotation);
    }
}
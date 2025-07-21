using UnityEngine;
using UnityEngine.Tilemaps;

public class AdvancedPendulum : MonoBehaviour
{
    [Header("Pendulum Settings")]
    public Vector2 pivotPoint;
    public float swingAngle = 0.3f;
    public float swingSpeed = 1f;
    public ChainTopCalculator leftChain;
    public ChainTopCalculator rightChain;
    public Tilemap tilemap;
    public Vector2 centerPos;

    [Header("Disturbance Settings")]
    public float disturbanceMultiplier = 5f; // 爆炸冲击强度系数
    public float damping = 0.5f;            // 阻尼系数（0-1）
    public float recoverySpeed = 2f;        // 恢复速度
    public float maxSwingAngle;

    private float timer;
    private float currentAngle;
    private float disturbanceAngle;         // 扰动角度偏移
    private float angularVelocity;          // 当前角速度

    private void Start()
    {
        EventManager.OnAirWaveSpread += AddExplosionForce;
    }
    void Update()
    {
        centerPos = GetTilemapCenter();
        // 基础钟摆运动（正弦波）
        timer += Time.deltaTime;
        float targetAngle = Mathf.Sin(timer * swingSpeed) * swingAngle;

        pivotPoint = new Vector2(
            (leftChain.topWorldPos.x + rightChain.topWorldPos.x) / 2, 
            (leftChain.topWorldPos.y + rightChain.topWorldPos.y) / 2
            );
        // 应用扰动（物理模拟）
        ApplyPhysicsDisturbance();

        // 平滑旋转到目标角度
        currentAngle = Mathf.LerpAngle(
            currentAngle,
            targetAngle + disturbanceAngle,
            recoverySpeed * Time.deltaTime
        );

        // 执行旋转
        transform.RotateAround(
            new Vector3(pivotPoint.x, pivotPoint.y, 0),
            Vector3.forward,
            currentAngle - transform.rotation.eulerAngles.z
        );
    }
    public Vector2 GetTilemapCenter()
    {
        // 1. 获取所有有Tile的单元格位置
        tilemap.CompressBounds(); // 确保边界计算准确
        BoundsInt bounds = tilemap.cellBounds;

        // 2. 计算所有Tile的中心点（世界坐标）
        Vector3 sum = Vector3.zero;
        int count = 0;

        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))
                {
                    sum += tilemap.GetCellCenterWorld(cellPos);
                    count++;
                }
            }
        }

        // 3. 返回平均值（几何中心）
        return count > 0 ? sum / count : transform.position;
    }
    // 物理扰动模拟
    void ApplyPhysicsDisturbance()
    {
        // 角速度阻尼
        angularVelocity *= (1 - damping * Time.deltaTime);

        // 扰动角度更新
        disturbanceAngle += angularVelocity * Time.deltaTime;

        // 扰动恢复力（类似弹簧）
        disturbanceAngle = Mathf.Lerp(disturbanceAngle, 0, recoverySpeed * Time.deltaTime);

        // 限制最大扰动角度
        disturbanceAngle = Mathf.Clamp(disturbanceAngle, -maxSwingAngle, maxSwingAngle);
    }

    // 外部调用：添加爆炸冲击
    public void AddExplosionForce(float force, Vector2 explosionPosition)
    {
        // 计算冲击方向（基于爆炸位置）
        Vector2 dir = (centerPos - explosionPosition).normalized;

        // 计算扭矩（叉积确定旋转方向）
        float torque = Vector3.Cross(dir, Vector3.up).z;

        // 应用角速度冲击
        angularVelocity += torque * force * disturbanceMultiplier;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(centerPos, 0.8f);
    //}
}
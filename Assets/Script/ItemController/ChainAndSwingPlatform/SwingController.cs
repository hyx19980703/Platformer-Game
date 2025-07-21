using UnityEngine;
using UnityEngine.Tilemaps;

public class SwingController : MonoBehaviour
{
    [Header("Pendulum Settings")]
    public Vector2 pivotPoint;              // 摆动旋转轴
    public ChainTopCalculator leftChain;
    public ChainTopCalculator rightChain;
    public Tilemap tilemap;
    public Vector2 centerPos;               // tile构成的平台的几何中心，用于判断爆炸施加力的方向

    [Header("Disturbance Settings")]
    public float disturbanceMultiplier = 5f;
    public float damping = 0.5f;            // 阻尼系数（0-1）,越大越快恢复原状
    public float gravity = 9.8f;            // 重力加速度
    public float maxSwingAngle = 45f;       // 受到爆炸后最大摆动角度

    //private float timer;
    private float currentAngle;
    private float angularVelocity;          // 当前角速度

    private void Start()
    {
        EventManager.OnAirWaveSpread += AddExplosionForce;
        currentAngle = 0;
        angularVelocity = 0;
    }

    void Update()
    {
        centerPos = GetTilemapCenter();
        pivotPoint = new Vector2(
            (leftChain.topWorldPos.x + rightChain.topWorldPos.x) / 2,
            (leftChain.topWorldPos.y + rightChain.topWorldPos.y) / 2
        );

        // 物理模拟更新
        UpdatePendulumPhysics();

        // 执行旋转
        transform.RotateAround(
            new Vector3(pivotPoint.x, pivotPoint.y, 0),
            Vector3.forward,
            currentAngle - transform.rotation.eulerAngles.z
        );
    }

    void UpdatePendulumPhysics()
    {
        // 计算重力扭矩（恢复力）
        float gravityTorque = -Mathf.Sin(currentAngle * Mathf.Deg2Rad) * gravity;

        // 更新角速度（考虑阻尼）
        angularVelocity += gravityTorque * Time.deltaTime;
        angularVelocity *= (1 - damping * Time.deltaTime);

        // 更新角度
        currentAngle += angularVelocity * Mathf.Rad2Deg * Time.deltaTime;

        // 限制最大角度
        currentAngle = Mathf.Clamp(currentAngle, -maxSwingAngle, maxSwingAngle);
    }

    public Vector2 GetTilemapCenter() //获取由tile构成的tilemap对象的几何中心的方法
    {
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;

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

        return count > 0 ? sum / count : transform.position;
    }

    public void AddExplosionForce(float force, Vector2 explosionPosition) //注册为事件订阅者
    {
        // 计算冲击方向
        Vector2 dir = (centerPos - explosionPosition).normalized;

        // 计算扭矩（叉积确定旋转方向）
        float torque = Vector3.Cross(dir, Vector3.up).z;

        // 应用角速度冲击
        angularVelocity += torque * force * disturbanceMultiplier;
    }
}
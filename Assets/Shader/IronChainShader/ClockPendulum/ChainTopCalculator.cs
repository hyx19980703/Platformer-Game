// 挂载到Tilemap对象
using UnityEngine.Tilemaps;
using UnityEngine;
using Unity.VisualScripting;

public class ChainTopCalculator : MonoBehaviour
{
    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public Vector2 topWorldPos; // 存储计算结果，此为旋转中心点
    public Vector2 bottomWorldPos;
    public SwingController swingPlatteController;
    public Vector2 centerPos; //获取由tile组成的整体的几何中心

    [Header("Disturbance Settings")]
    public float disturbanceMultiplier = 5f; // 爆炸冲击强度系数
    public float damping = 0.5f;            // 阻尼系数（0-1）
    public float gravity = 9.8f;            // 重力加速度
    public float maxSwingAngle;

    //public float recoverySpeed = 2f;        // 恢复速度

    //private float timer;
    private float currentAngle;
    private float disturbanceAngle;         // 扰动角度偏移
    private float angularVelocity;          // 当前角速度

    void Start()
    {
        EventManager.OnAirWaveSpread += AddExplosionForce;
        bottomWorldPos = GetTilemapBottom();
        topWorldPos = GetTilemapTop();
    }
    private void Update()
    {
        if (swingPlatteController != null)
        {
            centerPos = swingPlatteController.centerPos;
        }
        if (swingPlatteController == null)
        {
            centerPos = (bottomWorldPos + topWorldPos) / 2;
        }
        UpdatePendulumPhysics();
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        if (props != null)
        {
            props.SetVector("_RotatePos", new Vector4(topWorldPos.x, topWorldPos.y, 0, 0));
            props.SetFloat("_CurrentAngle", currentAngle);
            tilemapRenderer.SetPropertyBlock(props);
        }
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

    public Vector2 GetTilemapTop()
    {
        // 获取所有有Tile的位置
        var bounds = tilemap.cellBounds;
        float maxY = float.MinValue;
        Vector3 topPos = Vector3.zero;

        // 扫描所有Tile，找到Y值最大的顶点
        for (int y = bounds.yMax; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x <= bounds.xMax; x++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    var worldPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));
                    if (worldPos.y > maxY)
                    {
                        maxY = worldPos.y;
                        topPos = worldPos;
                    }
                    break; // 找到该列最高点就跳出
                }
            }
        }
        return topPos;
    }

    public Vector2 GetTilemapBottom()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3 lowestBottomPoint = new Vector3(0, float.MaxValue, 0);

        // 扫描所有Tile
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    // 获取Tile的世界坐标（中心点）
                    Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(x, y, 0));

                    // 计算Tile的底部位置（假设Tile高度为1单位）
                    Vector3 tileBottom = worldPos - new Vector3(0, 0.5f, 0);

                    // 记录最低的底部点
                    if (tileBottom.y < lowestBottomPoint.y)
                    {
                        lowestBottomPoint = tileBottom;
                    }
                    break; // 找到该列第一个tile就跳出
                }
            }
        }
        return new Vector2(lowestBottomPoint.x, lowestBottomPoint.y-0.5f/3);
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


    //// 物理扰动模拟
    //void ApplyPhysicsDisturbance()
    //{
    //    // 角速度阻尼
    //    angularVelocity *= (1 - damping * Time.deltaTime);

    //    // 扰动角度更新
    //    disturbanceAngle += angularVelocity * Time.deltaTime;

    //    // 扰动恢复力（类似弹簧）
    //    disturbanceAngle = Mathf.Lerp(disturbanceAngle, 0, recoverySpeed * Time.deltaTime);

    //    // 限制最大扰动角度
    //    disturbanceAngle = Mathf.Clamp(disturbanceAngle, -maxSwingAngle, maxSwingAngle);
    //}
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

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(topWorldPos, 0.2f);
        Gizmos.color = Color.red;
        // 在计算出的topWorldPos画十字线
        Gizmos.DrawLine(bottomWorldPos + Vector2.left * 0.5f, bottomWorldPos + Vector2.right * 0.5f);
        Gizmos.DrawLine(bottomWorldPos + Vector2.up * 0.1f, bottomWorldPos + Vector2.down * 0.1f);
        Gizmos.DrawLine(topWorldPos + Vector2.left * 0.5f, topWorldPos + Vector2.right * 0.5f);
        Gizmos.DrawLine(topWorldPos + Vector2.up * 0.1f, topWorldPos + Vector2.down * 0.1f);
        Gizmos.DrawLine(centerPos + Vector2.left * 0.5f, centerPos + Vector2.right * 0.5f);
        Gizmos.DrawLine(centerPos + Vector2.up * 0.1f, centerPos + Vector2.down * 0.1f);
    }

}
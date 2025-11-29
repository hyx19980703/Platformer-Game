using UnityEngine;
using UnityEngine.UI;

public class RadialLightController : MonoBehaviour
{
    public Transform playerTransform; // 引用你的角色Transform
    public float lightRadius = 5.0f;
    public float softness = 1.0f;

    private Material maskMaterial;

    void Start()
    {
        // 获取Image组件上的材质实例
        maskMaterial = GetComponent<Image>().material;
    }

    void Update()
    {
        if (playerTransform != null && maskMaterial != null)
        {
            // 将玩家的世界位置传递给Shader
            maskMaterial.SetVector("_PlayerPosition", playerTransform.position);
            
            // 你可以在这里动态修改半径和柔边
            maskMaterial.SetFloat("_Radius", lightRadius);
            maskMaterial.SetFloat("_Softness", softness);
        }
    }

    // 可选：当对象被销毁时，释放材质实例，防止内存泄漏

}
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class WaterController : MonoBehaviour
{
    [Header("Water Settings")]
    public float maxWaveHeight = 0.3f;
    public float rippleDuration = 2.5f;
    public float minExplosionDistance = 0.5f;
    public float maxEffectRadius = 5f;

    [Header("Visual Effects")]
    public ParticleSystem splashParticles;
    public GameObject splashPrefab;

    private Material waterMaterial;
    private List<ActiveExplosion> activeExplosions = new List<ActiveExplosion>();

    // 当前活动的爆炸
    private class ActiveExplosion
    {
        public Vector2 position;
        public float startTime;
        public float intensity;
        public float radius;
    }

    void Start()
    {
        // 获取水材质
        Renderer renderer = GetComponent<Renderer>();
        waterMaterial = renderer.material;
    }

    void Update()
    {
        // 更新所有活动的爆炸效果
        for (int i = activeExplosions.Count - 1; i >= 0; i--)
        {
            ActiveExplosion explosion = activeExplosions[i];
            float elapsed = Time.time - explosion.startTime;

            // 移除过期的爆炸
            if (elapsed > rippleDuration)
            {
                activeExplosions.RemoveAt(i);
                continue;
            }

            // 更新Shader参数（这里只处理最近的一个爆炸以获得最佳性能）
            if (i == activeExplosions.Count - 1)
            {
                waterMaterial.SetVector("_ExplosionCenter", explosion.position);
                waterMaterial.SetFloat("_ExplosionIntensity", explosion.intensity);
                waterMaterial.SetFloat("_ExplosionTime", explosion.startTime);
                waterMaterial.SetFloat("_ExplosionRadius", explosion.radius);
            }
        }
    }

    /// <summary>
    /// 处理爆炸事件
    /// </summary>
    /// <param name="explosionPosition">爆炸位置</param>
    /// <param name="explosionStrength">爆炸强度(0-1)</param>
    public void HandleExplosion(Vector2 explosionPosition, float explosionStrength)
    {
        // 将爆炸位置转换到水面空间
        Vector2 waterPosition = transform.InverseTransformPoint(explosionPosition);

        // 检查爆炸是否在水面范围内
        if (Mathf.Abs(waterPosition.y) > minExplosionDistance)
            return;

        // 计算强度（基于到水面的距离）
        float distanceFactor = 1 - Mathf.Clamp01(Mathf.Abs(waterPosition.y) / minExplosionDistance);
        float intensity = explosionStrength * distanceFactor;

        if (intensity < 0.1f) return;

        // 创建新的爆炸记录
        ActiveExplosion newExplosion = new ActiveExplosion()
        {
            position = explosionPosition,
            startTime = Time.time,
            intensity = intensity,
            radius = Mathf.Lerp(1f, maxEffectRadius, intensity)
        };

        activeExplosions.Add(newExplosion);

        // 生成水花效果
        SpawnSplashEffect(explosionPosition, intensity);
    }

    private void SpawnSplashEffect(Vector2 position, float intensity)
    {
        // 粒子系统水花
        if (splashParticles != null)
        {
            splashParticles.transform.position = position;
            var emission = splashParticles.emission;
            emission.rateOverTime = intensity * 100f;
            splashParticles.Play();
        }

        // 瞬时水花特效
        if (splashPrefab != null)
        {
            GameObject splash = Instantiate(splashPrefab, position, Quaternion.identity);
            splash.transform.localScale = Vector3.one * intensity;

            // 自动销毁
            Destroy(splash, 2f);
        }
    }

    // 注册到爆炸事件（根据您的事件系统修改）
    void OnEnable()
    {
        //EventManager.ExplosionInfluence += HandleExplosion;
        // 示例：YourEventSystem.OnExplosion += HandleExplosion;
    }

    void OnDisable()
    {
        //EventManager.ExplosionInfluence -= HandleExplosion;
        // 示例：YourEventSystem.OnExplosion -= HandleExplosion;
    }
}
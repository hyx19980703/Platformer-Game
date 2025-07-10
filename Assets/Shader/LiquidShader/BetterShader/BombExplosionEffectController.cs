using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BombExplosionEffectController : MonoBehaviour
{
    [Header("Shader Parameters")]
    [SerializeField] private Material explosionMaterialTemplate;
    [SerializeField] private float effectDuration = 1.0f;
    [SerializeField] private float maxDistortion = 0.5f;
    [SerializeField] private float waveSpeed = 2.0f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private Material _runtimeMaterial;
    private SpriteRenderer _renderer;
    private float _timer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        // 实例化独立材质
        _runtimeMaterial = new Material(explosionMaterialTemplate);
        _renderer.material = _runtimeMaterial;
        _renderer.enabled = false;
    }
    private void Start()
    {
        //EventManager.ExplosionInfluence += TriggerEffect;

    }

    void Update()
    {

        if (!_renderer.enabled) return;

        _timer += Time.deltaTime;
        float progress = Mathf.Clamp01(_timer / effectDuration);
        float fadeValue = fadeCurve.Evaluate(progress);

        // 更新Shader参数
        _runtimeMaterial.SetFloat("_FadeOut", 1 - fadeValue);
        _runtimeMaterial.SetFloat("_DistortionStrength", maxDistortion * fadeValue);

        if (_timer >= effectDuration)
        {
            _renderer.enabled = false;
        }
    }

    public void TriggerEffect(Vector2 position, float radius)
    {
        transform.position = position;
        transform.localScale = Vector3.one * radius * 2;

        _timer = 0f;
        _runtimeMaterial.SetFloat("_FadeOut", 0);
        _runtimeMaterial.SetFloat("_DistortionStrength", maxDistortion);
        _renderer.enabled = true;
    }
}
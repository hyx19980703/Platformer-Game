using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _shockWaveTime = 0.75f;

    private Coroutine _shockWaveCoroutine;

    private Material _material;

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > _shockWaveTime)
        {
            ObjectPool.Instance.ReturnToPool("ExplosionAirWave", gameObject);
        }
    }

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }
    public void CallShockWave(float startPos, float endPos)
    {
        if (_shockWaveCoroutine != null)
        {
            StopCoroutine(_shockWaveCoroutine);
        }
        _shockWaveCoroutine = StartCoroutine(ShockWaveRoutine(startPos, endPos));
    }
    private IEnumerator ShockWaveRoutine(float startPos, float endPos)
    {
        _material.SetFloat("_WaveDistanceFromCenter", startPos);

        float elapsedTime = 0f;
        while (elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / _shockWaveTime);
            _material.SetFloat("_WaveDistanceFromCenter", lerpedAmount);
            yield return null; // 等待下一帧
            //Debug.Log($"DeltaTime: {Time.deltaTime}, Elapsed: {elapsedTime}, Lerped: {lerpedAmount}");
        }
    }
    public void OnObjectSpawn()
    {
        timer = 0f;
        CallShockWave(-0.1f, 1f);
        EventManager.AirWaveEvent(10f, transform.position);
    }
    public void OnObjectReturn()
    {
        timer = 0f;
        _material.SetFloat("_WaveDistanceFromCenter", -0.1f);
    }

}

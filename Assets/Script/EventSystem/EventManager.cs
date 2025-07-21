using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // 定义按键事件类型（参数为按键名）
    public static event System.Action OnKeyPressed;

    public static event System.Action OnExplosionHurt;

    public static event System.Action OnExplosion;

    public static event System.Action OnPlayerDeath;

    public static event System.Action<float,Vector2> OnAirWaveSpread;

    public static event System.Action<float> GetDate;

    // 触发事件的方法
    public static void TriggerKeyPressedEvent()
    {
        OnKeyPressed?.Invoke();
    }
    public static void ExplosionHurtEvent()
    {
        OnExplosionHurt?.Invoke();
    }
    public static void PlayDeathEvent()
    {
        OnPlayerDeath?.Invoke();
    }
    public static void ExplosionEvent()
    {
        OnExplosion?.Invoke();
    }
    public static void AirWaveEvent(float force , Vector2 airWaveCenter)
    {
        OnAirWaveSpread?.Invoke(force, airWaveCenter);
    }
    public static void SentDate(float x)
    {
        GetDate?.Invoke(x);
    }
}
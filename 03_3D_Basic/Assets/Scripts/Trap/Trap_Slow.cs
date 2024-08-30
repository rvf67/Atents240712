using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Slow : TrapBase
{
    /// <summary>
    /// 슬로우 지속 시간
    /// </summary>
    public float slowDuration = 5.0f;

    /// <summary>
    /// 느려지는 비율(10%~100%)
    /// </summary>
    [Range(0.1f, 1f)]
    public float slowRate = 0.5f;

    /// <summary>
    /// 함정에 영향을 받는 플레이어
    /// </summary>
    Player target = null;

    ParticleSystem ps;
    Light effectLight;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        ps = child.GetComponent<ParticleSystem>();
        child = child.GetChild(1);
        effectLight = child.GetComponent<Light>();        
    }

    private void Start()
    {
        effectLight.enabled = false;    // CFXR_Effect가 OnEnable에서 자식 조명을 켜고 있기 때문에 Start에서 처리
    }

    protected override void OnTrapActivate(GameObject target)
    {
        effectLight.enabled = true;
        ps.Play();

        this.target = target.GetComponent<Player>();
        this.target.SetSlowDebuf(slowRate);         // 슬로우 디버프 설정
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ps.Stop();
            effectLight.enabled = false;

            target?.RemoveSlowDebuf(slowDuration);  // 슬로우 디버프 해제
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : TrapBase
{
    // 밟으면 Fire 파티클이 작동하고 파티클의 재생시간이 끝나면 정지된다.

    /// <summary>
    /// 이팩트 재생 시간
    /// </summary>
    public float duration = 5.0f;

    /// <summary>
    /// 조명 최대 범위
    /// </summary>
    public float maxLightRange = 7.0f;

    ParticleSystem ps;
    Light effectLight;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        ps = child.GetComponent<ParticleSystem>();
        child = transform.GetChild(2);
        effectLight = child.GetComponent<Light>();
        effectLight.range = 0.0f;       // 조명 범위 0으로 만들기
    }

    protected override void OnTrapActivate(GameObject target)
    {
        ps.Play();      // 이팩트 재생하기
        Player targetPlayer = target.GetComponent<Player>();
        targetPlayer?.Die();                // 밟은 플레이어 죽이기

        StopAllCoroutines();                // 이전에 재생하던 코루틴 정지
        StartCoroutine(EffectCoroutine());  // 이팩트 끄기용 코루틴 실행
    }

    IEnumerator EffectCoroutine()
    {
        const float lightTime = 0.2f;

        float remainsTime = lightTime;
        while (remainsTime > 0)         // 조명이 lightTime동안 범위가 넓어지게 만들기(최대치 maxLightRange)
        {
            effectLight.range = Mathf.Lerp(0, maxLightRange, (remainsTime - lightTime) * -10);
            remainsTime -= Time.deltaTime;

            yield return null;
        }
        
        yield return new WaitForSeconds(duration - lightTime);   // 전체 재생 시간이 끝나면 파티클 생성 중지하고 조명 끄기
        ps.Stop();

        remainsTime = lightTime;
        while (remainsTime > 0)     // 조명이 lightTime동안 범위가 줄어들게 만들기(최소치 0)
        {
            effectLight.range = Mathf.Lerp(0, maxLightRange, remainsTime * 10);
            remainsTime -= Time.deltaTime;

            yield return null;
        }

        effectLight.range = 0.0f;
    }

}

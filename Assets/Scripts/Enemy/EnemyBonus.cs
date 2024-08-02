using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonus : EnemyBase
{
    [Header("보너스 주는 적 데이터")]
    /// <summary>
    /// 등장 시간(처음 멈출때까지의 시간)
    /// </summary>
    public float appearTime = 0.5f;

    /// <summary>
    /// 대기 시간
    /// </summary>
    public float waitTime = 5.0f;

    /// <summary>
    /// 대기 시간 이후의 속도
    /// </summary>
    public float secondSpeed = 10.0f;

    Animator animator;

    readonly int Speed_Hash = Animator.StringToHash("Speed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnReset()
    {
        base.OnReset();

        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        animator.SetFloat(Speed_Hash, moveSpeed);
        yield return new WaitForSeconds(appearTime);

        moveSpeed = 0;  // 멈추게 하기
        animator.SetFloat(Speed_Hash, moveSpeed);

        yield return new WaitForSeconds(waitTime);

        moveSpeed = secondSpeed;    // 다시 움직이게 하기
        animator.SetFloat(Speed_Hash, moveSpeed);
    }

    protected override void OnDie()
    {
        Factory.Instance.GetPowerUp(transform.position);
    }
}

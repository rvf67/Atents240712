using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// 공격 애니메이션 재생 시간(공통)
    /// </summary>
    const float AttackAnimLenght = 0.533f;

    /// <summary>
    /// 쿨타임 설정용 변수(콤보를 위해서 애니 시간보다 작아야한다)
    /// </summary>
    [Range(0, AttackAnimLenght)]
    public float maxCoolTime = 0.3f;

    /// <summary>
    /// 현재 남아있는 쿨타임
    /// </summary>
    float coolTime = 0.0f;


    // 컴포넌트
    PlayerMovement playerMovement;
    Animator animator;


    // 애니메이터용 해시
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
    }

    /// <summary>
    /// 공격 입력이 들어오면 실행되는 함수
    /// </summary>
    public void OnAttackInput()
    {
        Attack();        
    }

    /// <summary>
    /// 공격 한번을 하는 함수
    /// </summary>
    void Attack()
    {
        // 쿨타임이 다 되거나, 가만히 서 있거나, 걷기 모드일때만 공격 가능
        if( coolTime < 0 && playerMovement.MoveMode != PlayerMovement.MoveState.Run)
        {
            animator.SetTrigger(Attack_Hash);
            coolTime = maxCoolTime;
        }
    }
}

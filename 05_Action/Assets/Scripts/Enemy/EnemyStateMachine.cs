using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    /// <summary>
    /// 상태의 종류를 나타내는 enum
    /// </summary>
    enum EnemyState : byte
    {
        Wait = 0,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    /// <summary>
    /// 대기 상태로 들어갔을 때 기다리는 시간
    /// </summary>
    public float waitTime = 1.0f;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// 적이 순찰할 웨이포인트(사실상 private)
    /// </summary>
    public Waypoints waypoints;

    /// <summary>
    /// 원거리 시야 범위
    /// </summary>
    public float farSightRange = 10.0f;

    /// <summary>
    /// 원거리 시야각의 절반
    /// </summary>
    public float sightHalfAngle = 50.0f;

    /// <summary>
    /// 근거리 시야범위
    /// </summary>
    public float nearSightRange = 1.5f;

    /// <summary>
    /// 적의 현재 상태
    /// </summary>
    EnemyState state = EnemyState.Patrol;

    /// <summary>
    /// 대기 시간 측정용 변수(계속 감소)
    /// </summary>
    float waitTimer = 1.0f;

    /// <summary>
    /// 추적 대상
    /// </summary>
    Transform chaseTarget = null;

    /// <summary>
    /// 공격 대상
    /// </summary>
    IBattler attackTarget = null;

    /// <summary>
    /// 적의 현재 상태를 설정하고 확인하기 위한 프로퍼티
    /// </summary>
    EnemyState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                switch (state)
                {
                    case EnemyState.Wait:
                        onStateUpdate = Update_Wait;
                        break;
                    case EnemyState.Patrol:
                        onStateUpdate = Update_Patrol;
                        break;
                    case EnemyState.Chase:
                        onStateUpdate = Update_Chase;
                        break;
                    case EnemyState.Attack:
                        onStateUpdate = Update_Attack;
                        break;
                    case EnemyState.Dead:
                        onStateUpdate = Update_Die;
                        break;                    
                }
            }
        }
    }

    /// <summary>
    /// 대기 시간 측정 처리용 프로퍼티
    /// </summary>
    float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;
            if (waitTimer < 0.0f)
            {
                State = EnemyState.Patrol;  // 대기시간이 끝나면 무조건 patrol 상태로 변환
            }
        }
    }

    Action onStateUpdate;

    private void Update()
    {        
        onStateUpdate();
    }

    void Update_Wait()
    {
    }

    void Update_Patrol()
    {
    }

    void Update_Chase()
    {
    }

    void Update_Attack()
    {

    }

    void Update_Die()
    {

    }
}

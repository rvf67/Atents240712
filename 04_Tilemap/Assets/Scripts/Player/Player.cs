using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동 속도(기본)
    /// </summary>
    public float speed = 3.0f;

    /// <summary>
    /// 공격 간격(쿨타임)
    /// </summary>
    public float attackInterval = 1.0f;

    /// <summary>
    /// 플레이어 최대 수명
    /// </summary>
    public float maxLifeTime = 1000.0f;

    /// <summary>
    /// 입력받은 방향
    /// </summary>
    Vector2 inputDirection = Vector2.zero;

    /// <summary>
    /// 현재 공격 쿨타임
    /// </summary>
    float attackCoolTime = 0.0f;

    /// <summary>
    /// 공격 쿨타임이 다 되었는지 확인하기 위한 프로퍼티
    /// </summary>
    bool IsAttackReady => attackCoolTime < 0.0f;

    /// <summary>
    /// 현재 속도
    /// </summary>
    float currentSpeed = 3.0f;

    /// <summary>
    /// AttackSensor의 축
    /// </summary>
    Transform attackSensorAxis;

    /// <summary>
    /// 지금 공격이 유효한 상태인지 확인하는 변수
    /// </summary>
    bool isAttackValid = false;

    /// <summary>
    /// 현재 내 공격 범위 안에 있는 모든 슬라임의 목록
    /// </summary>
    List<Slime> attackTargetList;

    /// <summary>
    /// 플레이어의 현재 수명
    /// </summary>
    float lifeTime;

    /// <summary>
    /// 플레이어가 살아있는지 표시하는 변수(true면 살아있다, false면 죽었다)
    /// </summary>
    bool isAlive = true;

    /// <summary>
    /// 플레이어가 죽인 슬라임의 수
    /// </summary>
    int killCount = -1;

    /// <summary>
    /// 플레이어의 최대 수명을 확인하기 위한 프로퍼티
    /// </summary>
    public float MaxLifeTime => maxLifeTime;

    float LifeTime
    {
        get => lifeTime;
        set
        {
            lifeTime = value;
            if (isAlive && lifeTime < 0.0f)
            {
                // 플레이어 사망
                Die();
            }
            else
            {
                lifeTime = Mathf.Clamp(lifeTime, 0.0f, maxLifeTime);    // 넘치거나 0이하로 떨어지지 않게 만들기
                onLifeTimeChange?.Invoke(lifeTime / maxLifeTime);         // 변화를 알리기
            }
        }
    }

    /// <summary>
    /// 킬카운트를 확인하고 설정하는 프로퍼티
    /// </summary>
    public int KillCount
    {
        get => killCount;
        set
        {
            if (killCount != value)
            {
                killCount = value;
                onKillCountChange?.Invoke(killCount);   // 값의 변경이 있을 때만 알림
            }
        }
    }

    /// <summary>
    /// 플레이어의 수명이 변경되었을 때 실행될 델리게이트(float:현재 수명/최대 수명)
    /// </summary>
    public Action<float> onLifeTimeChange;

    /// <summary>
    /// 플레이어가 킬을 할 때마다 실행되는 델리게이트(int:현재 죽인 슬라임의 수)
    /// </summary>
    public Action<int> onKillCountChange;

    /// <summary>
    /// 플레이어가 이동할 때 실행될 델리게이트(Vector3:플레이어의 위치)
    /// </summary>
    public Action<Vector3> onMove;

    /// <summary>
    /// 플레이어가 사망했을 때 실행될 델리게이트
    /// </summary>
    public Action onDie;

    // 인풋 액션
    PlayerInputActions inputActions;

    // 필요 컴포넌트들
    Rigidbody2D rigid;
    Animator animator;

    // 애니메이터용 해시
    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int Attack_Hash = Animator.StringToHash("Attack");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        attackSensorAxis = transform.GetChild(0);
        AttackSensor sensor = attackSensorAxis.GetComponentInChildren<AttackSensor>();
        sensor.onSlimeEnter += (slime) =>       // 공격 범위에 슬라임이 들어왔을 때
        {
            if (isAttackValid)
            {
                slime.Die();                    // 공격이 유효할 때 영역안에 들어오면 즉시 사망
                EnemyKill(slime.LifeTimeBonus); // 적 킬처리
            }
            else
            {
                attackTargetList.Add(slime);    // 공격이 유효하지 않으면 일단 리스트에 추가
            }
            slime.ShowOutline(true);    // 아웃라인 표시하기
        };
        sensor.onSlimeExit += (slime) =>        // 공격 범위에서 슬라임이 나갔을 때
        {
            attackTargetList.Remove(slime);     // 공격 대상 리스트에서 제거
            slime.ShowOutline(false);           // 아웃라인 끄기
        };

        attackTargetList = new List<Slime>(4);

        inputActions = new PlayerInputActions();

        currentSpeed = speed;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnStop;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Move.canceled -= OnStop;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        LifeTime = MaxLifeTime;
        KillCount = 0;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();      // 입력 받은 방향 저장

        animator.SetFloat(InputX_Hash, inputDirection.x);   // 애니메이터에 방향 전달
        animator.SetFloat(InputY_Hash, inputDirection.y);
        animator.SetBool(IsMove_Hash, true);                // 애니메이터에 움직이기 시작했다고 알림

        AttackSensorRotate(inputDirection);                 // 공격 영역 회전시키기
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        inputDirection = Vector2.zero;                      // 입력 방향 초기화

        // x,y 세팅 안함(이동 방향을 계속 바라보게 하기 위해)

        animator.SetBool(IsMove_Hash, false);               // 애니메이터에 멈췄다고 알림
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (IsAttackReady)
        {
            animator.SetTrigger(Attack_Hash);                   // 애니메이터에 공격 알림
            attackCoolTime = attackInterval;
            currentSpeed = 0.0f;
        }
    }

    private void Update()
    {
        attackCoolTime -= Time.deltaTime;
        LifeTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * currentSpeed * inputDirection);
        onMove?.Invoke(transform.position);
    }

    /// <summary>
    /// 속도를 원상복귀 시키는 함수
    /// </summary>
    public void RestoreSpeed()
    {
        currentSpeed = speed;
    }

    /// <summary>
    /// 입력 방향에 따라 AttackSensor를 회전시키는 함수
    /// </summary>
    /// <param name="direction">입력 방향</param>
    void AttackSensorRotate(Vector2 direction)
    {
        if (direction.y < 0.0f)
        {
            // 아래쪽을 바라보고 있다.
            attackSensorAxis.rotation = Quaternion.identity;
        }
        else if (direction.y > 0.0f)
        {
            // 위쪽을 바라보고 있다.
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction.x < 0.0f)
        {
            // 왼쪽을 바라보고 있다.
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (direction.x > 0.0f)
        {
            // 오른쪽을 바라보고 있다.
            attackSensorAxis.rotation = Quaternion.Euler(0, 0, 90);
        }

        //if (direction.sqrMagnitude > 0.01f)
        //{
        //    float angle = Vector2.SignedAngle(Vector2.down, direction);
        //    attackSensorAxis.rotation = Quaternion.Euler(0, 0, angle);
        //}
    }

    /// <summary>
    /// 공격 애니메이션 진행 중에 공격이 유효해지면 애니메이션 이벤트로 실행할 함수
    /// </summary>
    void AttackValid()
    {
        isAttackValid = true;                   // 유효하다고 표시하고
        foreach (var slime in attackTargetList)
        {
            slime.Die();                        // 범위 안에 있던 모든 슬라임 죽이기
            EnemyKill(slime.LifeTimeBonus);     // 적 킬 처리
        }
        attackTargetList.Clear();
    }

    /// <summary>
    /// 공격 애니메이션 진행 중에 공격이 유효하지 않게 되면 애니메이션 이벤트로 실행할 함수
    /// </summary>
    void AttackNotValid()
    {
        isAttackValid = false;
    }

    /// <summary>
    /// 플레이어가 죽었을 때 실행될 함수
    /// </summary>
    private void Die()
    {
        isAlive = false;                // 죽었다고 표시
        LifeTime = 0.0f;                // 수명도 0으로 설정
        onLifeTimeChange?.Invoke(0);    // 수명 변화 알리기
        inputActions.Player.Disable();  // 입력 막기
        onDie?.Invoke();                // 죽었다고 알리기
    }

    /// <summary>
    /// 적을 죽였을 때 실행될 함수
    /// </summary>
    /// <param name="bounus">적 처리 보너스(증가할 수명)</param>
    void EnemyKill(float bounus)
    {
        LifeTime += bounus;
        KillCount++;
    }
}
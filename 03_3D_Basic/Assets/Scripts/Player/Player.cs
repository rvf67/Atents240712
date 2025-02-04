using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPlatformRide
{
    // 플레이어가 WASD입력을 받아서 이동한다.(액션 이름은 Movement)
    // WS로 전진/후진
    // AD로 좌회전/우회전
    // 실제 이동 처리는 Rigidbody를 이용해서 처리

    /// <summary>
    /// 플레이어 이동 속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 플레이어 회전 속도
    /// </summary>
    public float rotateSpeed = 180.0f;

    /// <summary>
    /// 점프력
    /// </summary>
    public float jumpPower = 6.0f;

    /// <summary>
    /// 점프 쿨타임
    /// </summary>
    public float jumpCoolTime = 1;
    
    /// <summary>
    /// 플레이어 생존여부
    /// </summary>
    private bool isAlive = true;

    VirtualPad virtualPad;

    /// <summary>
    /// 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;

    /// <summary>
    /// 회전 방향(음수면 좌회전, 양수면 우회전)
    /// </summary>
    private float rotateDirection = 0.0f;

    /// <summary>
    /// 이동방향(양수면 전진, 음수면 후진)
    /// </summary>
    private float moveDirection = 0.0f;

    /// <summary>
    /// 지금 발이 바닥에 닿았는지 확인하는 변수(true면 바닥에 닿아있다.)
    /// </summary>
    bool isGrounded = true;

    /// <summary>
    /// 남아있는 점프 쿨타임
    /// </summary>
    float jumpCoolRemains = 0.0f;

    /// <summary>
    /// 속도 적용 비율(1일때 정상 속도)
    /// </summary>
    float speedModifier = 1.0f;

    /// <summary>
    /// 지금 점프가 가능한지 확인하는 프로퍼티(바닥에 닿아있고 쿨타임이 다 되었다)
    /// </summary>
    bool IsJumpAvailable => isGrounded && (JumpCoolRemains < 0.0f);

    /// <summary>
    /// 점프 쿨타임을 확인하고 설정하기 위한 프로퍼티
    /// </summary>
    float JumpCoolRemains
    {
        get => jumpCoolRemains;
        set
        {
            jumpCoolRemains = value;
            onJumpCoolTimeChange?.Invoke(jumpCoolRemains / jumpCoolTime); // 비율로 전달
        }
    }

    /// <summary>
    /// 점프 쿨타임에 변화가 있었음을 알리는 델리게이트
    /// </summary>
    public Action<float> onJumpCoolTimeChange;

    /// <summary>
    /// 이 플레이어가 죽었음을 알리는 델리게이트
    /// </summary>
    public Action onDie;
    // 컴포넌트들
    Rigidbody rigid;
    Animator animator;

    // 애니메이터용 해시
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int IsUse_Hash = Animator.StringToHash("Use");

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        GroundSensor groundSensor = GetComponentInChildren<GroundSensor>();
        groundSensor.onGround += (isGround) =>
        {
            isGrounded = isGround;      // GroundSensor에서 신호가 오면 변수 저장
            //Debug.Log(isGrounded);
        };

        UseSensor useSensor = GetComponentInChildren<UseSensor>();
        useSensor.onUse += (usable) => usable.Use();    // 사용 시도 신호가 들어오면 사용한다.
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Jump.performed += OnJumpInput;
        inputActions.Player.Use.performed += OnUseInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Use.performed -= OnUseInput;
        inputActions.Player.Jump.performed -= OnJumpInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector2>(), !context.canceled);  // 입력 받은 내용 처리
    }

    private void OnJumpInput(InputAction.CallbackContext _)
    {
        Jump();
    }

    private void OnUseInput(InputAction.CallbackContext context)
    {
        Use();
    }

    void Start()
    {
        //VirtualStick stick = GameManager.Instance.Stick;
        //if (stick != null)
        //{
        //    // 델리게이트에 람다식 연결(0.05보다 더 움직일때만 움직인다고 전달)
        //    stick.onMoveInput += (inputDelta) => SetInput(inputDelta, inputDelta.sqrMagnitude > 0.0025f);
        //}

        //VirtualButton button = GameManager.Instance.JumpButton;
        //if(button != null)
        //{
        //    button.onClick += Jump;
        //    onJumpCoolTimeChange += button.RefreshCoolTime;
        //}

        //VirtualPad virtualPad = GameManager.Instance.VirtualPad;
        //virtualPad.SetStickBind(0, (inputDelta) => SetInput(inputDelta, inputDelta.sqrMagnitude > 0.0025f));
        //virtualPad.SetButtonBind(0, Jump, ref onJumpCoolTimeChange);
    }

    private void Update()
    {
        JumpCoolRemains -= Time.deltaTime;  // 점프 쿨타임 줄이기
    }

    private void FixedUpdate()
    {
        Movement(Time.fixedDeltaTime);  // 이동 및 회전
    }

    /// <summary>
    /// 이동 및 회전 처리용 함수
    /// </summary>
    private void Movement(float deltaTime)
    {
        // 새 이동할 위치 : 현재위치 + (초당 moveSpeed * speedModifier의 속도로, 오브젝트의 앞쪽 방향을 기준으로 전진/후진/정지)
        Vector3 position = rigid.position + deltaTime * moveSpeed * speedModifier * moveDirection * transform.forward;

        // 새 회전 : 현재 회전에서 추가로 (초당 rotateSpeed의 속도로, 오브젝트의 up을 축으로 좌회전/우회전/정지하는 회전) 
        Quaternion rotation = rigid.rotation * Quaternion.AngleAxis(deltaTime * rotateSpeed * rotateDirection, transform.up);

        rigid.Move(position, rotation);
    }


    /// <summary>
    /// 이동 입력 처리용 함수
    /// </summary>
    /// <param name="input">입력된 방향</param>
    /// <param name="isMove">이동 중이면 true, 아니면 false</param>
    void SetInput(Vector2 input, bool isMove)
    {
        rotateDirection = input.x;
        moveDirection = input.y;
        animator.SetBool(IsMove_Hash, isMove);

        // 이동시에만 팔을 움직이게 하고 싶을 때
        //bool move = true;
        //if(moveDirection > -0.01f && moveDirection < 0.01f) // moveDirection == 0.0f 이것과 비슷한 코드
        //{
        //    move = false;
        //}
        //animator.SetBool(IsMove_Hash, move);
    }

    /// <summary>
    /// 플레이어 점프 처리용 함수
    /// </summary>
    void Jump()
    {
        // 점프 키를 누르면 실행된다(space키)
        // 2단 점프 금지
        // 쿨타임 존재

        if (IsJumpAvailable)        // 점프가 가능할 때만
        {
            //Debug.Log("Jump");
            rigid.AddForce(jumpPower * Vector3.up, ForceMode.Impulse);  // 위로 점프
            JumpCoolRemains = jumpCoolTime;                             // 쿨타임 초기화
        }
    }

    /// <summary>
    /// 상호작용 관련 처리용 함수
    /// </summary>
    void Use()
    {
        animator.SetTrigger(IsUse_Hash);    // 애니메이션으로 사용 처리 
    }

    /// <summary>
    /// 플레이어 사망 처리 함수
    /// </summary>
    public void Die()
    {
        if (isAlive)
        {
            animator.SetTrigger("Die");
            Debug.Log("플레이어 죽음");
            inputActions.Player.Disable();
            GameManager.Instance.VirtualPad.Disconnect();
            rigid.constraints =RigidbodyConstraints.None; //물리 잠금 모두 해제
            Transform head = transform.GetChild(5);
            rigid.AddForceAtPosition(-transform.forward, head.position, ForceMode.Impulse);
            rigid.AddTorque(transform.up*1.5f,ForceMode.Impulse);
            rigid.angularDrag = 0.00001f;

            onDie?.Invoke();
            isAlive=false;
            
        }
    }

    /// <summary>
    /// 슬로우 디버프를 거는 함수
    /// </summary>
    /// <param name="slowRate">느려지는 비율(0.1이면 속도가 10% 상태로 설정됨)</param>
    public void SetSlowDebuf(float slowRate)
    {
        //Debug.Log("슬로우 디버프");
        StopAllCoroutines();
        speedModifier = slowRate;
    }

    /// <summary>
    /// 슬로우 디버프를 해제하는 함수
    /// </summary>
    /// <param name="duration">이 시간 후에 완전 해제</param>
    public void RemoveSlowDebuf(float duration = 0.0f)
    {
        //Debug.Log("슬로우 디버프 - 해제 시작");
        StopAllCoroutines();
        StartCoroutine(RestoreSpeedModifier(duration));
    }

    /// <summary>
    /// duration동안 speedModifier를 1로 되돌리는 코루틴
    /// </summary>
    /// <param name="duration">전체 진행 시간</param>
    IEnumerator RestoreSpeedModifier(float duration)
    {
        float current = speedModifier;

        float timeElapsed = 0.0f;
        float inverseDuration = 1 / duration;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            speedModifier = Mathf.Lerp(current, 1.0f, timeElapsed * inverseDuration);
            //Debug.Log($"슬로우 디버프 - 해제중({speedModifier:f2})");

            yield return null;
        }

        speedModifier = 1.0f;
        //Debug.Log("슬로우 디버프 - 해제 완료");
    }

    /// <summary>
    /// 플랫폼 움직임에 따라 같이 움직이게 하는 함수
    /// </summary>
    /// <param name="moveDelta">플랫폼이 움직인 양</param>
    public void OnRidePlatform(Vector3 moveDelta)
    {
        rigid.MovePosition(rigid.position + moveDelta);
    }
}

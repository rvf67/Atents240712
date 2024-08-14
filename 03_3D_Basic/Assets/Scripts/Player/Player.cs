using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 플레이어가 WASD입력을 받아서 이동한다.(액션 이름은 Move)
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
    /// 양수면 우회전 ,음수면 좌회전
    /// </summary>
    private float rotateDirection=0.0f;
    /// <summary>
    /// 양수면 전진 음수면 후진
    /// </summary>
    private float moveDirection=0.0f;

    /// <summary>
    /// 입력된 방향
    /// </summary>
    Vector3 inputDirection = Vector3.zero;

    Rigidbody rb;
    /// <summary>
    /// 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        inputActions.Enable();                          // 인풋 액션 활성화
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        Movement(Time.deltaTime);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        SetInput(context.ReadValue<Vector3>(),!context.canceled);
    }

    void SetInput(Vector3 input,bool isMove)
    {
        rotateDirection = input.x;
        moveDirection = input.z;
    }

    /// <summary>
    /// 이동 및 회전처리용 함수
    /// </summary>
    void Movement(float deltaTime)
    {
        //새 이동할 위치: 현재 위치 + (초당 moveSpeed의 속도로,오브젝트의 앞쪽 방향을 기준으로 전진/후진/정지)
        Vector3 position = rb.position + deltaTime*moveSpeed*moveDirection*transform.forward;
        // 새 회전: 현재 회전에서 추가로(초당 rotateSpeed의 속도로 좌회전/우회전/정지 하는 회전) 
        Quaternion rotation = rb.rotation * Quaternion.AngleAxis(deltaTime * rotateSpeed * rotateDirection, transform.up);

        rb.Move(position, rotation);
    }
}

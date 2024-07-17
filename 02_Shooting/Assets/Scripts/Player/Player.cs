using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.01f;

    /// <summary>
    /// 입력된 방향
    /// </summary>
    Vector3 inputDirection = Vector3.zero;

    /// <summary>
    /// 입력용 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // 인풋 액션 생성
    }

    private void OnEnable()
    {
        inputActions.Enable();                          // 인풋 액션 활성화
        inputActions.Player.Fire.performed += OnFire;   // 액션과 함수 바인딩
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Fire.performed -= OnFire;
        inputActions.Disable();
    }

    /// <summary>
    /// Move 액션이 발생했을 때 실행될 함수
    /// </summary>
    /// <param name="context">입력 정보</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();   // 입력 값 읽기
        //transform.position += (Vector3)input;           // 입력 값에 따라 이동
        inputDirection = (Vector3)input;
    }

    /// <summary>
    /// Fire 액션이 발생했을 때 실행될 함수
    /// </summary>
    /// <param name="_">입력 정보(사용하지 않아서 칸만 잡아놓은 것)</param>
    private void OnFire(InputAction.CallbackContext _)
    {
        Debug.Log("발사");    // 발사라고 출력
    }

    private void Update()
    {
        // 곱하는 순서
        // 컴퓨터 성능

        transform.position += (moveSpeed * inputDirection);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public GameObject shootPrefab;
    public Transform shotPos;
    private bool isShoot;
    private Animator animator;
    private Vector3 pos;
    readonly int InputY_String = Animator.StringToHash("InputY");
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
        animator = GetComponent<Animator>();
        shotPos=transform.GetChild(0);
    }

    private void OnEnable()
    {
        inputActions.Enable();                          // 인풋 액션 활성화
        inputActions.Player.Fire.performed += OnFire;   // 액션과 함수 바인딩
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        //inputActions.Player.Fire.canceled += OnFire;
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.canceled -= OnFire;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        //inputActions.Player.Fire.performed -= OnFire;
        inputActions.Disable();
    }

    /// <summary>
    /// Move 액션이 발생했을 때 실행될 함수
    /// </summary>
    /// <param name="context">입력 정보</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>();   // 입력 값 읽기
        //transform.position += (Vector3)input;           // 입력 값에 따라 이동
        inputDirection = (Vector3)input;
        if (inputDirection.y < 0)
        {
            animator.SetFloat(InputY_String, -1.0f);
        }else if (inputDirection.y > 0)
        {
            animator.SetFloat(InputY_String, 1.0f);
        }
        else
        {
            animator.SetFloat(InputY_String, 0f);
        }
    }

    /// <summary>
    /// Fire 액션이 발생했을 때 실행될 함수
    /// </summary>
    /// <param name="_">입력 정보(사용하지 않아서 칸만 잡아놓은 것)</param>
    private void OnFire(InputAction.CallbackContext context)
    {
        Fire();
        Debug.Log("발사");    // 발사라고 출력
        
    }
    private void Update()
    {
        // 곱하는 순서
        // 컴퓨터 성능

        //transform.position += (moveSpeed * Time.deltaTime * inputDirection);
        transform.Translate(moveSpeed * Time.deltaTime * inputDirection);

        pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x<0f)pos.x = 0f;
        if(pos.x>1f)pos.x = 1f;
        if(pos.y<0f)pos.y = 0f;
        if(pos.y>1f)pos.y = 1f;
        transform.position =Camera.main.ViewportToWorldPoint(pos);
    }
    void Fire()
    {
        Instantiate(shootPrefab,shotPos.position,shotPos.rotation);
    }
}

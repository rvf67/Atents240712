using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 0.01f;

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float fireInterval = 0.5f;

    /// <summary>
    /// 총알 프리팹
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// 입력된 방향
    /// </summary>
    Vector3 inputDirection = Vector3.zero;

    /// <summary>
    /// 입력용 인풋 액션
    /// </summary>
    PlayerInputActions inputActions;

    /// <summary>
    /// 애니메이터 컴포넌트를 저장할 변수
    /// </summary>
    Animator animator;

    /// <summary>
    /// 애니메이터용 해시 만들기
    /// </summary>
    readonly int InputY_String = Animator.StringToHash("InputY");

    /// <summary>
    /// 총알 발사용 트랜스폼
    /// </summary>
    Transform fireTransform;

    /// <summary>
    /// 총알 발사용 코루틴
    /// </summary>
    IEnumerator fireCoroutine;

    /// <summary>
    /// 총알 발사 이팩트용 게임 오브젝트
    /// </summary>
    GameObject fireFlash;

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // 인풋 액션 생성

        animator = GetComponent<Animator>();        // 자신과 같은 게임오브젝트 안에 있는 컴포넌트 찾기        

        fireTransform = transform.GetChild(0);          // 첫번째 자식 찾기
        fireFlash = transform.GetChild(1).gameObject;   // 두번째 자식 찾아서 그 자식의 게임 오브젝트 저장하기

        fireCoroutine = FireCoroutine();            // 코루틴 저장하기
    }

    private void OnEnable()
    {
        inputActions.Enable();                          // 인풋 액션 활성화
        inputActions.Player.Fire.performed += OnFireStart;   // 액션과 함수 바인딩
        inputActions.Player.Fire.canceled += OnFireEnd;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Fire.canceled -= OnFireEnd;
        inputActions.Player.Fire.performed -= OnFireStart;
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

        //animator.SetFloat("InputY", input.y);       // 애니메이터의 "InputY" 파라메터 변경
        animator.SetFloat(InputY_String, input.y);
    }

    /// <summary>
    /// Fire 액션이 발생했을 때 실행될 함수
    /// </summary>
    /// <param name="_">입력 정보(사용하지 않아서 칸만 잡아놓은 것)</param>
    private void OnFireStart(InputAction.CallbackContext _)
    {
        Debug.Log("발사 시작");
        //Fire();
        //StartCoroutine("FireCoroutine");
        //StartCoroutine(FireCoroutine());
        StartCoroutine(fireCoroutine);
    }

    private void OnFireEnd(InputAction.CallbackContext _)
    {
        Debug.Log("발사 종료");
        //StopAllCoroutines();    // 모든 코루틴 정지시키기
        //StopCoroutine("FireCoroutine");
        //StopCoroutine(FireCoroutine());
        StopCoroutine(fireCoroutine);
    }

    private void Update()
    {
        //transform.position += (Time.deltaTime * moveSpeed * inputDirection);    // 초당 moveSpeed의 속도로 inputDirection 방향으로 이동
        //transform.position += (inputDirection * moveSpeed * Time.deltaTime);  // 위에 코드는 4번 곱하지만 이 코드는 6번 곱한다.

        transform.Translate(Time.deltaTime * moveSpeed * inputDirection);
    }

    /// <summary>
    /// 총알을 한발 발사하는 함수
    /// </summary>
    void Fire()
    {
        // 플래시 이팩트 잠깐 켜기
        StartCoroutine(FlashEffect());

        // 총알 생성
        //Instantiate(bulletPrefab, transform); // 자식은 부모를 따라다니므로 이렇게 하면 안됨
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);           // 총알이 비행기와 같은 위치에 생성
        //Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);   // 총알 발사 위치를 확인하기 힘듬
        Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);  // 총알을 fireTransform의 위치와 회전에 따라 생성
    }

    /// <summary>
    /// 연사용 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        // 코루틴 : 일정 시간 간격으로 코드를 실행하거나 일정 시간동안 딜레이를 줄 때 유용

        while (true) // 무한 루프
        {
            //Debug.Log("Fire");
            Fire();
            yield return new WaitForSeconds(fireInterval);  // fireInterval초만큼 기다렸다가 다시 시작하기
        }

        // 프레임 종료시까지 대기
        // yield return null;
        // yield return new WaitForEndOfFrame();
    }

    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true);  // 게임 오브젝트 활성화하기
        yield return new WaitForSeconds(0.1f);
        fireFlash.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
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
    /// 총알 간의 사이각
    /// </summary>
    public float fireAngle = 30.0f;

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
    /// 총알 발사용 트랜스폼의 배열
    /// </summary>
    Transform[] fireTransform;

    /// <summary>
    /// 총알 발사용 코루틴
    /// </summary>
    IEnumerator fireCoroutine;

    /// <summary>
    /// 총알 발사 이팩트용 게임 오브젝트
    /// </summary>
    GameObject fireFlash;

    /// <summary>
    /// 총알 발사 이팩트가 보일 시간용
    /// </summary>
    WaitForSeconds flashWait;

    /// <summary>
    /// 파워의 최소값
    /// </summary>
    private const int MinPower = 1;

    /// <summary>
    /// 파워의 최대값
    /// </summary>
    private const int MaxPower = 3;

    /// <summary>
    /// 현재 파워
    /// </summary>
    int power = 1;

    int Power
    {
        get => power;
        set
        {
            if (power != value) // 변경이 있을 때만 처리
            {
                power = value;

                if( power > MaxPower)
                {
                    // MaxPower보다 커졌으면 보너스 점수 얻기
                    ScoreText scoreText = GameManager.Instance.ScoreText;
                    scoreText.AddScore(PowerUp.BonusPoint);
                }

                // power는 MinPower ~ MaxPower 범위
                power = Mathf.Clamp(power, MinPower, MaxPower);

                // 발사 각도 변경
                RefreshFireAngles();
            }
        }
    }

    /// <summary>
    /// 리지드바디 컴포넌트
    /// </summary>
    Rigidbody2D rigid;

    private void Awake()
    {
        inputActions = new PlayerInputActions();    // 인풋 액션 생성

        animator = GetComponent<Animator>();        // 자신과 같은 게임오브젝트 안에 있는 컴포넌트 찾기        
        rigid = GetComponent<Rigidbody2D>();

        Transform fireRoot = transform.GetChild(0);     // 첫번째 자식 찾기
        fireTransform = new Transform[fireRoot.childCount];
        for (int i = 0; i < fireTransform.Length; i++)
        {
            fireTransform[i] = fireRoot.GetChild(i);    // 발사 위치 전부 찾기
        }
        fireFlash = transform.GetChild(1).gameObject;   // 두번째 자식 찾아서 그 자식의 게임 오브젝트 저장하기

        fireCoroutine = FireCoroutine();            // 코루틴 저장하기

        flashWait = new WaitForSeconds(0.1f);       // 총알 발사용 이팩트는 0.1초 동안만 보인다.
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

    //private void Update()
    //{
    //    // Update 함수가 호출되는 시간 간격(Time.deltaTime)은 매번 다르다.
    //    // Debug.Log(Time.deltaTime);

    //    //transform.position += (Time.deltaTime * moveSpeed * inputDirection);    // 초당 moveSpeed의 속도로 inputDirection 방향으로 이동
    //    //transform.position += (inputDirection * moveSpeed * Time.deltaTime);  // 위에 코드는 4번 곱하지만 이 코드는 6번 곱한다.

    //    //transform.Translate(Time.deltaTime * moveSpeed * inputDirection);
    //}

    private void FixedUpdate()
    {
        // 항상 일정 시간 간격(Time.fixedDeltaTime)으로 호출된다.
        // Debug.Log(Time.fixedDeltaTime);

        // transform.Translate(Time.fixedDeltaTime * moveSpeed * inputDirection);   // 한번은 파고 들어간다.
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * (Vector2)inputDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  // 이쪽을 권장. ==에 비해 가비지가 덜 생성된다. 생성되는 코드도 훨씬 빠르게 구현되어 있음.
        {
            Debug.Log("적과 부딪쳤다.");
        }
        //else if (collision.gameObject.CompareTag("PowerUp"))
        //{
        //    Power++;
        //    collision.gameObject.SetActive(false);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Power++;
            collision.gameObject.SetActive(false);
        }
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
        //Debug.Log("발사 시작");
        //Fire();
        //StartCoroutine("FireCoroutine");
        //StartCoroutine(FireCoroutine());
        StartCoroutine(fireCoroutine);
    }

    private void OnFireEnd(InputAction.CallbackContext _)
    {
        //Debug.Log("발사 종료");
        //StopAllCoroutines();    // 모든 코루틴 정지시키기
        //StopCoroutine("FireCoroutine");
        //StopCoroutine(FireCoroutine());
        StopCoroutine(fireCoroutine);
    }

    /// <summary>
    /// 총알을 한발 발사하는 함수
    /// </summary>
    void Fire(Transform fire)
    {
        // 플래시 이팩트 잠깐 켜기
        StartCoroutine(FlashEffect());

        // 총알 생성
        //Instantiate(bulletPrefab, transform); // 자식은 부모를 따라다니므로 이렇게 하면 안됨
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);           // 총알이 비행기와 같은 위치에 생성
        //Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);   // 총알 발사 위치를 확인하기 힘듬
        //Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);  // 총알을 fireTransform의 위치와 회전에 따라 생성

        // 팩토리 사용
        Factory.Instance.GetBullet(fire.position, fire.rotation.eulerAngles.z);
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
            for(int i=0;i<Power;i++)
            {
                Fire(fireTransform[i]);
            }
            yield return new WaitForSeconds(fireInterval);  // fireInterval초만큼 기다렸다가 다시 시작하기
        }

        // 프레임 종료시까지 대기
        // yield return null;
        // yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// 발사 이팩트용 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true);  // 게임 오브젝트 활성화하기
        yield return flashWait;     // 잠깐 딜레이 걸기
        fireFlash.SetActive(false);
    }

    private void RefreshFireAngles()
    {
        for(int i=0;i<MaxPower; i++)
        {
            if( i < Power )
            {
                // fireAngle이 30도 일 때
                // 1 : 0도
                // 2 : 15도, -15도
                // 3 : 30도, 0도, -30도

                // 회전 결정
                float startAngle = (Power - 1) * (fireAngle * 0.5f);
                float angleDelta = i * -fireAngle;
                fireTransform[i].rotation = Quaternion.Euler(0, 0, startAngle + angleDelta);

                // 약간 앞으로 옮기기
                fireTransform[i].localPosition = Vector3.zero;
                fireTransform[i].Translate(0.5f, 0, 0);

                // 보일 부분
                fireTransform[i].gameObject.SetActive(true);
            }
            else
            {
                // 안보일 부분
                fireTransform[i].gameObject.SetActive(false);
            }
        }
    }
}

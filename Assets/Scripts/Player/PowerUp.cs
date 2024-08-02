using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : RecycleObject
{
    // 1. 풀에서 관리된다.
    // 2. 설정된 방향으로 이동한다
    // 3. 일정 시간 마다 방향이 전환된다.(정해진 확률로 플레이어에서 멀어지는 방향으로 전환되어야 한다.)
    // 4. 보더에 부딪쳐도 방향이 전환된다.
    // 5. 일정 회수 이상 방향이 전환되면 더 이상 방향이 전환되지 않는다.
    // 6. 방향이 전환 될 때마다 더 빠르게 깜박거린다.

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// 방향 전환되는 시간 간격
    /// </summary>
    public float directionChangeInterval = 3.0f;

    /// <summary>
    /// 방향 전환이 가능한 최대 회수
    /// </summary>
    public int directionChangeMaxCount = 5;

    [Range(0f, 1f)]
    /// <summary>
    /// 플레이어로부터 도망칠 확률
    /// </summary>
    public float fleeChange = 0.7f;

    /// <summary>
    /// 파워업 아이템을 최고 단계일 때 먹으면 얻는 보너스 점수
    /// </summary>
    public const int BonusPoint = 1000;

    /// <summary>
    /// 현재 방향 남은 회수
    /// </summary>
    int directionChangeCount = 0;

    /// <summary>
    /// 현재 이동 방향
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// 플레이어의 트랜스폼
    /// </summary>
    Transform playerTransform;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator animator;

    /// <summary>
    /// 에니메이터 파라메터 접근용 해시 결과
    /// </summary>
    readonly int Count_Hash = Animator.StringToHash("Count");

    /// <summary>
    /// 방향 전환 회수 확인 및 설정용 프로퍼티
    /// </summary>
    int DirectionChangeCount
    {
        get => directionChangeCount;
        set
        {
            directionChangeCount = value;           // 값을 지정하고
            animator.SetInteger(Count_Hash, directionChangeCount);  // 애니메이터에 적용

            // 방향 전환 처리
            StopAllCoroutines();    // 이전 코루틴 제거용(벽에 부딪쳤을 때 대비), 수명은 의미없어짐

            // 방향전환할 회수가 남아있고, 활성화되어있으면
            if (directionChangeCount > 0 && gameObject.activeSelf)
            {
                StartCoroutine(DirectionChange());  // DirectionChange 코루틴 실행
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction);    // direction 방향으로 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 방향전환 회수가 남아있고, 부딪친 대상이 보더라면 처리
        if (DirectionChangeCount > 0 && collision.gameObject.CompareTag("Border"))
        {
            // 방향 전환
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);   // 반사된 백터를 새로운 방향으로 설정
            DirectionChangeCount--;     // 방향 전환 회수 감소
        }
    }

    protected override void OnReset()
    {
        playerTransform = GameManager.Instance.Player.transform;    // 플레이어 찾아서 저장해 놓기
        direction = Vector3.zero;                                   // 등장 직후는 안움직이게 하기
        DirectionChangeCount = directionChangeMaxCount;             // 방향 전환 회수 설정하면서 일정 시간 후에 움직이게 만들기
    }

    /// <summary>
    /// 일정 시간 후에 방향을 전환하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(directionChangeInterval);           // 일단 인터벌만큼 대기

        // fleeChange확률로 플레이어 반대방향으로 도망가게 만들기
        Vector2 fleeDir = (transform.position - playerTransform.position).normalized;    // 플레이어 위치에서 파워업 아이템으로 오는 방향
        if (Random.value > fleeChange)
        {            
            fleeDir = -fleeDir; // 근접할 경우에는 방향 반대로
        }
        // 앞에서 구해진 방향을 +-90도 범위로 회전해서 최종 방향 결정
        direction = Quaternion.Euler(0, 0, Random.Range(-90, 90)) * fleeDir;  

        DirectionChangeCount--;     // 방향 전환 회수 감소
    }
}

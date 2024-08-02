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
    // 6. 방향이 전환될 때마다 더 빠르게 깜빡거린다.

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
    Animator animator;

    readonly int Count_Hash = Animator.StringToHash("Count");
    [Range(0f, 1f)]
    /// <summary>
    /// 플레이어로부터 도망칠 확률
    /// </summary>
    public float fleeChange = 0.7f;

    /// <summary>
    /// 아이템을 먹었을 때 최고단계의 레벨이라면 보너스점수를 받음
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

    int DirectionChangeCount
    {
        get => directionChangeCount;
        set
        {
            directionChangeCount = value;
            animator.SetInteger(Count_Hash, directionChangeCount);
            StopAllCoroutines(); //이전 코루틴 제거용(벽에 부딪쳤을때 대비),수명은 의미 없어짐

            // DirectionChange 코루틴 실행
            //방향전환활 회수가 남아있고, 활성화되어있으면
            if (directionChangeCount > 0 && gameObject.activeSelf)
            {
                StartCoroutine(DirectionChange());
            }
        }
    }

    protected override void OnReset()
    {
        playerTransform = GameManager.Instance.Player.transform;
        direction = Vector3.zero;
        DirectionChangeCount = directionChangeMaxCount;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction);
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

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 일정 시간 후에 방향을 전환하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(directionChangeInterval);
        float randomAngle = Random.Range(-90, 90);
        Vector2 playerToItem = (playerTransform.position-transform.position).normalized;
        if (Random.value < fleeChange  /*fleeChange확률에 따라 처리*/)
        {
            // 플레이어에서 멀어지는 방향으로 설정
            direction = Quaternion.Euler(0, 0, randomAngle)*playerToItem;
            
        }
        else
        {
            // 플레이어에게 가까워지는 방향으로 설정
            direction = Quaternion.Euler(0,0,randomAngle)*-playerToItem;
        }
        //direction;    // 방향 최종 결정
        //direction = Random.insideUnitCircle.normalized; //임시

        DirectionChangeCount--;     // 방향 전환 회수 감소
    }
}

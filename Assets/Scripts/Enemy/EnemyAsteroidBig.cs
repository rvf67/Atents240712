using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidBig : EnemyBase
{
    [Header("큰 운석 데이터")]
    /// <summary>
    /// 최소 이동 속도
    /// </summary>
    public float minMoveSpeed = 2.0f;

    /// <summary>
    /// 최대 이동 속도
    /// </summary>
    public float maxMoveSpeed = 4.0f;

    /// <summary>
    /// 최소 회전 속도
    /// </summary>
    public float minRotateSpeed = 30.0f;

    /// <summary>
    /// 최대 회전 속도
    /// </summary>
    public float maxRotateSpeed = 720.0f;

    /// <summary>
    /// 회전 속도 랜덤 분포용 커브
    /// </summary>
    public AnimationCurve rotateSpeedCurve;

    /// <summary>
    /// 최소 자폭 시간
    /// </summary>
    public float minCrushTime = 4.0f;

    /// <summary>
    /// 최대 자폭 시간
    /// </summary>
    public float maxCrushTime = 7.0f;

    /// <summary>
    /// 자폭 표시색 결정용 커브
    /// </summary>
    public AnimationCurve crushCurve;

    /// <summary>
    /// 죽을 때 생성할 작은 운석의 개수(최소)
    /// </summary>
    public int minSmallCount = 3;

    /// <summary>
    /// 죽을 때 생성할 작은 운석의 개수(최대)
    /// </summary>
    public int maxSmallCount = 8;

    [Range(0f, 1f)]
    /// <summary>
    /// 크리티컬 확율
    /// </summary>
    public float criticalRate = 0.05f;

    [Min(1.0f)]
    /// <summary>
    /// 크리티컬이 터졌을 때의 배율
    /// </summary>
    public float criticalMultiplier = 3.0f;

    /// <summary>
    /// 최종 회전 속도
    /// </summary>
    float rotateSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// 원래 점수
    /// </summary>
    int originalPoint = 0;

    /// <summary>
    /// 자폭 목표 시간
    /// </summary>
    float crushTime;

    /// <summary>
    /// 자폭 진행 시간
    /// </summary>
    float crushElapsed = 0.0f;    

    /// <summary>
    /// 운석 스프라이트 랜더러
    /// </summary>
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        originalPoint = point;      // 자폭에 대비해서 미리 저장해 놓기
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnReset()
    {
        base.OnReset();

        point = originalPoint;          // 원래 점수로 복원

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);       // 이동속도 랜덤
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotateSpeed;    // 회전속도 랜덤

        spriteRenderer.color = Color.white; // 자폭 표시 색상 리셋
        crushElapsed = 0.0f;                // 자폭 진행시간 리셋
        StartCoroutine(SelfCrush());        // 자폭 카운트다운 시작
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    protected override void OnVisualUpdate(float deltaTime)
    {
        crushElapsed += deltaTime;
        // 진행율 : crushElapsed/crushTime
        // 시작색 : Color(1,1,1)
        // 마지막색 : Color(1,0,0)
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, crushCurve.Evaluate(crushElapsed / crushTime));
    }

    /// <summary>
    /// 목적지 설정하는 함수
    /// </summary>
    /// <param name="destination">목적지(월드좌표)</param>
    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }

    IEnumerator SelfCrush()
    {
        crushTime = Random.Range(minCrushTime, maxCrushTime);
        yield return new WaitForSeconds(crushTime);
        point = 0;      // 자폭하면 점수는 0점
        Die();
    }

    /// <summary>
    /// 큰 운석이 터질 때 일어날 일을 기록해놓은 함수
    /// </summary>
    protected override void OnDie()
    {
        int count = Random.Range(minSmallCount, maxSmallCount);             // 일단 랜덤으로 정하고
        if( Random.value < criticalRate)
        {
            count = Mathf.RoundToInt(maxSmallCount * criticalMultiplier);   // 크리티컬이면 수정
        }

        float angleDiff = 360.0f / count;   // 사이각 구하기
        Vector3 dir = Quaternion.Euler(0, 0, Random.Range(0,360.0f)) * Vector3.left;    // 왼쪽 벡터를 랜덤하게 돌린 것이 기준 방향
        for (int i = 0; i < count; i++)
        {
            Quaternion q = Quaternion.Euler(0, 0, angleDiff * i);                       // 기준 방향을 단계별로 돌리는 회전 만들기
            Factory.Instance.GetAsteroidSmall(transform.position, q * dir);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

// 큰운석
// 생성 후 랜덤한 시간이 지나면 자폭
// 자폭했을 때는 점수를 얻을 수 없다.
// 큰 운석은 자폭할 시간이 가까워질 수록 빨갛게 변한다.

// 실습 : 큰 운석이 터질 때 작은 운석 만들기
// 1. 죽을 때 작은 운석 생성(랜덤한 개수 min ~ max, 낮은 확률로 max의 3배로 나온다)
// 2. 생성된 작은 운석은 사방으로 퍼져나간다.(터진 큰 운석 위치를 중심으로 모든 작은 운석들이 같은 각도만큼 벌어진체로 날아간다)
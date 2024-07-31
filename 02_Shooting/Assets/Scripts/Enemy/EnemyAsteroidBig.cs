using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    /// 최종 회전 속도
    /// </summary>
    float rotateSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// 생성될 작은 운석의 개수
    /// </summary>
    int randomSmallAmount;
    /// <summary>
    /// 랜덤생성 최소 개수
    /// </summary>
    public int randomSmallMin=3;
    /// <summary>
    /// 랜덤생성 최대 개수
    /// </summary>
    public int randomSmallMax=7;

    [Range(0f, 1f)]
    /// <summary>
    /// 크리티컬 확률
    /// </summary>
    public float criticalPercentage=0.05f;
    /// <summary>
    /// 크리티컬 생성 배율
    /// </summary>
    public float criticalMultiplier = 3.0f;
    
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
        OnDie();
    }

    /// <summary>
    /// 큰 운석이 죽으면 작은 운석으로 분열하며 일정확률로 크리티컬이 터짐
    /// 각도는 일정한 간격으로 퍼짐
    /// </summary>
    protected override void OnDie()
    {
        base.OnDie();
        randomSmallAmount=Random.Range(randomSmallMin,randomSmallMax); //개수 랜점 지정
        if (Random.value < criticalPercentage) //크리티컬이면 수정
        {
            randomSmallAmount=Mathf.RoundToInt(randomSmallMax*criticalMultiplier);
        }
        float angleDiff = 360 / randomSmallAmount; //사이각 구하기
        if (!isAlive)
        {
            for (int i = 0; i < randomSmallAmount; i++)
            {
                Factory.Instance.GetAsteroidSmall(transform.position, Quaternion.Euler(0,0,angleDiff*i)*direction); //일정한 간격으로 생성
            }
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


using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyBoss : EnemyBase
{
    [Header("보스 기본 데이터")]
    //총알은 주기적으로 발사 (Fire1, Fire2 위치)
    //미사일은 방향전환을 할때 마다 일정 수(barrageCount)만큼 연사

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float bulletInterval = 1.0f;

    /// <summary>
    /// 미사일 일제 발사때 발사별 간경
    /// </summary>
    public float barrageInterval = 0.2f;
    /// <summary>
    /// 일제발사 때 발사 횟수
    /// </summary>
    public int barrageCount = 3;

    /// <summary>
    /// 처음 왼쪽으로 가는 시간
    /// </summary>
    public float appearTime = 2.0f;
    /// <summary>
    /// 보스 활동영역 최소 위치
    /// </summary>
    public Vector2 areaMin = new Vector2(2, -3);
    /// <summary>
    /// 보스 활동영역 최대 위치
    /// </summary>
    public Vector2 areaMax = new Vector2(7, 3);

    /// <summary>
    /// 총알발사 위치 1
    /// </summary>
    Transform fire1;
    /// <summary>
    /// 총알발사 위치 2
    /// </summary>
    Transform fire2;
    /// <summary>
    /// 총알발사 위치 3(미사일)
    /// </summary>
    Transform fire3;

    /// <summary>
    /// 보스 이동위치
    /// </summary>
    Vector3 moveDirection = Vector3.left;
    protected override void OnReset()
    {
        base.OnReset();
        StartCoroutine(MovePaternProcess());
    }

    

    private void Awake()
    {
        fire1= transform.GetChild(1).GetChild(0); //각 3개의 총알 발사위치를 찾음
        fire2= transform.GetChild(1).GetChild(1);
        fire3= transform.GetChild(1).GetChild(2);
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * moveDirection); //항상 movedirection방향으로 이동
    }

    /// <summary>
    /// 보스가 총을 쏘는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        while (true) // 무한 루프
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);
            yield return new WaitForSeconds(bulletInterval);  // fireInterval초만큼 기다렸다가 다시 시작하기
        }

    }

    IEnumerator MovePaternProcess()
    {
        moveDirection = Vector3.left;

        yield return null;          // 풀에서 꺼냈을 때 OnReset이 먼저 실행된 후 위치 설정을 하기 때문에,
                                    // 위치 설정 이후에 아래 코드가 실행되도록 한프레임 대기

        float middleX = (areaMax.x - areaMin.x) *0.5f +areaMin.x;
        while(transform.position.x >middleX)
        {
            yield return null; //보스의 x 위치가 middleX의 왼쪽이 될때까지 대기
        }
        StartCoroutine(FireCoroutine());
        ChangeDirection(); //일단 아래로 이동
        while (true)
        {
            if(transform.position.y >areaMax.y || transform.position.y< areaMin.y)
            {
                ChangeDirection();
                StartCoroutine(FIreMissileCorutine());
            }
            yield return null;
        }
    }
    IEnumerator FIreMissileCorutine()
    {
        for(int i = 0;i<barrageCount;i++)
        {
            Factory.Instance.GetBossMissile(fire3.position); //연속 발사 개수만큼 생성
            yield return new WaitForSeconds(barrageInterval);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = new(areaMin.x, areaMin.y);
        Vector3 p1 = new(areaMax.x, areaMin.y);
        Vector3 p2 = new(areaMax.x, areaMax.y);
        Vector3 p3 = new(areaMin.x, areaMax.y);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);

    }

    /// <summary>
    /// 보스의 이동 방향을 변경하는 함수(이동 범위를 벗어 났을 때만 실행되어야 함)
    /// </summary>
    void ChangeDirection()
    {
        Vector3 target = new Vector3();
        target.x = Random.Range(areaMin.x, areaMax.x);  //x위치는 areaMin.x ~ areaMax.x 사이
        target.y = (transform.position.y > areaMax.y) ? areaMin.y : areaMax.y; //areaMax보다 위로 갔으면 아래줄, 아니면 윗줄

        moveDirection =(target - transform.position).normalized; //방향 변경(target으로 가는 방향)
    }
}

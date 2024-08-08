using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine("BossStart");
    }

    private void Awake()
    {
        fire1= transform.GetChild(1).GetChild(0);
        fire2= transform.GetChild(1).GetChild(1);
        fire3= transform.GetChild(1).GetChild(2);
    }

    /// <summary>
    /// 보스가 시작시 일정거리를 이동하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator BossStart()
    {
        //moveDirection = new Vector2(Random.Range(areaMin.x, areaMax.x), areaMax.y).normalized;
        //while (transform.position.y<moveDirection.y)
        //{
        //    transform.Translate(Time.deltaTime * moveSpeed * moveDirection);
        //}
        
        transform.Translate(Time.deltaTime*moveSpeed*moveDirection);
        yield return new WaitForSeconds(appearTime);
        moveSpeed = 0;
        StartCoroutine("FireCoroutine");
        StopCoroutine("BossStart");
    }

    /// <summary>
    /// 보스가 총을 쏘는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        // 코루틴 : 일정 시간 간격으로 코드를 실행하거나 일정 시간동안 딜레이를 줄 때 유용

        while (true) // 무한 루프
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);
            yield return new WaitForSeconds(bulletInterval);  // fireInterval초만큼 기다렸다가 다시 시작하기
        }

        // 프레임 종료시까지 대기
        // yield return null;
        // yield return new WaitForEndOfFrame();
    }
}

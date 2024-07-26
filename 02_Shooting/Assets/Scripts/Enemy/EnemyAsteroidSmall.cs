using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidSmall : EnemyBase
{
    [Header("작은 운석 데이터")]
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
    /// 최종 회전 속도
    /// </summary>
    float rotateSpeed;

    /// <summary>
    /// 이동 방향
    /// </summary>
    Vector3 direction;

    protected override void OnReset()
    {
        base.OnReset();

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotateSpeed;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    /// <summary>
    /// 목적지 설정하는 함수
    /// </summary>
    /// <param name="destination">목적지(월드좌표)</param>
    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

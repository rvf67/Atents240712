using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidSmall : EnemyBase
{
    [Header("���� � ������")]
    /// <summary>
    /// �ּ� �̵� �ӵ�
    /// </summary>
    public float minMoveSpeed = 2.0f;

    /// <summary>
    /// �ִ� �̵� �ӵ�
    /// </summary>
    public float maxMoveSpeed = 4.0f;

    /// <summary>
    /// �ּ� ȸ�� �ӵ�
    /// </summary>
    public float minRotateSpeed = 30.0f;

    /// <summary>
    /// �ִ� ȸ�� �ӵ�
    /// </summary>
    public float maxRotateSpeed = 720.0f;

    /// <summary>
    /// ȸ�� �ӵ� ���� ������ Ŀ��
    /// </summary>
    public AnimationCurve rotateSpeedCurve;

    /// <summary>
    /// ���� ȸ�� �ӵ�
    /// </summary>
    float rotateSpeed;

    /// <summary>
    /// �̵� ����
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
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="destination">������(������ǥ)</param>
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

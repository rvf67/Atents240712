using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidBig : EnemyBase
{
    [Header("ū � ������")]
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
    /// �ּ� ���� �ð�
    /// </summary>
    public float minCrushTime = 4.0f;

    /// <summary>
    /// �ִ� ���� �ð�
    /// </summary>
    public float maxCrushTime = 7.0f;

    /// <summary>
    /// ���� ǥ�û� ������ Ŀ��
    /// </summary>
    public AnimationCurve crushCurve;

    /// <summary>
    /// ���� ȸ�� �ӵ�
    /// </summary>
    float rotateSpeed;

    /// <summary>
    /// �̵� ����
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// ���� ����
    /// </summary>
    int originalPoint = 0;

    /// <summary>
    /// ���� ��ǥ �ð�
    /// </summary>
    float crushTime;

    /// <summary>
    /// ���� ���� �ð�
    /// </summary>
    float crushElapsed = 0.0f;

    /// <summary>
    /// � ��������Ʈ ������
    /// </summary>
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        originalPoint = point;      // ������ ����ؼ� �̸� ������ ����
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnReset()
    {
        base.OnReset();

        point = originalPoint;          // ���� ������ ����

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);       // �̵��ӵ� ����
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotateSpeed;    // ȸ���ӵ� ����

        spriteRenderer.color = Color.white; // ���� ǥ�� ���� ����
        crushElapsed = 0.0f;                // ���� ����ð� ����
        StartCoroutine(SelfCrush());        // ���� ī��Ʈ�ٿ� ����
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * direction, Space.World);
        transform.Rotate(0, 0, deltaTime * rotateSpeed);
    }

    protected override void OnVisualUpdate(float deltaTime)
    {
        crushElapsed += deltaTime;
        // ������ : crushElapsed/crushTime
        // ���ۻ� : Color(1,1,1)
        // �������� : Color(1,0,0)
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, crushCurve.Evaluate(crushElapsed / crushTime));
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="destination">������(������ǥ)</param>
    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }

    IEnumerator SelfCrush()
    {
        crushTime = Random.Range(minCrushTime, maxCrushTime);
        yield return new WaitForSeconds(crushTime);
        point = 0;      // �����ϸ� ������ 0��
        OnDie();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

// ū�
// ���� �� ������ �ð��� ������ ����
// �������� ���� ������ ���� �� ����.
// ū ��� ������ �ð��� ������� ���� ������ ���Ѵ�.

// ���� � �����
// ���� �� ���� � ����(������ ����)
// ������ ���� ��� ������� ����������.
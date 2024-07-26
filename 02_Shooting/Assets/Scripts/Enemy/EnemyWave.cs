using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : EnemyBase
{
    [Header("Wave �� ������")]
    /// <summary>
    /// ���� �׷����� �ѹ� �պ��ϴµ� �ɸ��� �ð� ������(Ŀ������ �պ� �ӵ��� ��������)
    /// </summary>
    public float frequency = 2.0f;

    /// <summary>
    /// ���� �׷����� ������� ������Ű�� ��(��, �Ʒ� �����̴� ����)
    /// </summary>
    public float amplitude = 3.0f;

    /// <summary>
    /// �ð� ������ ����
    /// </summary>
    float elapsedTime = 0.0f;

    /// <summary>
    /// ���� ��ġ �����(������ ��ġ)
    /// </summary>
    float spawnY = 0.0f;

    private void Start()
    {
        spawnY = transform.position.y;      // ���� ��ġ ����ϱ�
    }

    /// <summary>
    /// Wave�� �̵�ó��
    /// </summary>
    /// <param name="deltaTime"></param>
    protected override void OnMoveUpdate(float deltaTime)
    {
        elapsedTime += deltaTime * frequency;   // frequency��ŭ ������ �ð��� ����

        // �� ��ġ ����
        transform.position = new Vector3(
            transform.position.x - deltaTime * moveSpeed,   // ���� x��ġ���� ���� ����
            spawnY + Mathf.Sin(elapsedTime) * amplitude,    // ������ġ���� sin*amplitude �����ŭ ����
            0.0f);
    }
}

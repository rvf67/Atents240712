using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidSmall : EnemyBase
{
    [Header("���� � ������")]


    
    /// <summary>
    /// ������� �⺻ �ӵ�
    /// </summary>
    public float baseSpeed;
    /// <summary>
    /// �ӵ� ����
    /// </summary>
    float speedRandomRange = 1;
    /// <summary>
    /// ���� ��� ȸ�� �ӵ�
    /// </summary>
    public float rotateSpeed = 30.0f;

    /// <summary>
    /// ���� ��� �̵� ����
    /// </summary>
    Vector3? direction = null;

    /// <summary>
    /// ���� ��� �̵� ������ Ȯ���ϰ� �����ϴ� ������Ƽ
    /// </summary>
    public Vector3 Direction
    {
        private get => direction.GetValueOrDefault(); // �б�� private
        set //����� public ������ �ѹ��� ��������
        {
            if(direction == null)
            {
                direction = value.normalized;
            }
        }
    }
    private void Awake()
    {
        baseSpeed = moveSpeed; // ���� �ӵ��� moveSpeed�� ���
    }

    protected override void OnReset()
    {
        base.OnReset();

        moveSpeed =baseSpeed+Random.Range(-speedRandomRange, speedRandomRange);
        rotateSpeed = Random.Range(0, 360); // ȸ�� �ӵ� ����
        direction = null; //������ null�� ���� ������Ƽ�� �ƴ�,Reset ���Ŀ� Direction�� �ѹ� ���� ���� �� �ֵ��� ����
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime*moveSpeed*Direction,Space.World); //�ʴ� moveSpeed�� �ӵ��� Direction �������� �̵�
        transform.Rotate(deltaTime*rotateSpeed*Vector3.forward, Space.World);
    }
}
// ���� � �����
//1. ���� �� ���� � ����(������ ���� min~max,���� Ȯ���� max�� 3��� ���´�.)
//2. ������ ���� ��� ������� ����������.(���� ū � ��ġ�� �߽����� ��� ���� ����� ���� ������ŭ ��������� ���ư���.)

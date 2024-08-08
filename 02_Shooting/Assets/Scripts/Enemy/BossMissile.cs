using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : EnemyBase
{
    //HP�� 1�̰� ��Ʈ���� �� ������ 0��

    //�������ڸ��� �÷��̾ ��ô��(�÷��̾� �������� �̵�)
    //�ڽ��� Ʈ���� �ȿ� �÷��̾ ������ �� �ķ� ���� ����
    //���� ������ ������ �� �ִ� ���� �����

    [Header("���� �̻��� ������")]
    /// <summary>
    /// �̻����� ���� ����. ���� ���� ������ target�������� ȸ��
    /// </summary>
    public float guidedPerformance = 1.5f;

    /// <summary>
    /// �������
    /// </summary>
    Transform target;

    /// <summary>
    /// ���� ������ ǥ���ϴ� ���� true�� ������
    /// </summary>
    bool isGuided = true;
    protected override void OnReset()
    {
        base.OnReset();
        target = GameManager.Instance.Player.transform;
        isGuided = true;
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        if (isGuided)
        {
            Vector2 direcrion= target.position-transform.position;  //target ��ġ�� ���� ����

            //�÷��̾������� õõ�� ȸ���ϰ� �����
            transform.right = -Vector3.Slerp(-transform.right,direcrion,deltaTime*guidedPerformance);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGuided && collision.CompareTag("Player"))
        {
            isGuided = false;
        }
    }
}

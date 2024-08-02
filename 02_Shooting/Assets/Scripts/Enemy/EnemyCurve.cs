using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurve : EnemyBase
{
    [Header("Ŀ�굵�� �� ������")]
    public float rotateSpeed = 10.0f;

    /// <summary>
    /// ȸ������(1�̸� �ݽð����, -1�̸� �ð� ����)
    /// </summary>
    float curveDirection = 1.0f;

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        

        //�ʴ�, rotateSpeed�� �ӵ���, curveDirection �������� z�� ȸ��
        transform.Rotate(deltaTime*rotateSpeed*curveDirection*Vector3.forward);
    }

    public void UpdateRotateDirection()
    {
        if (transform.position.y < 0)
        {
            //�Ʒ����̸� ��ȸ��
            curveDirection = -1;
        }
        else
        {
            //�����̸� ��ȸ��
            curveDirection = 1;
        }
    }
}

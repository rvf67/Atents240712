using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test17_Enemies : TestBase
{
    // ���߰�
    //1. EnemyBonus : ���� �� �Ŀ��� �������� ����. �����Ǿ��� �� ������ ���� ������ �� ��� ����ϰ� �� ���Ŀ� �ٽ� �����δ�.
    //2. EnemyCurve : Ŀ�긦 ���� �������� �Ѵ�. ���ʿ� ���� �Ǿ����� ��ȸ��, �Ʒ��ʿ� �����Ǿ����� ��ȸ��
    //3. ��Ƽ�����ʿ� �����ϱ�
    public Transform target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyCurve(target.position);
    }
}

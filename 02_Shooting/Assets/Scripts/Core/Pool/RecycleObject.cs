using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    //��������Ʈ - �Լ��� ������ �� �ִ� ������ Ÿ��. �� ���뵵 = Ư�� ��Ȳ���� �˸��� ���� ���.
    //Action : �Ķ���Ϳ� ������ ���� �Լ��� ���� ������ ��������Ʈ
    //Func<T> : ������ �ݵ�� �ְ� �Ķ���͵� ���� ������ ��������Ʈ

    /// <summary>
    /// ��Ȱ�� ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action onDisable = null;


    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {
        onDisable?.Invoke();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    //델리게이트 - 함수를 저장할 수 있는 데이터 타입. 주 사용용도 = 특정 상황임을 알리기 위해 사용.
    //Action : 파라미터와 리턴이 없는 함수만 저장 가능한 델리게이트
    //Func<T> : 리턴은 반드시 있고 파라미터도 설정 가능한 델리게이트

    /// <summary>
    /// 재활용 오브젝트가 비활성화 될 때 실행되는 델리게이트
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

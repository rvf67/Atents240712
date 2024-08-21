using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 문의 부모 클래스
public class DoorBase : MonoBehaviour
{
    Animator animator;

    readonly int IsOpen_Hash = Animator.StringToHash("IsOpen");

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 문이 열릴 때 각 종류별로 따로 처리해야하는 일을 기록할 가상함수(빈함수)
    /// </summary>
    protected virtual void OnOpen() {}

    /// <summary>
    /// 문이 닫힐 때 각 종류별로 따로 처리해야하는 일을 기록할 가상함수(빈함수)
    /// </summary>
    protected virtual void OnClose() {}

    /// <summary>
    /// 문을 여는 함수
    /// </summary>
    public void Open()
    {
        animator.SetBool(IsOpen_Hash, true);
        OnOpen();
    }

    /// <summary>
    /// 문을 닫는 함수
    /// </summary>
    public void Close()
    {
        OnClose();
        animator.SetBool(IsOpen_Hash, false);
    }
}

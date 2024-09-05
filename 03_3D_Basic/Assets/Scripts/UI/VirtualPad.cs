using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPad : MonoBehaviour
{
    /// <summary>
    /// 이 패드가 가진 모든 가상 스틱
    /// </summary>
    VirtualStick[] sticks;

    /// <summary>
    /// 이 패드가 가진 모든 가상 버튼
    /// </summary>
    VirtualButton[] buttons;

    //Player[] targets;

    private void Awake()
    {
        // 자식으로 있는 것 모두 찾기
        sticks = GetComponentsInChildren<VirtualStick>();
        buttons = GetComponentsInChildren<VirtualButton>();
    }

    /// <summary>
    /// 특정 스틱을 리턴하는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public VirtualStick GetStick(int index)
    {
        return sticks[index];
    }

    /// <summary>
    /// 특정 버튼을 리턴하는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public VirtualButton GetButton(int index)
    {
        return buttons[index];
    }

    /// <summary>
    /// 특정 스틱에 기능을 바인딩하는 함수
    /// </summary>
    /// <param name="index">스틱의 인덱스</param>
    /// <param name="moveInput">바인딩 될 함수</param>
    public void SetStickBind(int index, Action<Vector2> moveInput)
    {
        sticks[index].onMoveInput = moveInput;
    }

    /// <summary>
    /// 특정 버튼에 기능을 바인딩하는 함수
    /// </summary>
    /// <param name="index">버튼의 인덱스</param>
    /// <param name="onClick">버튼이 눌려졌을 때 실행될 함수</param>
    /// <param name="onCoolTimeChange">버튼의 쿨타임 변화를 알리는 델리게이트</param>
    public void SetButtonBind(int index, Action onClick, ref Action<float> onCoolTimeChange)
    {
        buttons[index].onClick = onClick;
        onCoolTimeChange += buttons[index].RefreshCoolTime;
    }

    /// <summary>
    /// 가상 패드 연결을 해제하는 함수
    /// </summary>
    public void Disconnect()
    {
        foreach (VirtualStick stick in sticks)
        {
            stick.onMoveInput = null;   // 스틱에 연결된 함수들 제거
        }
        foreach (VirtualButton button in buttons)
        {
            button.onClick = null;      // 버튼에 연결된 함수들 제거
            //onCoolTimeChange -= button.RefreshCoolTime;   // 쿨타임 변화 알림을 더 이상 받지 않기
        }
    }
}

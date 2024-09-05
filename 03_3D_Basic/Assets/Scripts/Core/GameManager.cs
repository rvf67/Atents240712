using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 플레이어 확인용 프로퍼티
    /// </summary>
    public Player Player => player;

    ///// <summary>
    ///// 가상 스틱
    ///// </summary>
    //VirtualStick stick;

    ///// <summary>
    ///// 가상 스틱 확인용 프로퍼티
    ///// </summary>
    //public VirtualStick Stick => stick;

    ///// <summary>
    ///// 가상버튼(점프)
    ///// </summary>
    //VirtualButton jumpButton;

    ///// <summary>
    ///// 가상버튼(점프) 확인용 프로퍼티
    ///// </summary>
    //public VirtualButton JumpButton => jumpButton;

    /// <summary>
    /// 가상 패드
    /// </summary>
    VirtualPad virtualPad;

    /// <summary>
    /// 가상패드 참조용 프로퍼티
    /// </summary>
    public VirtualPad VirtualPad => virtualPad;


    /// <summary>
    /// 초기화용 함수
    /// </summary>
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        //stick = FindAnyObjectByType<VirtualStick>();
        //jumpButton = FindAnyObjectByType<VirtualButton>();

        virtualPad = FindAnyObjectByType<VirtualPad>();
    }


}

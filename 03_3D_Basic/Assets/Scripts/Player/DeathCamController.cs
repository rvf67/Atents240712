using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamController : MonoBehaviour
{
    /// <summary>
    /// 카트 최대 속도
    /// </summary>
    public float cartMaxSpeed = 10.0f;
    /// <summary>
    /// 카트 최저 속도
    /// </summary>
    public float cartMinSpeed =3.0f;
    /// <summary>
    /// 카트의 속도가 최대에서 최저로 떨어지는데 걸리는 시간
    /// </summary>
    public float speedDecreaseDuration = 3.0f;
    /// <summary>
    /// 카트의 속도 변화를 나타내는 커브
    /// </summary>
    public AnimationCurve cartSpeedCurve;
    /// <summary>
    /// 사망시간으로부터 얼마나 흘렀는지
    /// </summary>
    private float elapsedTime;
    /// <summary>
    /// 죽은 신호
    /// </summary>
    private bool isStart;
    CinemachineVirtualCamera vcam;
    CinemachineDollyCart cart;
    Player player;
    Transform playerCameraRoot;

    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        cart = GetComponentInChildren<CinemachineDollyCart>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        playerCameraRoot=player.transform.GetChild(8);
        player.onDie += DeathCamStart; //플레이어가 죽으면 시작
    }
    private void Update()
    {
        if (isStart) //죽었자는 신호가 오면
        {
            transform.position = playerCameraRoot.transform.position; //플레이어 카메라 루트 위치로 옮기고
                
            elapsedTime += Time.deltaTime;                              //시간 흐름 저장


            float ratio = cartSpeedCurve.Evaluate(elapsedTime / speedDecreaseDuration);
            cart.m_Speed = cartMinSpeed + (cartMaxSpeed - cartMinSpeed) * ratio; //카트 속도 조절
        }
    }
    //사망 카메라 연출시작
    private void DeathCamStart()
    {
        isStart = true;         //시작되여야한다고 표시
        vcam.Priority = 100;    //카메라 우선 순위 높여서 이 카메라로 찍히게 만들기
        cart.m_Speed = cartMaxSpeed;    //카트 속도 최대치로 변경
        cart.m_Position = 0;            //카트 위치 리셋
        elapsedTime = 0.0f;             //시작부터 시간이 얼마나 지났는지 측정하기 위한 값 리셋
    }
}

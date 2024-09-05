using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamController : MonoBehaviour
{
    public float cartSpeed = 20.0f;
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
        playerCameraRoot=player.transform.GetChild(8);
        player = GameManager.Instance.Player;
        player.onDie += DeathCamStart; //플레이어가 죽으면 시작
    }
    private void Update()
    {
        transform.position = player.transform.position;

    }
    //사망 카메라 연출시작
    private void DeathCamStart()
    {
        vcam.Priority = 100;
        cart.m_Speed = cartSpeed;
    }
}

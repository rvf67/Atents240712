using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    /// <summary>
    /// 숫자 증가 속도
    /// </summary>
    public float countingSpeed = 10.0f;

    /// <summary>
    /// 목표시간
    /// </summary>
    float target = 0.0f;
    /// <summary>
    /// 현재시간
    /// </summary>
    float current = 0.0f;
    ImageNumber imageNumber;
    Player player;
    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        player.onKillCountChange += OnKillCountChange;
    }
    private void Update()
    {
        current += Time.deltaTime * countingSpeed;
        if(current > target)
        {
            current =target;                            //target까지만 설정
        }
        imageNumber.Number =Mathf.FloorToInt(current);  //current를 이미지 넘버에 설정

    }
    private void OnKillCountChange(int count)
    {
        target = count;
    }

    // 숫자에 증가 속도 적용
}
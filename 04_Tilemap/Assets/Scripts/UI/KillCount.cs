using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    ImageNumber imageNumber;

    private void Awake()
    {
        imageNumber = GetComponent<ImageNumber>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onKillCountChange += OnKillCountChange;
    }

    private void OnKillCountChange(int count)
    {
        imageNumber.Number = count;
    }

    // 숫자에 증가 속도 적용
}
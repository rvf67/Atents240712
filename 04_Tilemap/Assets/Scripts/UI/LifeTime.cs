using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeTime : MonoBehaviour
{
    public Gradient color;

    float maxLifeTime = 0.0f;

    Slider timeSlider;
    Image fill;
    TextMeshProUGUI timeText;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        timeSlider = child.GetComponent<Slider>();  // Slider
        child = timeSlider.transform.GetChild(1);   // Fill Area
        child = child.GetChild(0);                  // Fill
        fill = child.GetComponent<Image>();
        child = transform.GetChild(1);              // Text (TMP)
        timeText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onLifeTimeChange += OnLifeTimeChange;
        maxLifeTime = player.MaxLifeTime;

        OnLifeTimeChange(1);    // 첫 초기화
    }

    /// <summary>
    /// 플레이어의 수명이 변경될 때마다 호출되는 함수
    /// </summary>
    /// <param name="ratio">수명이 남은 비율(0~1)</param>
    private void OnLifeTimeChange(float ratio)
    {
        timeSlider.value = ratio;
        fill.color = color.Evaluate(ratio);
        timeText.text = $"{maxLifeTime * ratio:f2} sec";
    }
}

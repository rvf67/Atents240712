using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerClickHandler
{
    // 1. 누르면 플레이어가 점프한다.
    // 2. 플레이어 쿨다운 변화에 따라 CoolTime이미지의 FillAmount가 변화한다.

    Image coolDown;

    public Action onClick;

    void Awake()
    {
        Transform child = transform.GetChild(1);
        coolDown = child.GetComponent<Image>();
        coolDown.fillAmount = 0.0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    /// <summary>
    /// 쿨타임 표시 갱신하는 함수
    /// </summary>
    /// <param name="ratio">비율</param>
    public void RefreshCoolTime(float ratio)
    {
        coolDown.fillAmount = ratio;
    }
}

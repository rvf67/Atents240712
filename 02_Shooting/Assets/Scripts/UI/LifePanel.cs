using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    /// <summary>
    /// 수명칸(비행기그림)이 비활성화 되었을 때의 색상
    /// </summary>
    public Color disableColor;

    /// <summary>
    /// 자식으로 있는 이미지의 배열
    /// </summary>
    Image[] lifeImages;

    private void Awake()
    {
        lifeImages = new Image[transform.childCount];
        for (int i = 0; i < lifeImages.Length; i++)
        {
            Transform child = transform.GetChild(i);
            lifeImages[i] = child.GetComponent<Image>();    // 자식으로 있는 이미지 저장해 놓기       
        }
    }

    /// <summary>
    /// 초기화 때 실행될 함수(플레이어 초기화보다 늦어야 한다.)
    /// </summary>
    public void OnInitialize()
    {
        Player player = GameManager.Instance.Player;
        player.onLifeChange += OnLifeChange;        // 플레이어 수명이 변경될 때 실행될 함수 등록
    }

    /// <summary>
    /// Life가 변경되었을 때 실행될 함수
    /// </summary>
    /// <param name="life">현재 Life</param>
    private void OnLifeChange(int life)
    {
        for (int i = 0; i < life; i++)
        {
            lifeImages[i].color = Color.white;      // 남아있는 생명은 정상적으로 보이기
        }
        for (int i = life; i < lifeImages.Length; i++)
        {
            lifeImages[i].color = disableColor;     // 날아간 생명은 비활성화된 색으로 보이게 하기
        }
    }
}

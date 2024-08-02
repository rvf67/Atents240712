using System;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    // 자식으로 있는 슬롯(bgSlots)을 일정한 속도(scrollingSpeed)로 계속 왼쪽으로 이동시키다가,
    // 슬롯이 화면을 벗어나면(slotWidth, baseLineX) 오른쪽 끝(slotWidth*3)으로 보낸다.

    /// <summary>
    /// 슬롯들의 이동 속도
    /// </summary>
    public float scrollingSpeed = 2.5f;

    /// <summary>
    /// 슬롯하나의 가로 길이
    /// </summary>
    protected float slotWidth;

    /// <summary>
    /// 배경 슬롯
    /// </summary>
    Transform[] bgSlots;

    /// <summary>
    /// 배경을 그리는 랜더러들(슬롯에 들어있는 모든 랜더러)
    /// </summary>
    protected SpriteRenderer[] spriteRenderers;

    /// <summary>
    /// 화면 밖을 벗어났다는 것을 확인하기 위한 기준선(x좌표값)
    /// </summary>
    protected float baseLineX;

    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];      // 슬롯의 트랜스폼을 저장하기 위한 배열 만들기
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);             // 슬롯의 트랜스폼을 하나씩 저장
        }

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();    // 슬롯들에 들어있는 모든 랜더러 찾기
        if(spriteRenderers.Length > 0 )
        {
            Sprite sprite = spriteRenderers[0].sprite;
            // 스프라이트의 가로길이와 pixelPerUnit을 이용해, UnityUnit으로 길이가 얼마가 되는지 계산            
            slotWidth = sprite.textureRect.width / sprite.pixelsPerUnit;
        }
    }

    private void Update()
    {
        for (int i = 0; i < bgSlots.Length; i++)            // 모든 슬롯을 순서대로 처리
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.right);   // 왼쪽으로 이동하기(초당 scrollingSpeed만큼)
            if (bgSlots[i].position.x < baseLineX)          // 충분히 왼쪽으로 갔는지 확인(기준선을 넘었는지 확인)
            {
                MoveRightEnd(i);   // 충분히 왼쪽으로 이동했으면 오른쪽 끝으로 보내기
            }
        }
    }

    /// <summary>
    /// 오른쪽 끝으로 슬롯을 이동시키는 함수
    /// </summary>
    /// <param name="index">이동시킬 슬롯의 인덱스</param>
    void MoveRightEnd(int index)
    {
        bgSlots[index].Translate(slotWidth * bgSlots.Length * transform.right); // 슬롯 가로길이 * 슬롯 개수만큼 오른쪽으로 이동
        OnMoveRightEnd(index);
    }

    /// <summary>
    /// 오른쪽 끝으로 이동했을 때 상속받은 클래스 별로 따로 처리해야하는 코드를 넣는 함수
    /// </summary>
    /// <param name="index">이동한 슬롯의 인덱스</param>
    protected virtual void OnMoveRightEnd(int index)
    {
    }
}

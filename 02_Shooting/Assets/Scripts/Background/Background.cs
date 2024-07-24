using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Background : MonoBehaviour
{
    //자식으로 있는 슬롯을 일정한 속도로 계속 왼쪽으로 이동시킫다가, 슬롯이 화면을 벗어나면 오른쪽 끝으로 보낸다.
    //슬롯이 화면을 벗어나면(SlotWidth) 오른쪽 끝(SlotWidth*3) 으로 보낸다.
    public float scrollingSpeed = 2.5f;

    const float SlotWidth = 13.6f;
    Transform[] bgSlots;

    float baseLineX;
    private void Awake()
    {
        bgSlots = new Transform[transform.childCount];  //슬롯의 트랜스폼을 저장하기 위한 배열 생성
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);    //슬롯의 트랜스폼을 하나씩 저장
        }

        baseLineX = transform.position.x-SlotWidth; //기준선 계산
    }
    private void Update()
    {
        for (int i = 0;i < bgSlots.Length;i++)              //모든 슬롯을 순서대로 처리
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.right);   //왼쪽으로 이동하기(초당 scrollingSpeed만큼)
            if (bgSlots[i].position.x < baseLineX)  //충분히 왼쪽으로 갔는지 확인(기준선을 넘었는지 확인
            {
                MoveRight(i); //오른쪽 끝으로 보내기
            }
        }

    }
    /// <summary>
    /// 오른쪽 끝으로 슬롯을 이동시키는 함수
    /// </summary>
    /// <param name="index">이동시킬 슬롯의 인덱스</param>
    protected virtual void MoveRight(int index)
    {
        bgSlots[index].Translate(SlotWidth*bgSlots.Length*transform.right); //슬롯 가로길이*슬롯개수만큼 오른쪽으로 이동
    }
}

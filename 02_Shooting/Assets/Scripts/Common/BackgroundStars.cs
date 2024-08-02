using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStars : Scrolling
{
    // 실습
    // 오른쪽 끝으로 이동할 때 SpriteRenderer의 flipX와 flipY가 랜덤하게 변경된다.

    protected override void Awake()
    {
        base.Awake();   // 부모인 Background의 Awake함수 실행

        baseLineX = transform.position.x - slotWidth * 0.5f;    // Stars는 피봇이 가운데 있기 때문에 절반만 이동
    }

    protected override void OnMoveRightEnd(int index)
    {
        int rand = Random.Range(0, 4);  // 0~3 사이의 값을 랜덤으로 구하기( 나올 수 있는 경우의 수는 4가지이기 때문)

        // rand =  0(0b_00), 1(0b_01), 2(0b_10), 3(0b_11) 중 하나

        spriteRenderers[index].flipX = ((rand & 0b_01) != 0);   // 1 아니면 3이다(첫번째 비트가 1이면 true)
        spriteRenderers[index].flipY = ((rand & 0b_10) != 0);   // 2 아니면 3이다(두번째 비트가 1이면 true)

        // c#에서 숫자 앞에 "0b_"를 붙이면 2진수라는 의미
        // c#에서 숫자 앞에 "0x_"를 붙이면 16진수라는 의미
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : Scrolling
{
    protected override void Awake()
    {
        base.Awake();

        baseLineX = transform.position.x - slotWidth;       // 기준선 계산(현위치에서 슬롯 크기만큼 왼쪽으로 간 x위치)
    }

    protected override void OnMoveRightEnd(int index)
    {
        // 홀짝으로 플립방향 선택하기
        //int rand = Random.Range(0, 2);
        //spriteRenderers[index].flipX = (rand % 2) != 0; // 홀수면 true, 짝수면 false

        // 0.0 ~ 1.0 사이의 랜덤값을 받아와서 확인
        float rand = Random.value;
        spriteRenderers[index * 2].flipX = rand < 0.5f;
        rand = Random.value;
        spriteRenderers[index * 2 + 1].flipX = rand < 0.5f;

        // Debug.Log(spriteRenderers[index * 2].transform.parent.gameObject.name);
        // Debug.Log(spriteRenderers[index * 2 + 1].transform.parent.gameObject.name);
    }
}

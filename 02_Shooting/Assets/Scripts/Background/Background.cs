using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : Scrolling
{
    protected override void Awake()
    {
        base.Awake();

        baseLineX = transform.position.x - slotWidth;       // ���ؼ� ���(����ġ���� ���� ũ�⸸ŭ �������� �� x��ġ)
    }

    protected override void OnMoveRightEnd(int index)
    {
        // Ȧ¦���� �ø����� �����ϱ�
        //int rand = Random.Range(0, 2);
        //spriteRenderers[index].flipX = (rand % 2) != 0; // Ȧ���� true, ¦���� false

        // 0.0 ~ 1.0 ������ �������� �޾ƿͼ� Ȯ��
        float rand = Random.value;
        spriteRenderers[index * 2].flipX = rand < 0.5f;
        rand=Random.value;
        spriteRenderers[index*2+1].flipX = rand < 0.5f;
    }
}

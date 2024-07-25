using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStars : Scrolling
{
    // �ǽ�
    // ������ ������ �̵��� �� SpriteRenderer�� flipX�� flipY�� �����ϰ� ����ȴ�.

    protected override void Awake()
    {
        base.Awake();   // �θ��� Background�� Awake�Լ� ����

        baseLineX = transform.position.x - slotWidth * 0.5f;    // Stars�� �Ǻ��� ��� �ֱ� ������ ���ݸ� �̵�
    }

    protected override void OnMoveRightEnd(int index)
    {
        int rand = Random.Range(0, 4);  // 0~3 ������ ���� �������� ���ϱ�( ���� �� �ִ� ����� ���� 4�����̱� ����)

        // rand =  0(0b_00), 1(0b_01), 2(0b_10), 3(0b_11) �� �ϳ�

        spriteRenderers[index].flipX = ((rand & 0b_01) != 0);   // 1 �ƴϸ� 3�̴�(ù��° ��Ʈ�� 1�̸� true)
        spriteRenderers[index].flipY = ((rand & 0b_10) != 0);   // 2 �ƴϸ� 3�̴�(�ι�° ��Ʈ�� 1�̸� true)

        // c#���� ���� �տ� "0b_"�� ���̸� 2������� �ǹ�
        // c#���� ���� �տ� "0x_"�� ���̸� 16������� �ǹ�
    }
}

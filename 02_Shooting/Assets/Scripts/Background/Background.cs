using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Background : MonoBehaviour
{
    //�ڽ����� �ִ� ������ ������ �ӵ��� ��� �������� �̵��õT�ٰ�, ������ ȭ���� ����� ������ ������ ������.
    //������ ȭ���� �����(SlotWidth) ������ ��(SlotWidth*3) ���� ������.
    public float scrollingSpeed = 2.5f;

    const float SlotWidth = 13.6f;
    Transform[] bgSlots;

    float baseLineX;
    private void Awake()
    {
        bgSlots = new Transform[transform.childCount];  //������ Ʈ�������� �����ϱ� ���� �迭 ����
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);    //������ Ʈ�������� �ϳ��� ����
        }

        baseLineX = transform.position.x-SlotWidth; //���ؼ� ���
    }
    private void Update()
    {
        for (int i = 0;i < bgSlots.Length;i++)              //��� ������ ������� ó��
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.right);   //�������� �̵��ϱ�(�ʴ� scrollingSpeed��ŭ)
            if (bgSlots[i].position.x < baseLineX)  //����� �������� ������ Ȯ��(���ؼ��� �Ѿ����� Ȯ��
            {
                MoveRight(i); //������ ������ ������
            }
        }

    }
    /// <summary>
    /// ������ ������ ������ �̵���Ű�� �Լ�
    /// </summary>
    /// <param name="index">�̵���ų ������ �ε���</param>
    protected virtual void MoveRight(int index)
    {
        bgSlots[index].Translate(SlotWidth*bgSlots.Length*transform.right); //���� ���α���*���԰�����ŭ ���������� �̵�
    }
}

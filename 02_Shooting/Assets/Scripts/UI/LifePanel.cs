using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    /// <summary>
    /// ����ĭ(�����׸�)�� ��Ȱ��ȭ �Ǿ��� ���� ����
    /// </summary>
    public Color disableColor;

    /// <summary>
    /// �ڽ����� �ִ� �̹����� �迭
    /// </summary>
    Image[] lifeImages;

    private void Awake()
    {
        lifeImages = new Image[transform.childCount];
        for (int i = 0; i < lifeImages.Length; i++)
        {
            Transform child = transform.GetChild(i);
            lifeImages[i] = child.GetComponent<Image>();    // �ڽ����� �ִ� �̹��� ������ ����       
        }
    }

    /// <summary>
    /// �ʱ�ȭ �� ����� �Լ�(�÷��̾� �ʱ�ȭ���� �ʾ�� �Ѵ�.)
    /// </summary>
    public void OnInitialize()
    {
        Player player = GameManager.Instance.Player;
        player.onLifeChange += OnLifeChange;        // �÷��̾� ������ ����� �� ����� �Լ� ���
    }

    /// <summary>
    /// Life�� ����Ǿ��� �� ����� �Լ�
    /// </summary>
    /// <param name="life">���� Life</param>
    private void OnLifeChange(int life)
    {
        for (int i = 0; i < life; i++)
        {
            lifeImages[i].color = Color.white;      // �����ִ� ������ ���������� ���̱�
        }
        for (int i = life; i < lifeImages.Length; i++)
        {
            lifeImages[i].color = disableColor;     // ���ư� ������ ��Ȱ��ȭ�� ������ ���̰� �ϱ�
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    /// <summary>
    /// �гο��� ���̴� ��� ��ũ ����
    /// </summary>
    RankLine[] rankLines;

    /// <summary>
    /// ��Ŀ �̸� �Է��� ���� ��ǲ �ʵ�
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// �ְ� ������ �̸�(1��~5�� ������ ���ĵǾ� ����)
    /// </summary>
    string[] rankers;

    /// <summary>
    /// �ְ� ����(1��~5�� ������ ���ĵǾ� ����)
    /// </summary>
    int[] highRecords;

    /// <summary>
    /// ��ŷ ǥ�õǴ� ��� ��
    /// </summary>
    const int MaxRankings = 5;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];

        inputField = GetComponentInChildren<TMP_InputField>(true);  // ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� ã������ �Ķ���͸� true�� �ؾ� �Ѵ�.
    }

    /// <summary>
    /// ��ŷ �����͸� �ʱⰪ���� �����ϴ� �Լ�
    /// </summary>
    void SetDefaultData()
    {
        /// 1st AAA 1,000,000
        /// 2nd BBB 100,000
        /// 3rd CCC 10,000
        /// 4th DDD 1,000
        /// 5th EEE 100

        int score = 1000000;

        for (int i = 0; i < MaxRankings; i++)
        {
            char temp = 'A';                // 'A' == 65
            temp = (char)((byte)temp + i);  // i��ŭ ���� = A���� i��ŭ ������ ���ڷ� ����
            rankers[i] = $"{temp}{temp}{temp}"; // AAA ~ EEE

            highRecords[i] = score;
            score = Mathf.RoundToInt(score * 0.1f); // �ѹ��� �� ������ 1/10�� ���̱�
        }

        RefreshRankLines();
    }

    /// <summary>
    /// �г� ���̴� �κ� ���ſ� �Լ�
    /// </summary>
    void RefreshRankLines()
    {
        for (int i = 0; i < MaxRankings; i++)
        {
            rankLines[i].SetData(rankers[i], highRecords[i]);   // ����� �����ʹ�� �ٽ� ǥ��
        }
    }

    /// <summary>
    /// ��ŷ �����͸� ������Ʈ�ϴ� �Լ�
    /// </summary>
    /// <param name="score">�� ����</param>
    void UpdataRankData(int score)
    {
        for (int i = 0; i < MaxRankings; i++)
        {
            if (highRecords[i] < score)
            {
                // �ű���̴�.
                for (int j = MaxRankings - 1; j > i; j--)            // ������+1�������� i������ ����
                {
                    rankers[j] = rankers[j - 1];
                    highRecords[j] = highRecords[j - 1];        // �Ѵܰ� �Ʒ������� ������                    
                }

                rankers[i] = "�� ��Ŀ";                        // �� ��Ŀ�̸��� ���� ����ϱ�
                highRecords[i] = score;

                Vector3 pos = inputField.transform.position;    // ����� �°� ��ġ �̵�
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;
                inputField.gameObject.SetActive(true);          // ��ǲ�ʵ� ���̰� �����

                RefreshRankLines(); // ȭ�� ����
                break;              // for�� ������
            }
        }
    }

#if UNITY_EDITOR

    public void Test_DefaultRankPanel()
    {
        SetDefaultData();
    }

    public void Test_UpdateRankPanel(int score)
    {
        UpdataRankData(score);
    }
#endif
}

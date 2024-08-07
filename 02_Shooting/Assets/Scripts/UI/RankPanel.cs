using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// null�� �ƴϸ� ������ ��ŷ�� ������Ʈ �� �ε���
    /// </summary>
    int? updatedIndex = null;

    /// <summary>
    /// ��ŷ ǥ�õǴ� ��� ��
    /// </summary>
    const int MaxRankings = 5;

    /// <summary>
    /// ���̺� ���� �̸�
    /// </summary>
    const string SaveFileName = "Save.json";

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];

        inputField = GetComponentInChildren<TMP_InputField>(true);  // ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� ã������ �Ķ���͸� true�� �ؾ� �Ѵ�.
        inputField.onEndEdit.AddListener(OnNameInputEnd);   // onEndEdit�� �ߵ����θ� Ȯ���� �Լ� ���(=onEndEdit �̺�Ʈ�� �ߵ��� �� ����� �Լ� �߰�)
    }

    private void Start()
    {
        LoadRankData();     // ��ũ ���� �ҷ�����        
        GameManager.Instance.Player.onDie += () => UpdataRankData(GameManager.Instance.Score);  // �÷��̾ ������ ��ŷ ������Ʈ �õ�
    }

    /// <summary>
    /// inputField.onEndEdit �̺�Ʈ�� �ߵ��Ǿ��� �� ����� �Լ�
    /// </summary>
    /// <param name="inputText">inputField�� �Է��� �Ϸ�Ǿ��� ���� �Է� ����</param>
    private void OnNameInputEnd(string inputText)
    {
        //Debug.Log(inputText);

        inputField.gameObject.SetActive(false);     // ��ǲ�ʵ� ��Ȱ��ȭ
        if (updatedIndex != null)
        {
            rankers[updatedIndex.Value] = inputText;    // �̸� ����
            RefreshRankLines(updatedIndex.Value);       // �г� ȭ�� ����
            SaveRankData();                             // ��ŷ ���� ����
            updatedIndex = null;
        }
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
    /// �гο��� Ư�� ���θ� ������Ʈ
    /// </summary>
    /// <param name="index">������Ʈ�� ������ �ε���</param>
    void RefreshRankLines(int index)
    {
        rankLines[index].SetData(rankers[index], highRecords[index]);   // ����� �����ʹ�� �ٽ� ǥ��
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
                updatedIndex = i;                               // ������Ʈ�� �ε��� �����ϱ�

                Vector3 pos = inputField.transform.position;    // ����� �°� ��ġ �̵�
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;
                inputField.text = string.Empty;                 // ��ǲ�ʵ� ���� ����
                inputField.gameObject.SetActive(true);          // ��ǲ�ʵ� ���̰� �����

                RefreshRankLines(); // ȭ�� ����
                break;              // for�� ������
            }
        }
    }

    /// <summary>
    /// ��ŷ ������ ���Ϸ� �����ϴ� �Լ�
    /// </summary>
    void SaveRankData()
    {
        // Assets/Save ������ Save.json�̶�� �̸����� �����ϱ�
        SaveData data = new SaveData();             // ����ȭ ������ Ŭ���� �Ҵ�
        data.rankers = rankers;                     // ��ü�� ������ �ֱ�
        data.highRecords = highRecords;
        string jsonText = JsonUtility.ToJson(data); // ��ü ������ json ���ڿ��� ����

        string path = $"{Application.dataPath}/Save/";  // ���� ��� �����س���
        if (!Directory.Exists(path))                   // �ش� ������ ������ Ȯ��
        {
            Directory.CreateDirectory(path);            // ������ ������ �����.
        }

        File.WriteAllText($"{path}{SaveFileName}", jsonText);    // ���Ϸ� ����
    }

    /// <summary>
    /// ��ŷ ������ ���Ͽ��� �ҷ����� �Լ�
    /// </summary>
    void LoadRankData()
    {
        bool isSuccess = false;

        // Assets/Save ������ �ִ� Save.json�̶�� ������ �о ��ŷ ���� �����
        string path = $"{Application.dataPath}/Save/";
        if (Directory.Exists(path))
        {
            // ������ �ִ�
            string fullPath = $"{path}{SaveFileName}";
            if (File.Exists(fullPath))
            {
                // ���ϵ� �ִ�.
                string jsonText = File.ReadAllText(fullPath);
                SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonText); // ������ ��ȯ
                rankers = loadedData.rankers;           // RankPanel�� ����
                highRecords = loadedData.highRecords;

                isSuccess = true;
            }
        }

        if (!isSuccess)  // �ε��� ����������
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    // ������ ������ �����.
            }
            SetDefaultData();   // ������ ���ٸ� �⺻������ ����
        }

        RefreshRankLines();
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

    public void Test_Save()
    {
        SaveRankData();

        //// rankers, highRecords
        //string final = "";
        //for (int i = 0; i < MaxRankings; i++)
        //{
        //    final += (rankers[i] + ",");
        //    final += (highRecords[i] + ",");
        //}

        //Debug.Log(final);

        //SaveData data = new SaveData();
        //data.rankers = rankers;
        //data.highRecords = highRecords;

        //string jsonText = JsonUtility.ToJson(data);
        //Debug.Log(jsonText);
    }

    public void Test_Load()
    {
        LoadRankData();

        //string final = "AAA,1000000,BBB,100000,CCC,10000,DDD,1000,EEE,100,";
        //string[] finals = final.Split(',');
        //foreach (string s in finals)
        //{
        //    Debug.Log(s);
        //}

        //string json = "{\"rankers\":[\"AAA\",\"BBB\",\"CCC\",\"DDD\",\"EEE\"],\"highRecords\":[1000000,100000,10000,1000,100]}";
        //SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

        //int i = 0;
    }
#endif
}

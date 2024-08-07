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
    /// 패널에서 보이는 모든 랭크 라인
    /// </summary>
    RankLine[] rankLines;

    /// <summary>
    /// 랭커 이름 입력을 위한 인풋 필드
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// 최고 득점자 이름(1등~5등 순서로 정렬되어 있음)
    /// </summary>
    string[] rankers;

    /// <summary>
    /// 최고 득점(1등~5등 순서로 정렬되어 있음)
    /// </summary>
    int[] highRecords;

    /// <summary>
    /// null이 아니면 직전에 랭킹이 업데이트 된 인덱스
    /// </summary>
    int? updatedIndex = null;

    /// <summary>
    /// 랭킹 표시되는 사람 수
    /// </summary>
    const int MaxRankings = 5;

    /// <summary>
    /// 세이브 파일 이름
    /// </summary>
    const string SaveFileName = "Save.json";

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        rankers = new string[MaxRankings];
        highRecords = new int[MaxRankings];

        inputField = GetComponentInChildren<TMP_InputField>(true);  // 비활성화 되어있는 컴포넌트를 찾으려면 파라메터를 true로 해야 한다.
        inputField.onEndEdit.AddListener(OnNameInputEnd);   // onEndEdit의 발동여부를 확인할 함수 등록(=onEndEdit 이벤트가 발동할 때 실행될 함수 추가)
    }

    private void Start()
    {
        LoadRankData();     // 랭크 정보 불러오기        
        GameManager.Instance.Player.onDie += () => UpdataRankData(GameManager.Instance.Score);  // 플레이어가 죽으면 랭킹 업데이트 시도
    }

    /// <summary>
    /// inputField.onEndEdit 이벤트가 발동되었을 때 실행될 함수
    /// </summary>
    /// <param name="inputText">inputField에 입력이 완료되었을 때의 입력 내용</param>
    private void OnNameInputEnd(string inputText)
    {
        //Debug.Log(inputText);

        inputField.gameObject.SetActive(false);     // 인풋필드 비활성화
        if (updatedIndex != null)
        {
            rankers[updatedIndex.Value] = inputText;    // 이름 변경
            RefreshRankLines(updatedIndex.Value);       // 패널 화면 갱신
            SaveRankData();                             // 랭킹 정보 저장
            updatedIndex = null;
        }
    }

    /// <summary>
    /// 랭킹 데이터를 초기값으로 설정하는 함수
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
            temp = (char)((byte)temp + i);  // i만큼 증가 = A에서 i만큼 떨어진 글자로 변경
            rankers[i] = $"{temp}{temp}{temp}"; // AAA ~ EEE

            highRecords[i] = score;
            score = Mathf.RoundToInt(score * 0.1f); // 한바퀴 돌 때마다 1/10로 줄이기
        }

        RefreshRankLines();
    }

    /// <summary>
    /// 패널 보이는 부분 갱신용 함수
    /// </summary>
    void RefreshRankLines()
    {
        for (int i = 0; i < MaxRankings; i++)
        {
            rankLines[i].SetData(rankers[i], highRecords[i]);   // 저장된 데이터대로 다시 표시
        }
    }

    /// <summary>
    /// 패널에서 특정 라인만 업데이트
    /// </summary>
    /// <param name="index">업데이트할 라인의 인덱스</param>
    void RefreshRankLines(int index)
    {
        rankLines[index].SetData(rankers[index], highRecords[index]);   // 저장된 데이터대로 다시 표시
    }

    /// <summary>
    /// 랭킹 데이터를 업데이트하는 함수
    /// </summary>
    /// <param name="score">새 점수</param>
    void UpdataRankData(int score)
    {
        for (int i = 0; i < MaxRankings; i++)
        {
            if (highRecords[i] < score)
            {
                // 신기록이다.
                for (int j = MaxRankings - 1; j > i; j--)            // 마지막+1에서부터 i전까지 진행
                {
                    rankers[j] = rankers[j - 1];
                    highRecords[j] = highRecords[j - 1];        // 한단계 아래쪽으로 내리기                    
                }

                rankers[i] = "새 랭커";                        // 새 랭커이름과 점수 기록하기
                highRecords[i] = score;
                updatedIndex = i;                               // 업데이트된 인덱스 저장하기

                Vector3 pos = inputField.transform.position;    // 등수에 맞게 위치 이동
                pos.y = rankLines[i].transform.position.y;
                inputField.transform.position = pos;
                inputField.text = string.Empty;                 // 인풋필드 내용 비우기
                inputField.gameObject.SetActive(true);          // 인풋필드 보이게 만들기

                RefreshRankLines(); // 화면 갱신
                break;              // for문 끝내기
            }
        }
    }

    /// <summary>
    /// 랭킹 정보를 파일로 저장하는 함수
    /// </summary>
    void SaveRankData()
    {
        // Assets/Save 폴더에 Save.json이라는 이름으로 저장하기
        SaveData data = new SaveData();             // 직렬화 가능한 클래스 할당
        data.rankers = rankers;                     // 객체에 데이터 넣기
        data.highRecords = highRecords;
        string jsonText = JsonUtility.ToJson(data); // 객체 내용을 json 문자열로 변경

        string path = $"{Application.dataPath}/Save/";  // 폴더 경로 저장해놓기
        if (!Directory.Exists(path))                   // 해당 폴더가 없는지 확인
        {
            Directory.CreateDirectory(path);            // 폴더가 없으면 만든다.
        }

        File.WriteAllText($"{path}{SaveFileName}", jsonText);    // 파일로 저장
    }

    /// <summary>
    /// 랭킹 정보를 파일에서 불러오는 함수
    /// </summary>
    void LoadRankData()
    {
        bool isSuccess = false;

        // Assets/Save 폴더에 있는 Save.json이라는 파일을 읽어서 랭킹 정보 덮어쓰기
        string path = $"{Application.dataPath}/Save/";
        if (Directory.Exists(path))
        {
            // 폴더가 있다
            string fullPath = $"{path}{SaveFileName}";
            if (File.Exists(fullPath))
            {
                // 파일도 있다.
                string jsonText = File.ReadAllText(fullPath);
                SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonText); // 데이터 변환
                rankers = loadedData.rankers;           // RankPanel에 저장
                highRecords = loadedData.highRecords;

                isSuccess = true;
            }
        }

        if (!isSuccess)  // 로딩이 실패했으면
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    // 폴더가 없으면 만든다.
            }
            SetDefaultData();   // 파일이 없다면 기본데이터 설정
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

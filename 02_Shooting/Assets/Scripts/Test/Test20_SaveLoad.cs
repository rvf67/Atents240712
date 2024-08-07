using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
public class Test20_SaveLoad : TestBase
{
    Player player;
    ScoreText scoreText;
    RankPanel rankPanel;
    public int score = 12345;

    public bool isStartDie = false;

#if UNITY_EDITOR
    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreText;

        rankPanel = FindFirstObjectByType<RankPanel>();
        rankPanel.Test_DefaultRankPanel();

        if (!isStartDie)
        {
            player.TestDie();
        }
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        rankPanel.Test_UpdateRankPanel(score);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        rankPanel.Test_Save();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
       rankPanel.Test_Load();
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Debug.Log("���� �Ϸ�");

        // Application.dataPath: �����Ϳ��� �������� ���� Asstrs������ �ǹ�,�����ؼ� �������� ���� "���������̸�_Data"������ �ǹ�
        //System.IO.File.WriteAllText($"{Application.dataPath}/Save/a.txt", "Hello");     //������ ��� ������ �ȵ�

        if (Directory.Exists($"{Application.dataPath}/Save"))
        {
            Debug.Log("Assets �����ȵ� Save ������ �ִ�.");
        }
        else
        {
            Debug.Log("Assets �����ȵ� Save ������ ����.");
            Directory.CreateDirectory($"{Application.dataPath}/Save");
        }

        File.WriteAllText($"{Application.dataPath}/Save/a.txt", "Hello"); //������ �������� ���� ����ϱ� OK
    }
    protected override void OnTest5(InputAction.CallbackContext context)
    {
        Debug.Log("�ҷ�����");
        string result=System.IO.File.ReadAllText("a.txt");
        Debug.Log($"{result}����");
    }
#endif
}

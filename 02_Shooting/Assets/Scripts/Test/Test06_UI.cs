using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_UI : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int score = 100;

        //GameObject obj = GameObject.Find("ScoreText");//����õ:���ڿ��� �˻� ,�̸��� �ߺ��Ǹ� �߸�ã�� �� ����
        //GameObject obj=GameObject.FindGameObjectWithTag("Test");
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Test");
        //GameObject.FindWithTag;

        //������Ʈ Ÿ������ ã��
        ScoreText scoreText= FindObjectOfType<ScoreText>();//�ϳ��� ã��
        ScoreText[] scoreTexts = FindObjectsByType<ScoreText>
            (FindObjectsInactive.Include, FindObjectsSortMode.None);//���� ������Ʈ ������ ã��
        FindAnyObjectByType<ScoreText>();//�ϳ��� ã�� (ù��°���� ����
        FindFirstObjectByType<ScoreText>();//ù��° ã�� (�ӵ��� ���� ,������ �߿�
        
    }
    int Add(int a, int b)
    {
        return a + b;
    }

}

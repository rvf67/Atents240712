using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_UI : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int score = 100;

        // �̸����� ã��
        //GameObject obj = GameObject.Find("ScoreText");  // ����õ : ���ڿ��� �˻�, �̸��� �ߺ��Ǹ� �߸�ã�� �� ����        

        // �±׷� ã��
        //GameObject obj = GameObject.FindGameObjectWithTag("Test");      // ���� �±� �� �ϳ��� ã��
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Test");  // ���� �±� ��� ã��
        // GameObject.FindWithTag;  // ���ο��� FindGameObjectWithTag ȣ��
        //Debug.Log(obj.name);

        // ������Ʈ Ÿ������ ã��(Ư�� ������Ʈ�� ����)
        //ScoreText scoreText = FindObjectOfType<ScoreText>();    // �ϳ��� ã��
        //ScoreText[] scoreTexts = FindObjectsByType<ScoreText>(FindObjectsInactive.Include, FindObjectsSortMode.None);   // ���� ���� ��� ã��
        //FindAnyObjectByType<ScoreText>();       // �ϳ��� ã��(FindObjectOfType���� ����)
        //FindFirstObjectByType<ScoreText>();     // ù��°�� ã��(�ӵ��� ����, ������ �߿��Ҷ� ���)

        ScoreText scoreText = FindAnyObjectByType<ScoreText>();
        scoreText.AddScore(score);
    }
}

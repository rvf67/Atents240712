using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_UI : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int score = 100;

        //GameObject obj = GameObject.Find("ScoreText");//비추천:문자열로 검색 ,이름이 중복되면 잘못찾을 수 있음
        //GameObject obj=GameObject.FindGameObjectWithTag("Test");
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Test");
        //GameObject.FindWithTag;

        //컴포넌트 타입으로 찾음
        ScoreText scoreText= FindObjectOfType<ScoreText>();//하나만 찾기
        ScoreText[] scoreTexts = FindObjectsByType<ScoreText>
            (FindObjectsInactive.Include, FindObjectsSortMode.None);//같은 컴포넌트 여러개 찾기
        FindAnyObjectByType<ScoreText>();//하나만 찾기 (첫번째보나 빠름
        FindFirstObjectByType<ScoreText>();//첫번째 찾기 (속도는 느림 ,순서가 중요
        
    }
    int Add(int a, int b)
    {
        return a + b;
    }

}

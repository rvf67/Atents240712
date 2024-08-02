using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_UI : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int score = 100;

        // 이름으로 찾기
        //GameObject obj = GameObject.Find("ScoreText");  // 비추천 : 문자열로 검색, 이름이 중복되면 잘못찾을 수 있음        

        // 태그로 찾기
        //GameObject obj = GameObject.FindGameObjectWithTag("Test");      // 같은 태그 중 하나만 찾기
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Test");  // 같은 태그 모두 찾기
        // GameObject.FindWithTag;  // 내부에서 FindGameObjectWithTag 호출
        //Debug.Log(obj.name);

        // 컴포넌트 타입으로 찾기(특정 컴포넌트로 리턴)
        //ScoreText scoreText = FindObjectOfType<ScoreText>();    // 하나만 찾기
        //ScoreText[] scoreTexts = FindObjectsByType<ScoreText>(FindObjectsInactive.Include, FindObjectsSortMode.None);   // 같은 종류 모두 찾기
        //FindAnyObjectByType<ScoreText>();       // 하나만 찾기(FindObjectOfType보다 빠름)
        //FindFirstObjectByType<ScoreText>();     // 첫번째것 찾기(속도는 느림, 순서가 중요할때 사용)

        ScoreText scoreText = FindAnyObjectByType<ScoreText>();
        scoreText.AddScore(score);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        int score = 1000;
        ScoreText scoreText = FindAnyObjectByType<ScoreText>();
        scoreText.AddScore(score);
    }
}

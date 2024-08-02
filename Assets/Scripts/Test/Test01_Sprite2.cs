using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test01_Sprite2 : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // base;    // 부모에 접근하기 위한 키워드
        // this;    // 자기 자신에 접근하기 위한 키워드

        //base.OnTest1(context);  // 부모의 기능도 같이 실행하는 경우
        Debug.Log("자식 : OnTest1");
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        int rand = Random.Range(0, 10);
        Debug.Log($"랜덤 : {rand}");
    }

    protected override void OnTestWASD(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Debug.Log($"입력 : {input}");
    }
}

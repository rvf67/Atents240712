using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Instantiate : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int i = Random.Range(0, 10);            // 0~9 중 랜덤한 int를 리턴한다.
        float f = Random.Range(0.0f, 10.0f);    // 0.0 ~ 10.0 중 랜덤한 float을 리턴한다.
        float f2 = Random.value;                // 0.0 ~ 1.0 중 랜덤한 float을 리턴
    }
}

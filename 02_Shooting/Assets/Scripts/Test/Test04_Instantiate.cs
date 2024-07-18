using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Instantiate : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        int i = Random.Range(0, 10);
        float f = Random.Range(0f, 10f);
        float f2 = Random.value; //0~1·£´ý
    }
}

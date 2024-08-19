using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test99_UIUX : TestBase
{
    TestA a;
    TestB b;
    TestA c;

    private void Start()
    {
        a = new TestA();
        b = new TestB();
        c = b;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        a.Run();
        a.RunVirtual();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        b.Run(); b.RunVirtual();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        c.Run(); 
        c.RunVirtual();     // TestB의 RunVirtual 함수가 실행됨
    }
}

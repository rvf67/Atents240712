using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_Singleton : TestBase
{
    private void Start()
    {
        // ��ü ���� ���
        //TestSingleton test = new TestSingleton(); // public�� �����ڰ� ��� new �Ұ���
        TestClass test2 = new TestClass();
        TestClass test3 = new TestClass(10);
        //test2.staticNumber = 10;

        TestClass.staticNumber = 10;
        test2.TestPrint();
        test3.TestPrint();

        //TestSingleton.Instance.test = 20;

    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Debug.Log(SimpleFactory.Instance.gameObject);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        SimpleFactory.Instance.GetBullet();
    }
}

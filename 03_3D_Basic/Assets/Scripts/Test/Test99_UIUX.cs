using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Build;

public class Test99_UIUX : TestBase
{
    public Sprite testSprite;
    public Color color;
    TestA a;
    TestB b;
    TestA c;

    private void Start()
    {
        a = new TestA();
        b = new TestB();
        c = b;

        //Text test1 = GetComponent<Text>();
        //TextMeshProUGUI test2 = GetComponent<TextMeshProUGUI>();
        //test1.text = "Hello";
        //test2.text = "Hello";

        //test1.fontStyle = FontStyle.Bold;
        //test2.fontStyle = FontStyles.Bold;

        //Image test3 = GetComponent<Image>();
        //test3.sprite = Resources.Load<Sprite>("forest");
        //test3.sprite = testSprite;

        //test3.color = new Color(1,1,1,1);   // 흰색
        //new Color(0,0,0,1); // 검정색

        //test3.pixelsPerUnit;
        //test3.type;
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

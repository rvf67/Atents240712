using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test06_Doors : TestBase
{
    // 문 두종류 만들기
    // 1. SlidingDoor - 옆으로 열리는 자동문
    // 2. OneWayDoor - 플레이어가 문앞에 있을 때만 열리는 문(뒤쪽은 열리지 않음, 코드로 구현하기)

    Transform cam1;
    Transform cam2;

    private void Start()
    {
        cam1 = transform.GetChild(0);
        cam2 = transform.GetChild(1);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        //Camera.main;
        Camera.main.transform.position = cam1.position;
        Camera.main.transform.rotation = cam1.rotation;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Camera.main.transform.position = cam2.position;
        Camera.main.transform.rotation = cam2.rotation;
    }
}

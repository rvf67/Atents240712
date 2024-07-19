using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test02_Move : TestBase
{
    public GameObject target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        target.transform.position += new Vector3(0, 1); // 위로 1만큼 이동
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        target.transform.position += new Vector3(0, -1); // 아래로 1만큼 이동
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        target.transform.position += new Vector3(-1, 0); // 왼쪽으로 1만큼 이동
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        target.transform.position += new Vector3(1, 0); // 오른쪽으로 1만큼 이동
    }

    protected override void OnTestWASD(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        target.transform.position += (Vector3)input;
    }
}

// 실습
// 1. PlayerInputActions 완성하기
//      1.1. Move 액션(value) 만들기
//      1.2. Fire 액션(Button) 만들기
// 2. Player 스크립트 완성하기
//      2.1. WASD로 이동하기
//      2.2. Fire 액션이 발동되면 Debug로 "발사"라고 출력하기
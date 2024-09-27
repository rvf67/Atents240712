using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test18_PlayerLife : TestBase
{
    public int number = 0;
    public ImageNumber imageNumber;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        imageNumber.Number = number;
    }
}
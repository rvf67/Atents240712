using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test16_PowerUp : TestBase
{
    public Transform target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetPowerUp(target.position);
    }
}

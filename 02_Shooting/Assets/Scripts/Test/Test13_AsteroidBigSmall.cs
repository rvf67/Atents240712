using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_AsteroidBigSmall : TestBase
{
    public Transform target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetAsteroidBig(target.position, target.position + Vector3.left);
    }
}
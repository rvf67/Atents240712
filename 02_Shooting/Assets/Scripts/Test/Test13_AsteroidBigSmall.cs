using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_AsteroidBigSmall : TestBase
{
    public Transform SpawnPosition;
    public Transform moveTarget;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetAsteroidBig(SpawnPosition.position, moveTarget.position);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetAsteroidSmall(SpawnPosition.position, moveTarget.position);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test13_AsteroidBigSmall : TestBase
{
    public Transform spawnPosition;
    public Transform moveTarget;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetAsteroidBig(spawnPosition.position, moveTarget.position);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        //Factory.Instance.GetAsteroidSmall(spawnPosition.position, moveTarget.position - spawnPosition.position);
        Factory.Instance.GetAsteroidSmall(spawnPosition.position, moveTarget.position);
    }
}

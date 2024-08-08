using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test21_Boss : TestBase
{
    public Transform target;
    public EnemyBoss bossPrefab;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBossBullet(target.position);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBossMissile(target.position);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyBoss(target.position);
    }
}

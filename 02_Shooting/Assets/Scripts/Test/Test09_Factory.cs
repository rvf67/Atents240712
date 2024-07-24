using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test09_Factory : TestBase
{
    public enum ObjectType
    {
        Bullet,
        HitEffect,
        ExplosionEffect,
        Enemy
    }

    public ObjectType spawnType = ObjectType.Bullet;

    Transform target;

    private void Start()
    {
        target = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        switch (spawnType)
        {
            case ObjectType.Bullet:
                Factory.Instance.GetBullet(target.position);
                break;
            case ObjectType.HitEffect:
                Factory.Instance.GetHitEffect(target.position);
                break;
            case ObjectType.ExplosionEffect:
                Factory.Instance.GetExplosion(target.position);
                break;
            case ObjectType.Enemy:
                Factory.Instance.GetEnemy(target.position);
                break;
        }
    }
}

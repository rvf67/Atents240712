using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test08_ObjectPool : TestBase
{
    public enum ObjectType
    {
        Bullet,
        HitEffect,
        ExplosionEffect,
        Enemy
    }

    public BulletPool bulletPool;
    public HitEffectPool hitEffectPool;
    public ExplosionEffectPool explosionEffectPool;
    public OldEnemyPool enemyPool;

    public ObjectType spawnType;


    private void Start()
    {
        bulletPool.Initialize();
        hitEffectPool.Initialize();
        explosionEffectPool.Initialize();
        enemyPool.Initialize();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        StartCoroutine(PoolSpawn());
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        StartCoroutine(InstantiateSpawn());
    }

    IEnumerator PoolSpawn()
    {
        while (true)
        {
            bulletPool.GetObject();
            yield return null;
        }
    }

    IEnumerator InstantiateSpawn()
    {
        while (true)
        {
            SimpleFactory.Instance.GetBullet();
            yield return null;
        }
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        switch (spawnType)
        {
            case ObjectType.Bullet:
                bulletPool.GetObject();
                break;
            case ObjectType.HitEffect:
                hitEffectPool.GetObject();
                break;
            case ObjectType.ExplosionEffect:
                explosionEffectPool.GetObject();
                break;
            case ObjectType.Enemy:
                enemyPool.GetObject();
                break;
        }
    }
}

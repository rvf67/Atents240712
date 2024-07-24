using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test08_ObjectPool : TestBase
{
    public BulletPool bulletPool;

    private void Start()
    {
        bulletPool.Initialized();
    }
}

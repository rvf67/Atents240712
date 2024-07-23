using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFactory:Singleton<SimpleFactory>
{
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject hitEffectPrefab;
    public GameObject explosionPrefab;


    public GameObject GetEnemy(Vector3? position=null,float angle=0.0f)
    {
        return Instantiate(enemyPrefab, position.GetValueOrDefault(), Quaternion.Euler(0,0,angle));
    }

    public GameObject GetBullet(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(bulletPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
    public GameObject GetHitEffect(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(hitEffectPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
    public GameObject GetExplosionEffect(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(explosionPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
}

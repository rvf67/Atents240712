using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// Factory타입의 싱글톤
public class SimpleFactory : Singleton<SimpleFactory>
{
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject hitEffectPrefab;
    public GameObject explosionEffectPrefab;

    //public GameObject GetEnemy()
    //{
    //    return Instantiate(enemyPrefab);
    //}

    public GameObject GetEnemy(Vector3? position = null, float angle = 0.0f)
    {
        return Instantiate(enemyPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
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
        return Instantiate(explosionEffectPrefab, position.GetValueOrDefault(), Quaternion.Euler(0, 0, angle));
    }
}

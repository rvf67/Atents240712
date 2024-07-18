using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnPoint;
    private int poolSize = 100;
    private float coolDown = 2f;
}

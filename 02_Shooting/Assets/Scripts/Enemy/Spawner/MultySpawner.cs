using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultySpawner : MonoBehaviour
{
    public enum SpawnType
    {
        Wave = 0,
        Asteroid
    }

    // 직렬화 : 특정 데이터가 메모리상에 연속적으로 붙게 하는 작업

    [Serializable]  // 아래 클래스를 직렬화해서 저장하겠다는 의미.
                    // 인스펙터 창에서 맴버 변수인 구조체나 클래스 내부를 확인하고 싶을 때 반드시 추가해야 한다.
    public struct SpawnData
    {
        public SpawnType type;
        public float interval;
    }

    /// <summary>
    /// 스폰할 종류의 적과 스폰 간격을 저장해 놓은 배열
    /// </summary>
    public SpawnData[] spawnDatas;

    /// <summary>
    /// 최소 높이
    /// </summary>
    protected const float MinY = -4;

    /// <summary>
    /// 최대 높이
    /// </summary>
    protected const float MaxY = 4;

    /// <summary>
    /// 목적지(목적지가 필요한 적용)
    /// </summary>
    Transform destinationArea;

    private void Awake()
    {
        destinationArea = transform.GetChild(0);
    }

    private void Start()
    {
        foreach (var data in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(data));   // 데이터 별로 코루틴 실행
        }
    }

    /// <summary>
    /// 적을 주기적으로 스폰하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnCoroutine(SpawnData data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.interval);

            Vector3 spawnPosition = GetSpawnPosition();

            switch (data.type)
            {
                case SpawnType.Wave:
                    Factory.Instance.GetEnemyWave(spawnPosition);
                    break;
                case SpawnType.Asteroid:
                    EnemyAsteroidBig big = Factory.Instance.GetAsteroidBig(spawnPosition);
                    big.SetDestination(GetDestination());
                    break;
            }

        }
    }

    /// <summary>
    /// 스폰될 위치를 정해주는 함수
    /// </summary>
    /// <returns>스폰될 위치</returns>
    protected Vector3 GetSpawnPosition()
    {
        Vector3 result = transform.position;
        result.y = Random.Range(MinY, MaxY);
        return result;
    }

    /// <summary>
    /// 목적지 위치를 결정해주는 함수
    /// </summary>
    /// <returns>목적지 위치(월드좌표)</returns>
    Vector3 GetDestination()
    {
        Vector3 pos = destinationArea.position;
        pos.y += Random.Range(MinY, MaxY);

        return pos;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // 출발지점 그리기
        Gizmos.color = Color.green;
        Vector3 p0 = transform.position + Vector3.up * MaxY;
        Vector3 p1 = transform.position + Vector3.up * MinY;
        Gizmos.DrawLine(p0, p1);

        // 목적지점 그리기
        if (destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.yellow;
        p0 = destinationArea.position + Vector3.up * MaxY;
        p1 = destinationArea.position + Vector3.up * MinY;
        Gizmos.DrawLine(p0, p1);
    }

    void OnDrawGizmosSelected()
    {
        // 출발지점 그리기
        Gizmos.color = Color.red;
        Vector3 p0 = transform.position + MaxY * Vector3.up + 0.5f * Vector3.left;
        Vector3 p1 = transform.position + MaxY * Vector3.up + 0.5f * Vector3.right;
        Vector3 p2 = transform.position + MinY * Vector3.up + 0.5f * Vector3.right;
        Vector3 p3 = transform.position + MinY * Vector3.up + 0.5f * Vector3.left;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);

        // 목적지점 그리기
        if (destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.red;
        p0 = destinationArea.position + MaxY * Vector3.up + 0.5f * Vector3.left;
        p1 = destinationArea.position + MaxY * Vector3.up + 0.5f * Vector3.right;
        p2 = destinationArea.position + MinY * Vector3.up + 0.5f * Vector3.right;
        p3 = destinationArea.position + MinY * Vector3.up + 0.5f * Vector3.left;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
#endif
}

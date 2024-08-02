using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : EnemySpawner
{
    Transform destinationArea;

    private void Awake()
    {
        destinationArea = transform.GetChild(0);
    }

    protected override void Spawn()
    {
        OldAsteroid asteroid = Factory.Instance.GetAsteroid(GetSpawnPosition());
        asteroid.SetDestination(GetDestination());
    }

    Vector3 GetDestination()
    {
        Vector3 pos = destinationArea.position;
        pos.y += Random.Range(MinY, MaxY);

        return pos;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if(destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.yellow;
        Vector3 p0 = destinationArea.position + Vector3.up * MaxY;
        Vector3 p1 = destinationArea.position + Vector3.up * MinY;
        Gizmos.DrawLine(p0, p1);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if (destinationArea == null)
            destinationArea = transform.GetChild(0);

        Gizmos.color = Color.red;
        Vector3 p0 = destinationArea.position + MaxY * Vector3.up + 0.5f * Vector3.left;
        Vector3 p1 = destinationArea.position + MaxY * Vector3.up + 0.5f * Vector3.right;
        Vector3 p2 = destinationArea.position + MinY * Vector3.up + 0.5f * Vector3.right;
        Vector3 p3 = destinationArea.position + MinY * Vector3.up + 0.5f * Vector3.left;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    /// <summary>
    /// 총알 발사 주기
    /// </summary>
    public float fireInterval = 1.0f;

    /// <summary>
    /// 총알 발사 위치 및 방향용 트랜스폼
    /// </summary>
    protected Transform firePosition;

    /// <summary>
    /// 포의 트랜스폼
    /// </summary>
    protected Transform gunTransform;
    protected virtual void Awake()
    {
        gunTransform = transform.GetChild(2);
        firePosition=gunTransform.GetChild(2);
    }
    public IEnumerator Fire()
    {
        while (true)
        {
            Factory.Instance.GetBullet(firePosition.position, firePosition.eulerAngles);
            yield return new WaitForSeconds(fireInterval);
        }
    }
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        //앞쪽 방향으로 빨간색 화살표 그리기
        Vector3 p0= transform.position+transform.up *0.01f;
        Vector3 p1 = p0+transform.forward*2.0f;
        Vector3 p2 = p1+Quaternion.AngleAxis(110.0f,transform.up)*transform.forward*0.2f;
        Vector3 p3 = p1+Quaternion.AngleAxis(-110.0f,transform.up)*transform.forward*0.2f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p1, p3);
    }
#endif
}

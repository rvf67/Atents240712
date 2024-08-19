using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.IsolatedStorage;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TurretTrace : TurretBase
{
    //사정거리(sightRange) 안에 플레이어가 들어오면 플레이어 방향으로 회전한다.
    //사정거리 안에 플레이어가 들어오면 계속 총알을 발사한다.

    //시야각 적용해보기
    //시야 가려짐 적용해보기

    /// <summary>
    /// 회전 속도용 모디파이어
    /// </summary>
    public float turnSmooth = 2.0f;
    /// <summary>
    /// 사정거리
    /// </summary>
    public float sightRange = 10.0f;
    /// <summary>
    /// 추적할 플레이어
    /// </summary>
    Transform target;
    /// <summary>
    /// 시야범위 체크용 트리거
    /// </summary>
    SphereCollider sightTrigger;

    IEnumerator fireCorutine;
    private bool isFiring;

    protected override void Awake()
    {
        base.Awake();
        sightTrigger = GetComponent<SphereCollider>();
        sightTrigger.radius = sightRange;
        fireCorutine = Fire();
    }
    private void Update()
    {
        LookTargetANdAttack();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target=other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
    void LookTargetANdAttack()
    {
        if(target != null)
        {
            Vector3 dir = target.position - gunTransform.position; //타겟과 터렛의 위치를 뺀 값을 dir로 갖고
            Quaternion lookRotation = Quaternion.LookRotation(dir.normalized);
            lookRotation.x = 0;
            lookRotation.z = 0;

            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, lookRotation, Time.deltaTime*turnSmooth);
            StartFire();
        }
        else
        {
            StopFire();
        }
    }
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position,transform.up, sightRange);
    }
#endif
    void StartFire()
    {
        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(fireCorutine);
        }
    }
    void StopFire()
    {
        if (isFiring)
        {
            isFiring = false;
            StopCoroutine(fireCorutine);
        }
    }
}

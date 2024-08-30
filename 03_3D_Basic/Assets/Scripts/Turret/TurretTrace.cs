using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TurretTrace : TurretBase
{
    // 사정거리(sightRange) 안에 플레이어가 들어오면 플레이어 방향으로 Gun이 회전한다.
    // 사정거리(sightRange) 안에 플레이어가 들어오면 계속 총알을 발사한다.

    // 시야각 적용해보기
    // 시야 가려짐 적용해보기

    /// <summary>
    /// 사정거리
    /// </summary>
    public float sightRange = 10.0f;

    /// <summary>
    /// 회전 속도용 모디파이어
    /// </summary>
    public float turnSmooth = 2.0f;

    /// <summary>
    /// 터렛이 총알 발사를 시작하는 좌우 발사각(10일 경우 +-10도 씩)
    /// </summary>
    public float fireAngle = 10.0f;

    /// <summary>
    /// 추적할 플레이어
    /// </summary>
    Transform target;

    /// <summary>
    /// 시야범위 체크용 트리거
    /// </summary>
    SphereCollider sightTrigger;

    /// <summary>
    /// 총알 발사 코루틴을 저장해 놓은 변수
    /// </summary>
    IEnumerator fireCoroutine;

    /// <summary>
    /// 현재 발사 중인지를 기록하는 변수
    /// </summary>
    bool isFiring = false;

    /// <summary>
    /// 발사 재시작용 쿨타임
    /// </summary>
    float fireCoolTime = 0.0f;

#if UNITY_EDITOR

    /// <summary>
    /// 발사할 수 있는 상황인지 확인하는 변수
    /// </summary>
    bool isFireReady = false;
#endif

    protected override void Awake()
    {
        base.Awake();

        sightTrigger = GetComponent<SphereCollider>();
        sightTrigger.radius = sightRange;
        fireCoroutine = PeriodFire();
    }

    private void Update()
    {
        fireCoolTime -= Time.deltaTime;
        LookTargetAndAttack();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 시야 범위 그리기
        //Gizmos.DrawWireSphere(transform.spawn, sightRange);
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange, 3.0f);

        // 총구 방향 그리기
        Handles.color = Color.yellow;
        if(gunTransform == null)
            gunTransform = transform.GetChild(2);   // 없으면 찾기
        Vector3 from = transform.position;
        Vector3 to = from + gunTransform.forward * sightRange;
        Handles.DrawDottedLine(from, to, 2.0f);

        // 발사각 그리기

        // 녹색 : 내 시야 범위안에 플레이어가 없는 상태일때
        // 주황색 : 내 시야범위안에 플레이어가 있고 발사를 할 수 없는 상태일 때(시야각 밖이거나 가려지는 물체가 있다)
        // 빨간색 : 내 시야범위안에 플레이어가 있고 발사를 할 수 있는 상태일 때(시야각 안이고 가려지는 물체도 없다)
        if (target == null)
        {
            Handles.color = Color.green;
        }
        else
        {
            if (isFireReady)
            {
                Handles.color = Color.red;
            }
            else
            {
                Handles.color = new Color(1, 0.5f, 0);
            }
        }

        Vector3 dir1 = Quaternion.AngleAxis(-fireAngle, transform.up) * gunTransform.forward;   // gunTransform.forward를 왼쪽으로 fireAngle만큼 회전
        Vector3 dir2 = Quaternion.AngleAxis(fireAngle, transform.up) * gunTransform.forward;    // gunTransform.forward를 오른쪽으로 fireAngle만큼 회전

        to = from + dir1 * sightRange;      // dir1방향으로 sightRange만큼 이동한 위치
        Handles.DrawLine(from, to, 3.0f);   // 가장자리 부분의 선 그리기
        to = from + dir2 * sightRange;      // dir2방향으로 sightRange만큼 이동한 위치
        Handles.DrawLine(from, to, 3.0f);   // 가장자리 부분의 선 그리기
        Handles.DrawWireArc(from, transform.up, dir1, fireAngle * 2, sightRange, 3.0f); // 호 그리기
        //Handles.DrawWireArc(중심점, 위쪽방향, 시작 방향 벡터, 각도, 두깨);
    }
#endif

    /// <summary>
    /// 플레이어 추적 및 발사 처리용 함수
    /// </summary>
    void LookTargetAndAttack()
    {
        bool isStartFire = false;
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position; // 플레이어를 바라보는 방향
            direction.y = 0.0f; // xz평면으로만 회전하게 하기 위해 y는 제거
            //gunTransform.forward = direction; // 즉시 이동

            if(IsTargetVisible(direction))  // 타겟이 보이고 있다.
            {
                // 총의 방향을 플레이어 쪽으로 돌리기
                gunTransform.rotation = Quaternion.Slerp(
                    gunTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSmooth);

                // 발사각 적용하기
                float angle = Vector3.Angle(gunTransform.forward, direction);   // gunTransform.forward와 direction의 사이각 구하기
                if(angle < fireAngle)
                {
                    // 사이각이 fireAngle보다 작으면 발사각 안이다.
                    isStartFire = true;                    
                }   
            }
        }

#if UNITY_EDITOR
        isFireReady = isStartFire;
#endif

        if (isStartFire)
        {
            StartFire();    // 발사 시작
        }
        else
        {
            StopFire();     // 발사 정지
        }
    }

    /// <summary>
    /// 총알 발사 코루틴을 실행시키는 함수
    /// </summary>
    void StartFire()
    {
        if(!isFiring && fireCoolTime < 0.0f)    // 발사 중이 아닐 때만 && 쿨타임이 다 되었을 때
        {
            isFiring = true;
            StartCoroutine(fireCoroutine);      // 발사 코루틴 실행
        }
    }

    /// <summary>
    /// 총알 발사 코루틴을 정지시키는 함수
    /// </summary>
    void StopFire()
    {
        if (isFiring)
        {
            StopCoroutine(fireCoroutine);   // 발사 코루틴 정지
            isFiring = false;
            fireCoolTime = fireInteval;     // 쿨타임 초기화
        }
    }

    /// <summary>
    /// 추적 대상이 보이는지 확인하는 함수
    /// </summary>
    /// <param name="lookDirection">바라보는 방향</param>
    /// <returns>true면 보인다. false면 안보인다.</returns>
    bool IsTargetVisible(Vector3 lookDirection)
    {
        bool result = false;

        Ray ray = new Ray(gunTransform.position, lookDirection);

        // 총알이 레이케스팅 되는 문제 방지
        int mask = int.MaxValue;                        // 1111 1111 1111 1111 1111 1111 1111 1111
        int bulletMask = LayerMask.GetMask("Bullet");   // 0000 0000 0000 0000 0000 0010 0000 0000
        bulletMask = ~bulletMask;                       // 1111 1111 1111 1111 1111 1101 1111 1111
        mask = mask & bulletMask;                       // 1111 1111 1111 1111 1111 1101 1111 1111
                                                        // mask는 총알을 제외한 모든 레이어가 세팅되어 있음.   
                                                        //Physics.Raycast(ray, out RaycastHit hitInfo, sightRange, LayerMask.GetMask("Player", "Default"))

        // out : 출력용 파라메터라고 알려주는 키워드. 함수가 실행되면 자동으로 초기화된다.
        if ( Physics.Raycast(ray, out RaycastHit hitInfo, sightRange, mask) )
        {
            //Debug.Log(hitInfo.transform.gameObject.name);
            // ray에 닿은 오브젝트가 있다.
            if( hitInfo.transform == target )   // 첫번째로 닿은 오브젝트가 target이다.(= 가리는 물체가 없다)
            {
                result = true;
            }
        }
        
        return result;
    }
}

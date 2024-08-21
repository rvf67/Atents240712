using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// 초기 속도
    /// </summary>
    public float initialSpeed = 20.0f;

    /// <summary>
    /// 총알 수명
    /// </summary>
    public float lifeTime = 10.0f;

    /// <summary>
    /// 발사 후 충돌이 있었는지 기록하는 변수
    /// </summary>
    bool isCollide = false;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnReset()
    {
        DisableTimer(lifeTime);     // 수명 설정

        isCollide = false;
        rigid.angularVelocity = Vector3.zero;               // 재활용 할 때 회전력 초기화하기
        rigid.velocity = initialSpeed * transform.forward;  // 앞으로 날아가게 만들기
    }

    private void OnCollisionEnter(Collision collision)
    {
        isCollide = true;
        StopAllCoroutines();    // 부딪치면 이전 코루틴 정지
        DisableTimer(2.0f);     // 새로 2초뒤에 사라지기
    }

    private void FixedUpdate()
    {
        // 벡터의 길이는 Vector3.magnitude로 얻을 수 있다.
        // 다만 연산에 많은 시간이 걸리므로 가능하면 Vector3.sqrMagnitude(root연산 안한 벡터의 크기, 크기의 제곱)를 사용해야 한다


        // 총알이 날아갈 때 앞으로 기울어지게 만들기
        if (!isCollide) // 아직 충돌하지 않았으면 
        {
            //transform.rotation = Quaternion.LookRotation(rigid.velocity); // 아래와 같은 코드
            transform.forward = rigid.velocity; // 움직이는 방향으로 forward 설정
        }
    }
}

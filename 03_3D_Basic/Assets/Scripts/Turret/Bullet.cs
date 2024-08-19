using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// 초기 속도
    /// </summary>
    public float initialSpeed = 20.0f;

    public float rotateSpeed = 3.0f;
    /// <summary>
    /// 총알 수명
    /// </summary>
    public float lifeTime = 10.0f;

    /// <summary>
    /// 발사 후 충돌이 있었는지 기록하는 변수
    /// </summary>
    bool isCrushed= false;

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnReset()
    {
        DisableTimer(lifeTime);     // 수명 설정

        rigid.angularVelocity =Vector3.zero; // 재활용할 때 회전력 초기화하기
        rigid.velocity = initialSpeed * transform.forward;  // 앞으로 날아가게 만들기
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();    // 부딪치면 이전 코루틴 정지
        DisableTimer(2.0f);     // 새로 2초뒤에 사라지기
        isCrushed = true;
    }

    // 총알이 날아갈 때 앞으로 기울어지게 만들기
    private void FixedUpdate()
    {
        //magnitude를 안쓰는 이유는 루트연산이 오래걸리기 때문
        if(!isCrushed&&rigid.velocity.sqrMagnitude > 0.1f) //어느정도 이동하고 있다면
        {
            transform.forward = rigid.velocity;
        }
    }
}

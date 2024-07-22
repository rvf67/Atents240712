using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 총알의 이동속도
    /// </summary>
    public float moveSpeed = 7.0f;

    /// <summary>
    /// 총알의 수명
    /// </summary>
    public float lifeTime = 10.0f;

    /// <summary>
    /// 총알이 맞았을 때 나올 이팩트
    /// </summary>
    public GameObject hitEffect;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // 실습
    // 1. 게임을 시작하면 총알이 로컬의 오른쪽으로 계속 날아가게 만들기
    private void Update()
    {
        // 내 위치에서 초당 moveSpeed 속도로, 내 오른쪽 방향으로 이동하기
        //transform.position += (Time.deltaTime * moveSpeed * transform.right);

        // 초당 moveSpeed 속도로, 월드 기준으로 내 오른쪽 방향으로 이동하기
        //transform.Translate(Time.deltaTime * moveSpeed * transform.right, Space.World);

        // 초당 moveSpeed 속도로, 로컬 기준으로 오른쪽 방향으로 이동하기
        //transform.Translate(Time.deltaTime * moveSpeed * Vector3.right, Space.Self);

        // 초당 moveSpeed 속도로, 로컬 기준으로 오른쪽 방향으로 이동하기
        //transform.Translate(Time.deltaTime * moveSpeed * Vector3.right);

        // 초당 moveSpeed 속도로, 로컬 기준으로 오른쪽 방향으로 이동하기
        transform.Translate(Time.deltaTime * moveSpeed, 0, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌이 시작되었을 때 실행
        Debug.Log("충돌 시작");
        Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);    // 자기자신 제거하기
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // 충돌이 된 상태에서 움직임이 있을 때 실행
    //    //Debug.Log("충돌 중");
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // 충돌이 끝났을 때 실행
    //    Debug.Log("충돌 종료");
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("겹침 시작");
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //      Debug.Log("겹침 중");
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("겹침 종료");
    //}
}

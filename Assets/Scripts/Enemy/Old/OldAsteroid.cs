using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAsteroid : RecycleObject
{
    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 4.0f;

    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 720.0f;

    public AnimationCurve rotateSpeedCurve;

    float moveSpeed;
    float rotateSpeed;

    Vector3 direction;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotateSpeed = minRotateSpeed + rotateSpeedCurve.Evaluate(Random.value) * maxRotateSpeed;

        //Time.timeScale = 0.1f;
    }

    private void Update()
    {
        // Rotate 함수 활용법 : 원래 회전에서 추가로 회전
        //transform.Rotate(0, 0, Time.deltaTime * 360);             // x,y,z를 따로 받기
        //transform.Rotate(Time.deltaTime * 360 * Vector3.forward); // Vector3로 받기
        //transform.Rotate(Vector3.forward, Time.deltaTime * 360);  // 축과 축을 중심으로 얼마나 회전할지를 받기

        // Quaternion 활용법
        //Quaternion.Euler(0, 0, 30); // z축으로 30도만큼 회전
        //transform.rotation *= Quaternion.Euler(0, 0, 30);   // 원래 회전에서 추가로 z축 30도 회전
        //Quaternion.LookRotation(Vector3.forward);   // z축 방향을 바라보는 회전 만들기
        //Quaternion.AngleAxis(angle, axis) // 특정 축을 기준으로 angle만큼 돌아가는 회전 만들기

        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World);
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);        
    }

    public void SetDestination(Vector3 destination)
    {
        direction = (destination - transform.position).normalized;
    }

    // 빙글빙글 돌면서 왼쪽으로 이동시키기

    // Asteroid는 RecycleObject
    // 팩토리에서 생성 가능해야 한다.

    // 운석 스포너 만들기 -> 운석은 랜덤한 목적지로 날아간다.

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// 사인 그래프가 한번 왕복하는데 걸리는 시간 증폭용(커질수록 왕복 속도가 빨라진다)
    /// </summary>
    public float frequency = 2.0f;

    /// <summary>
    /// 사인 그래프의 결과값을 증폭시키는 값(위, 아래 움직이는 정도)
    /// </summary>
    public float amplitude = 3.0f;

    /// <summary>
    /// 시간 누적용 변수
    /// </summary>
    float elapsedTime = 0.0f;

    /// <summary>
    /// 시작 위치 저장용(스폰된 위치)
    /// </summary>
    float spawnY = 0.0f;

    // 실습
    // 1. 계속 월드의 왼쪽으로 움직인다.
    // 2. 위아래로 일정범위를 물결치듯이 움직인다.

    private void Start()
    {
        // Mathf : 유니티 수학용 클래스

        spawnY = transform.position.y;      // 시작 위치 기록하기
    }

    private void Update()
    {
        MoveUpdate(Time.deltaTime);         // 이동 업데이트 처리
    }

    /// <summary>
    /// 이동 처리를 하는 함수
    /// </summary>
    /// <param name="deltaTime">Time.delta</param>
    void MoveUpdate(float deltaTime)
    {
        elapsedTime += deltaTime * frequency;   // frequency만큼 증폭된 시간을 누적

        // 새 위치 지정
        transform.position = new Vector3(
            transform.position.x - deltaTime * moveSpeed,   // 현재 x위치에서 조금 왼쪽
            spawnY + Mathf.Sin(elapsedTime) * amplitude,    // 시작위치에서 sin*amplitude 결과만큼 변동
            0.0f);
    }
}

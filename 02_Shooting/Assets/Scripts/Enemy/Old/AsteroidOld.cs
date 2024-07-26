using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidOld : RecycleObject
{
    public AnimationCurve rotateSpeedCurve;
    public float minMovespeed = 2f;
    public float maxMovespeed = 5f;
    public float minRotateSpeed = 30f;
    public float maxRotateSpeed = 720f;
    
    float moveSpeed;
    float rotateSpeed;

    Vector3 direction;

    private void Start()
    {
        moveSpeed = Random.Range(minMovespeed, maxMovespeed);

        rotateSpeed=minRotateSpeed+rotateSpeedCurve.Evaluate(Random.value)*maxRotateSpeed;
        
    }
    private void Update()
    {
        //Rotate 함수 활용법 :원래 회전에서 추가로 회전
        //transform.Rotate(0,0,Time.deltaTime*360); //x,y,z를 따로받기
        //transform.Rotate(Time.deltaTime *360*Vector3.forward); //벡터3로 받기
        //transform.Rotate(Vector3.forward,Time.deltaTime*360); //축과 축을 중심으로 얼마나 회전할지


        //Quaternion 활용법
        //Quaternion.Euler(0, 0, 30);
        //transform.rotation *= Quaternion.Euler(0, 0, 30);// 원래외전에서 추가로 z축 30도 회전

        //Quaternion.LookRotation(Vector3.forward);//z축 방향을 바라보는 회전 만들기
        //Quaternion.AngleAxis(angle, axis); //특정 축을 기준으로 angle만큼 돌아가는 회전 만들기

        transform.Rotate(0, 0,30*rotateSpeed*Time.deltaTime);
        //transform.position += Vector3.left * Time.deltaTime;
        //빙글빙글 돌면서 왼쪽으로 이동시키기
        transform.Translate(Time.deltaTime * moveSpeed * direction, Space.World);
    }
    public void SetDestination(Vector3 destination)
    {
        direction = (destination-transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }
}

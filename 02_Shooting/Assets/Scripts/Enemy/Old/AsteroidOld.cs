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
        //Rotate �Լ� Ȱ��� :���� ȸ������ �߰��� ȸ��
        //transform.Rotate(0,0,Time.deltaTime*360); //x,y,z�� ���ιޱ�
        //transform.Rotate(Time.deltaTime *360*Vector3.forward); //����3�� �ޱ�
        //transform.Rotate(Vector3.forward,Time.deltaTime*360); //��� ���� �߽����� �󸶳� ȸ������


        //Quaternion Ȱ���
        //Quaternion.Euler(0, 0, 30);
        //transform.rotation *= Quaternion.Euler(0, 0, 30);// ������������ �߰��� z�� 30�� ȸ��

        //Quaternion.LookRotation(Vector3.forward);//z�� ������ �ٶ󺸴� ȸ�� �����
        //Quaternion.AngleAxis(angle, axis); //Ư�� ���� �������� angle��ŭ ���ư��� ȸ�� �����

        transform.Rotate(0, 0,30*rotateSpeed*Time.deltaTime);
        //transform.position += Vector3.left * Time.deltaTime;
        //���ۺ��� ���鼭 �������� �̵���Ű��
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

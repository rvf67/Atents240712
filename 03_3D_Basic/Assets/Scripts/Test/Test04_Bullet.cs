using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_Bullet : TestBase
{
    public GameObject simpleBulletPrefab;

    Transform fire;

    private void Start()
    {
        fire = transform.GetChild(0);
        //Time.timeScale = 0.1f;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Instantiate(simpleBulletPrefab, fire);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetBullet(fire.position, fire.eulerAngles);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Quaternion a = Quaternion.identity; // 아무것도 하지 않는 회전
        Quaternion b = Quaternion.identity;
        a = Quaternion.Euler(90, 0, 0);     // 오일러 각을 이용한 회전 만들기
        a = Quaternion.LookRotation(transform.forward); // 특정 방향을 바라보게 만드는 회전 만들기
        a = Quaternion.FromToRotation(Vector3.forward, Vector3.right);  // from에서 to로 가는 회전만들기
        a = Quaternion.Inverse(a);  // 역회전 만들기
        Quaternion.Angle(a, a); // 두 회전 사이의 각도를 구해주는 함수
        Quaternion.RotateTowards(a, b, 30.0f);  // from에서 to로 회전, 최대 delta 각도 만큼만 회전
        Quaternion.Slerp(a, b, 0.1f);   // from에서 to로 회전. t비율만큼만 회전

        //transform.Rotate()    // 오일러 각만큼 추가회전
        //transform.RotateAround()  // 특정 축 기준으로 회전
        //transform.LookAt()    // 특정 지점을 바라보게 만들기
    }
}

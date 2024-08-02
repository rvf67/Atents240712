using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test17_Enemies : TestBase
{
    // 적 추가
    // 1. EnemyCurve : 커브를 도는 움직임을 한다. 위쪽에 스폰되었으면 좌회전, 아래쪽에 스폰되었으면 우회전
    // 2. EnemyBonus : 죽을 때 파워업 아이템을 생성. 스폰되었을 때 오른쪽 끝에 등장한 후 잠시 대기하고 그 이후에 다시 움직인다.
    // 3. 멀티스포너에 적용하기
    public Transform target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyCurve(target.position);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.GetEnemyBonus(target.position);
    }
}

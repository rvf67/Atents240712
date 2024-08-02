using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurve : EnemyBase
{
    [Header("커브도는 적 데이터")]
    public float rotateSpeed = 10.0f;

    /// <summary>
    /// 회전방향(1이면 반시계방향, -1이면 시계 방향)
    /// </summary>
    float curveDirection = 1.0f;

    protected override void OnMoveUpdate(float deltaTime)
    {
        base.OnMoveUpdate(deltaTime);
        

        //초당, rotateSpeed의 속도로, curveDirection 방향으로 z축 회전
        transform.Rotate(deltaTime*rotateSpeed*curveDirection*Vector3.forward);
    }

    public void UpdateRotateDirection()
    {
        if (transform.position.y < 0)
        {
            //아래쪽이면 우회전
            curveDirection = -1;
        }
        else
        {
            //위쪽이면 좌회전
            curveDirection = 1;
        }
    }
}

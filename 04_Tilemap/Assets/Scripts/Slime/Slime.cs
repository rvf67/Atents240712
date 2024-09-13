using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : RecycleObject
{
    protected override void OnEnable()
    {
        //스폰될때 phase 작동
        base.OnEnable();
    }

    public void Die()
    {
        //죽을 때 dissove작동
        
    }
    
    /// <summary>
    /// 아웃라인을 보여줄지 말지 결정하는 함수
    /// </summary>
    /// <param name="isShow">true면 보여줌true</param>
    public void ShowOutline(bool isShow = true)
    {

    }
}

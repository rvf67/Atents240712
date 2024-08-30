using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Manual : Platform_OneWay, IInteractable
{
    // 플레이어가 플랫폼에 있는 스위치를 작동시키면 반대쪽으로 움직이는 플랫폼
    public bool CanUse => true;

    public void Use()
    {
        //Debug.Log($"사용됨 : {gameObject.name}");
        isPause = false;
    }
}

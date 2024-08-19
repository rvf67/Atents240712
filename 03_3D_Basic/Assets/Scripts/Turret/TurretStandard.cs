using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStandard : TurretBase
{
    //일정 주기별로 총알을 발사
    private void Start()
    {
        StartCoroutine(Fire());
    }
}

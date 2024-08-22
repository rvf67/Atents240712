using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorManualAutoClose : DoorManualStandard
{
    public float autoCloseTime = 3.0f;

    protected override void OnOpen()
    {
        StopAllCoroutines();
        StartCoroutine(AutoClose());    // 열리고 나면 autoCloseTime 이후에 자동으로 닫힘
    }

    IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoCloseTime);
        Close();
    }
}

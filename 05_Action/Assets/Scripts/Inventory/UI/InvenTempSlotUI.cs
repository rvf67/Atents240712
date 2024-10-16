using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenTempSlotUI : SlotUI_Base
{
    private void Update()
    {
        // 항상 마우스 위치에 임시 슬롯을 위치하게 만들기(아이템이 임시 슬롯에 들어왔을 때만 보인다)
        transform.position = Mouse.current.position.ReadValue();
    }
}

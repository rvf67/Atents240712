using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InvenSlotUI : SlotUI_Base
{
    TextMeshProUGUI equipText;

    protected override void Awake()
    {
        base.Awake();
        Transform child = transform.GetChild(2);
        equipText=child.GetComponent<TextMeshProUGUI>();
    }
    protected override void OnRefresh()
    {
        base.OnRefresh();
        if(InvenSlot.IsEquipped)
        {
            equipText.color =Color.red;     //장비했을 때는 빨간색
        }
        else
        {
            equipText.color = Color.clear;  //장비하지 않았을 때는 투명색
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 아이템용 ItemData
/// </summary>
[CreateAssetMenu(fileName = "New Item Data - Weapon", menuName = "Scripable Objects/Item Data - Weapon", order = 6)]
public class ItemData_Weapon : ItemData_Equip
{
    [Header("무기 데이터")]
    /// <summary>
    /// 무기의 공격력
    /// </summary>
    public float attackPower = 30.0f;
}

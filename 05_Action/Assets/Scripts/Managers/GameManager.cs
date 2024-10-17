using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemDataManager))]
public class GameManager : Singleton<GameManager>
{
    InventoryUI inventoryUI;

    Player player;
    public Player Player => player;

    ItemDataManager itemDataManager;

    /// <summary>
    /// ItemDataManager. 인덱서로 개별 아이템 데이터에 접근 가능
    /// </summary>
    public ItemDataManager ItemData => itemDataManager;

    /// <summary>
    /// 인벤토리 UI 접근용 프로퍼티(Get만 가능)
    /// </summary>
    public InventoryUI InventoryUI => inventoryUI;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindAnyObjectByType<Player>();
        inventoryUI = FindAnyObjectByType<InventoryUI>();

        player?.Initialize();
    }
}

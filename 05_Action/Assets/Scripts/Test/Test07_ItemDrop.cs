using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test07_ItemDrop : TestBase
{
    public ItemCode code = ItemCode.Misc;
    public uint spawnCount = 1;
    public Transform spawnPosition;
    public bool noise = false;

    Player player;


#if UNITY_EDITOR    
    private void Start()
    {
        spawnPosition = transform.GetChild(0);

        player = GameManager.Instance.Player;
        player.Inventory.AddItem(ItemCode.Ruby);
        player.Inventory.AddItem(ItemCode.Ruby);
        player.Inventory.AddItem(ItemCode.Sapphire);
        player.Inventory.AddItem(ItemCode.Sapphire);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.AddItem(ItemCode.Emerald);
        player.Inventory.MoveItem(2, 3);
        player.Inventory.AddItem(ItemCode.Sapphire, 2);
        player.Inventory.AddItem(ItemCode.Sapphire, 2);
        player.Inventory.AddItem(ItemCode.Sapphire, 5);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.MakeItems(code, spawnCount, spawnPosition.position, noise);
    }
#endif
}

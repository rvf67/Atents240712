using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    Inventory inventory;
    public Inventory Inventory => inventory;

    public void Initialize()
    {
        inventory = new Inventory(this);
        GameManager.Instance.InventoryUI.InitializeInventory(inventory);
    }
}

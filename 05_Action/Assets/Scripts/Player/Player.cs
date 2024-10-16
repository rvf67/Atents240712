using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttack),typeof(PlayerInventory))]
public class Player : MonoBehaviour ,IInitializable
{
    PlayerInventory inventory;
    public Inventory Inventory => inventory.Inventory;
    // 컴포넌트 들
    CharacterController characterController;

    PlayerInputController inputController;
    PlayerMovement movement;
    PlayerAttack attack;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputController = GetComponent<PlayerInputController>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();

        inputController.onMove += movement.SetDirection;
        inputController.onMoveModeChange += movement.ToggleMoveMode;
        inputController.onAttack += attack.OnAttackInput;

    }
    public void Initialize()
    {
        IInitializable[] inits = GetComponents<IInitializable>();
        foreach(var init in inits)
        {
            init.Initialize();
        }
    }
}

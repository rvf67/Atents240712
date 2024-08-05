using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test18_PlayerLife : TestBase
{
    public Transform powerUpPosition;

    public PowerUp[] powerUps;

    Player player;

    private void Start()
    {
        for (int i = 0; i < powerUps.Length; i++)
        {
            powerUps[i].transform.position = powerUpPosition.position + Vector3.right * i * 2;
        }

        player = GameManager.Instance.Player;
    }

#if UNITY_EDITOR
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.TestLifeUp();
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        player.TestLifeDown();
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        player.TestDie();
    }
#endif
}

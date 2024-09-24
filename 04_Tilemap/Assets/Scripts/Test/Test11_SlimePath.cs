using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Test11_SlimePath : TestBase
{
    public Tilemap background;
    public Tilemap obstacle;
    public Slime slime;
    public Slime blockSlime;
    TileGridMap map;
    private void Start()
    {
        map = new TileGridMap(background,obstacle);
        slime.Initialize(map, slime.transform.position);
        blockSlime.Initialize(map, blockSlime.transform.position);
        slime.ShowPath();
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        //클릭위치로 이동
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        slime.SetDestination(world);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        //랜덤위치로 이동
        slime.SetDestination(map.GetRandomMovablePostion());
    }
}

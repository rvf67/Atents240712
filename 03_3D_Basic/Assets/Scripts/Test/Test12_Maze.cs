using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test12_Maze : TestBase
{
    //윌슨 알고리즘(미로생성 알고리즘 중 하나)
    //1. 필드의 한곳을 랜덤으로 미로에 추가한다.
    //2. 미로에 포함되지 않은 필드의 한곳을 랜덤으로 결정한다.(A셀0
    //3. A셀 위치에서 랜덤으로 한칸 움직인다. (B셀)
    //3-1. 움직일 때는 어느 셀에서 어느 셀로 움직였는지를 기록한다.
    //3.3 한칸 움직인 곳이 미로에 포함된 셀이 아니면 3번 항목을 반복한다.
    //4. B셀이 포함된 셀에 도착하면 B셀 시작에서 현재 셀까지의 경로를 미로에 포함시킨다.
    //4.1 B셀에서 움직이다가 경로에 닿았을 경우 이전 경호는 무시한다.
    //5. 모든 셀이 미로에 포함될 때까지 2번 항목을 반복한다.
    public PathDirection pathDir;
    public CornerMask cornerMask;
    public CellVisualizer cell;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        PathDirection dir = PathDirection.North | PathDirection.West;
        Debug.Log(dir);

        TestDirection dir2 = TestDirection.North | TestDirection.West;
        Debug.Log(dir2);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        cell.RefreshWall(pathDir);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        cell.RefreshCorner(cornerMask);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        CellBase test = new CellBase(0, 0);
        test.MakePath(pathDir);
        Debug.Log(test.Path);
        //test.x = 10;
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        int[] Array = new int[10];
        int[,] int2dArray = new int[3, 4]; //가로로 4 세로로 3
        //2차원 배열은 C#에서 사용하면 성능이 떨어짐 , 데이터에 접근할 때 느림,배열의 장점이 사라짐
    }
}

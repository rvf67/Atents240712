using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Test10_AStar_TileGridmap : TestBase
{
    public Tilemap background;
    public Tilemap obstacle;
    public PathLine pathLine;

    public Vector2Int start;
    public Vector2Int end;

    TileGridMap tileGridMap;

    private void Start()
    {
        tileGridMap = new TileGridMap(background, obstacle);
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        Node node = tileGridMap.GetNode(world);
        //Debug.Log($"({node.X}, {node.Y}) : {tileGridMap.Test_CalcIndex(node.X, node.Y)}");  // 0,0이 190

        if( !tileGridMap.IsWall(node.X, node.Y) )
        {
            start = tileGridMap.WorldToGrid(world);
            Debug.Log($"Start : {start}" );

            List<Vector2Int> path = AStar.PathFind(tileGridMap, start, end);
            PrintList(path);
            pathLine.DrawPath(tileGridMap, path);
        }
    }

    protected override void OnTestRClick(InputAction.CallbackContext context)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(screen);

        Node node = tileGridMap.GetNode(world);

        if (!tileGridMap.IsWall(node.X, node.Y))
        {
            end = tileGridMap.WorldToGrid(world);
            Debug.Log($"End : {end}");

            List<Vector2Int> path = AStar.PathFind(tileGridMap, start, end);
            PrintList(path);
            pathLine.DrawPath(tileGridMap, path);
        }
    }

    /// <summary>
    /// 경로 출력용 테스트 함수
    /// </summary>
    /// <param name="list">전체 경로</param>
    void PrintList(List<Vector2Int> list)
    {
        if (list != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Vector2Int p in list)
            {
                sb.Append($"{tileGridMap.GridToIndex(p)} -> ");
            }
            sb.Append("End");
            Debug.Log(sb.ToString());
        }
    }
}

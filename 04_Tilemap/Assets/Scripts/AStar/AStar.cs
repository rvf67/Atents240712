using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    /// <summary>
    /// 옆으로 이동하는거리
    /// </summary>
    const float sideDistance = 1.0f;

    /// <summary>
    /// 대각선으로 이동하는 거리
    /// </summary>
    const float diagonalDistance = 1.414213f;

    /// <summary>
    /// 경로를 찾아주는 함수
    /// </summary>
    /// <param name="map">경로를 찾을 맵</param>
    /// <param name="start">시작 위치</param>
    /// <param name="end">도착위치</param>
    /// <returns>시작 위치에서 도착 위치까지의 경로(길을 못찾으면 null)</returns>
    public static List<Vector2Int> PathFind(GridMap map, Vector2Int start, Vector2Int end)
    {
        Node current = map.GetNode(start);
        Node compare = null;
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();
        closeList.Add(current);
        while (true)
        {
            for (int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    if(map.IsValidPosition(current.X+j, current.Y + i)&&map.IsPlain(current.X + j, current.Y + i))
                    {
                        compare = map.GetNode(current.X + j, current.Y + i);
                        if(i != 0 ||  j != 0)
                        {
                            openList.Add(compare);
                        }
                        compare.H = GetHeuristic(compare,end); 
                    }
                }
            }
            openList.Sort();
            break;
        }
        List<Vector2Int> path = null;
        return path;
    }


    /// <summary>
    /// A* 알고리즘의 휴리스틱 값을 계산하는 함수 (현재 위치에서 목적지까지의 예상거리)
    /// </summary>
    /// <param name="current">현재노드</param>
    /// <param name="end">목적지</param>
    /// <returns>예상거리</returns>
    private static float GetHeuristic(Node current,Vector2Int end)
    {
        return sideDistance*Mathf.Abs(current.X-end.x)+ sideDistance * Mathf.Abs(current.Y - end.y);
    }
}

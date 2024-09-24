using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapArea : MonoBehaviour
{
    /// <summary>
    /// 맵의 배경용 타일맵(전체크기)
    /// </summary>
    Tilemap background;
    /// <summary>
    /// 맵의 벽용 타일맵 (못가는 지역 결정)
    /// </summary>
    Tilemap obstacle;

    /// <summary>
    /// 타일맵으로 생성한 그리드맵 (A*용)
    /// </summary>
    TileGridMap gridMap;

    /// <summary>
    /// 그리드맵 확인용 프로퍼티
    /// </summary>
    public TileGridMap GridMap=> gridMap;

    private void Awake()
    {
        Transform parent = transform.parent;
        Transform sibiling = parent.GetChild(0);
        background = sibiling.GetComponent<Tilemap>();
        sibiling=parent.GetChild(1);
        obstacle = sibiling.GetComponent<Tilemap>();

        gridMap = new TileGridMap(background, obstacle);
    }

    /// <summary>
    /// 스폰 가능한 영역을 미리 찾는데 사용하는 함수
    /// </summary>
    /// <param name="position">피봇 위치</param>
    /// <param name="size">영역 크기</param>
    /// <returns>영역안에 plain인 모든 노드</returns>
    public List<Node> CalcSpawnArea(Vector3 position,Vector3 size)
    {
        List<Node> result = new List<Node>();

        Vector2Int min = gridMap.WorldToGrid(position);
        Vector2Int max = gridMap.WorldToGrid(position+size);


        for(int y= min.y; y < max.y; y++)
        {
            for(int x= min.x; x < max.x; x++)
            {
                if (gridMap.IsPlain(x, y))
                {
                    result.Add(gridMap.GetNode(x, y));
                }
            }
        }


        return result;
    }

    /// <summary>
    /// 맵의 그리드 좌표를 월드 좌표로 변경해주는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector2 GridToWorld(int x,int y)
    {
        return gridMap.WorldToGrid(new(x,y));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeVisualizer : MonoBehaviour
{
    /// <summary>
    /// 셀의 프리팹
    /// </summary>
    public GameObject cellPrefab;

    /// <summary>
    /// 마지막으로 그린 미로
    /// </summary>
    MazeBase maze = null;

    /// <summary>
    /// 코너 방향 세트를 저장해 놓은 배열(북서, 북동, 남동, 남서 순서)
    /// </summary>
    (PathDirection, PathDirection)[] corners = null;

    /// <summary>
    /// 이웃의 방향을 저장해 놓은 딕셔너리
    /// </summary>
    Dictionary<PathDirection, Vector2Int> neighborDir;

    private void Awake()
    {
        corners = new (PathDirection, PathDirection)[]
            {
                (PathDirection.North, PathDirection.West),
                (PathDirection.North, PathDirection.East),
                (PathDirection.South, PathDirection.East),
                (PathDirection.South, PathDirection.West)
            };

        neighborDir = new Dictionary<PathDirection, Vector2Int>(4);
        neighborDir[PathDirection.North] = new Vector2Int(0, -1);
        neighborDir[PathDirection.East] = new Vector2Int(1, 0);
        neighborDir[PathDirection.South] = new Vector2Int(0, 1);
        neighborDir[PathDirection.West] = new Vector2Int(-1, 0);
    }

    /// <summary>
    /// 파라메터로 받은 미로를 그리는 함수
    /// </summary>
    /// <param name="maze"></param>
    public void Draw(MazeBase maze)
    {
        this.maze = maze;                       //미로기록
        float size = CellVisualizer.CellSize;   //셀의 길이 기록

        foreach (var cell in maze.Cells)        //미로의 모든 셀에 대해 처리
        {
            GameObject obj = Instantiate(cellPrefab, transform);        //셀 생성
            obj.transform.Translate(cell.X * size, 0, -cell.Y * size);  //셀 위치 옮기기
            obj.gameObject.name = $"Cell_({cell.X}, {cell.Y})";         //셀 게임오브젝트의 이름 변경

            CellVisualizer cellVisualizer = obj.GetComponent<CellVisualizer>();
            cellVisualizer.RefreshWall(cell.Path);                      //길 데이터에 따라 벽 제거

            // 코너 : 내 모서리쪽 이웃으로 길이 있고, 이웃은 내 모서리쪽에 벽이 있다.
            CornerMask cornerMask = 0;
            for (int i = 0; i < corners.Length; i++)
            {
                if (IsConerVisible(cell, corners[i].Item1, corners[i].Item2))   //보여야 할 코너인지 확인
                {
                    cornerMask |= (CornerMask)(1 << i);                         //보여야 된다면 flag 설정
                }
            }
            cellVisualizer.RefreshCorner(cornerMask);                           //설정된 플래그에 따라 on/off
        }
    }

    /// <summary>
    /// 그린 미로를 삭제하는 함수
    /// </summary>
    public void Clear()
    {
        while(transform.childCount > 0)     //자식이 남아있는 한 반복
        {
            Transform child = transform.GetChild(0);    //첫번째 자식을 선택해서
            child.SetParent(null);                      //부모 제거하고
            Destroy(child.gameObject);                  //삭제하기
        }
    }

    bool IsConerVisible(CellBase cell, PathDirection dir1, PathDirection dir2)
    {
        // 코너 : 내 모서리쪽 이웃으로 길이 있고, 이웃은 내 모서리쪽에 벽이 있다.
        if (cell.CornerPathCheck(dir1, dir2))
        {
            CellBase neighborCell1= maze.GetCell(cell.X + neighborDir[dir1].x, cell.Y + neighborDir[dir1].y);
            CellBase neighborCell2= maze.GetCell(cell.X + neighborDir[dir2].x, cell.Y + neighborDir[dir2].y);
            if (neighborCell1.IsWall(dir2)&&neighborCell2.IsWall(dir1))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 그리드 좌표로 셀의 로컬 구하는 함수
    /// </summary>
    /// <param name="x">확인할 x위치</param>
    /// <param name="y">확인할 Y 위치</param>
    /// <returns>로컬 좌표</returns>
    public static Vector3 Grid2Local(int x, int y)
    {
        float size = CellVisualizer.CellSize;
        float sizeHalf = size * 0.5f;

        return new(size*x+sizeHalf,0,size*y-sizeHalf);
    }

    /// <summary>
    /// 로컬좌표를 그리드 좌표로 변경하는 함수
    /// </summary>
    /// <param name="local">변경할 로컬 좌표</param>
    /// <returns>로컬 좌표</returns>
    public static Vector2Int Local2Grid(Vector3 local)
    {
        float size = CellVisualizer.CellSize;
        return new Vector2Int((int)(local.x/size), (int)(-local.z/size));
    }
}

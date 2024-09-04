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
        this.maze = maze;
        float size = CellVisualizer.CellSize;

        foreach (var cell in maze.Cells)
        {
            GameObject obj = Instantiate(cellPrefab, transform);
            obj.transform.Translate(cell.X * size, 0, -cell.Y * size);
            obj.gameObject.name = $"Cell_({cell.X}, {cell.Y})";

            CellVisualizer cellVisualizer = obj.GetComponent<CellVisualizer>();
            cellVisualizer.RefreshWall(cell.Path);

            // 코너 : 내 모서리쪽 이웃으로 길이 있고, 이웃은 내 모서리쪽에 벽이 있다.
            CornerMask cornerMask = 0;
            for (int i = 0; i < corners.Length; i++)
            {
                if (IsConerVisible(cell, corners[i].Item1, corners[i].Item2))
                {
                    cornerMask |= (CornerMask)(1 << i);
                }
            }
            cellVisualizer.RefreshCorner(cornerMask);
        }
    }

    /// <summary>
    /// 그린 미로를 삭제하는 함수
    /// </summary>
    public void Clear()
    {

    }

    bool IsConerVisible(CellBase cell, PathDirection dir1, PathDirection dir2)
    {
        // 코너 : 내 모서리쪽 이웃으로 길이 있고, 이웃은 내 모서리쪽에 벽이 있다.
        return false;
    }
}

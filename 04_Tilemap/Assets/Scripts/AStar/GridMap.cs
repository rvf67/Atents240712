using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap 
{
    /// <summary>
    /// 이 맵이 가지는 모든 노드
    /// </summary>
    protected Node[] nodes;

    /// <summary>
    /// 이맵의 가로길이
    /// </summary>
    protected int width;
    /// <summary>
    /// 이맵의 세로길이
    /// </summary>
    protected int height;

    public GridMap(int width, int height)
    {
        this.width = width;
        this.height = height;

        nodes = new Node[width*height];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                nodes[y * width + x] = new Node(x, y);
            }
        }
    }


    public void ClearMapData()
    {
        foreach(Node node in nodes)
        {
            node.ClearData();
        }
    }
    /// <summary>
    /// 그리드 좌표를 인덱스 값으로 변경해주는 함수
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <param name="index">변경된 인덱스 값(출력용)</param>
    /// <returns>변경이 성공했으면 true,아니면 false</returns>
    protected bool GridToIndex(int x, int y, out int? index)
    {
        bool result = false;
        index = null;           //IsValidPosition가 false일 때를 대비해서 값 설정
        if (IsValidPosition(x, y))
        {
            index = CalcIndex(x,y);
            result = true;
        }
        return result;
    }

    /// <summary>
    /// index값을 좌표로 변경해주는 함수
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector2Int IndexToGrid(int index)
    {
        return new(index % width, index / width);
    }

    /// <summary>
    /// 특정위치에 있는 노드를 리턴하는 함수
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns>찾은 노드</returns>
    public Node GetNode(int x, int y)
    {
        Node result = null;
        if(GridToIndex(x,y,out int? index))
        {
            result = nodes[index.Value];
        }
        return result;
    }

    /// <summary>
    /// 특정 위치에 있는 노드를 리턴하는 함수
    /// </summary>
    /// <param name="grid">좌표</param>
    /// <returns>찾은 노드 (잘못된 좌표면 null)</returns>
    public Node GetNode(Vector2Int grid)
    {
        return GetNode(grid.x, grid.y);
    }
    /// <summary>
    /// 특정위치가 평지인지 확인하는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsPlain(int x , int y)
    {
        Node node = GetNode(x, y);
        return node != null&& node.nodeType == Node.NodeType.Plain; 
    }
    public bool IsPlain(Vector2Int grid)
    {
        return IsPlain(grid.x,grid.y);
    }
    /// <summary>
    /// 특정위치가 평지인지 확인하는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsWall(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.nodeType == Node.NodeType.Plain;
    }
    public bool IsWall(Vector2Int grid)
    {
        return IsWall(grid.x, grid.y);
    }

    /// <summary>
    /// 특정위치가 평지인지 확인하는 함수
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsSlime(int x, int y)
    {
        Node node = GetNode(x, y);
        return node != null && node.nodeType == Node.NodeType.Plain;
    }
    public bool IsSlime(Vector2Int grid)
    {
        return IsSlime(grid.x, grid.y);
    }
    /// <summary>
    /// 간단하게 인덱스를 계산하는 함수
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns>계산된 인덱스</returns>
    private int? CalcIndex(int x, int y)
    {
        return x+y*width;
    }

    /// <summary>
    /// 맵안인지 확인하는 함수
    /// </summary>
    /// <param name="x">x좌표</param>
    /// <param name="y">y좌표</param>
    /// <returns>true면 맵안, false면 맵 밖</returns>
    public virtual bool IsValidPosition(int x, int y)
    {
        return x < width && y < height && x > -1 && y > -1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVisualizer : MonoBehaviour
{
    /// <summary>
    /// 셀 한변의 길이를 나타내는 상수
    /// </summary>
    public const float CellSize = 10.0f;

    /// <summary>
    /// 벽 게임 오브젝트의 배열
    /// </summary>
    GameObject[] walls;

    /// <summary>
    /// 코너 게임 오브젝트의 배열
    /// </summary>
    GameObject[] corners;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        walls = new GameObject[child.childCount];
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = child.GetChild(i).gameObject;
        }
        corners = new GameObject[child.childCount];
        for (int i = 0; i < corners.Length; i++)
        {
            corners[i] = child.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// 입력받은 데이터에 맞게 벽의 활성화 여부 재설정
    /// </summary>
    /// <param name="flagData">벽의 on/off를 표시하는 비트마스크</param>
    public void RefreshWall(PathDirection flagData)
    {
        //data :북동남서 순서대로 1이 세팅되어 있으면 길(=벽이없음) , 0이 세팅되어 있으면 벽
        //0000~1111
        //0001: 북쪽에만 길이있고 동남서는 벽이다.

        byte data = (byte)flagData;

        for (int i = 0; walls.Length > i; i++)
        {
            int mask = 1 << i;
            
            walls[i].SetActive((data & mask) == 0); // & 결과가 0이면 mask에 세팅된 비트자리가 
        }

    }

    /// <summary>
    /// 입력받은 데이터에 맞게 코너의 활성화 여부 재설정
    /// </summary>
    /// <param name="flagData">코너의 on/off를 표시하는 비트마스크</param>
    public void RefreshCorner(CornerMask flagData)
    {
        int data = (int)flagData;

        for (int i = 0; i < walls.Length; i++)
        {
            int mask = 1 << i;      // 0001, 0010, 0100, 1000
            corners[i].SetActive((data & mask) != 0); // & 결과가 0이면 mask에 세팅된 비트자리가 0이었다는 소리이므로 z코너가 없어야 한다.
        }
    }
    public PathDirection GetPath()
    {
        int mask = 0;
        for(int i=0; walls.Length > i++;)
        {
            if (!walls[i].activeSelf)
            {
                mask |= 1 << i;
            }
        }
        return (PathDirection)mask;
    }
}

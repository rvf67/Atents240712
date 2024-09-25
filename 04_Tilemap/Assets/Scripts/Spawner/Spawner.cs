using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //정해진 범위 내에서 일정 시간 간격으로 한마리씩 스폰된다.
    //정해진 최대치까지만 스폰된다.
    
    /// <summary>
    /// 생성된 슬라임수
    /// </summary>
    int slimeCount =0;
    /// <summary>
    /// 스포너에서 최대로 유지 가능한 슬라임의 수
    /// </summary>
    public int capacity = 3;
    /// <summary>
    /// 마지막 스폰에서 진행된 시간
    /// </summary>
    float elapsedTime = 0.0f;
    
    public float interval = 1.0f;
    /// <summary>
    /// 스폰영역에서 plain인 노드의 목록
    /// </summary>
    List<Node> spawnAreaList;
    /// <summary>
    /// 스폰영역의 크기(피봇은 transform.position ,x:오른쪽 y:위쪽)
    /// </summary>
    public Vector2 size;
    /// <summary>
    /// 그리드맵, 타일맵 관리하는 객체
    /// </summary>
    MapArea mapArea;
    private void Start()
    {
        mapArea = GetComponentInParent<MapArea>();
        spawnAreaList = mapArea.CalcSpawnArea(transform.position,size);
        //StartCoroutine("SlimeSpawn");
    }


    private void Update()
    {
        
        if (slimeCount < capacity)
        {
            elapsedTime += Time.deltaTime; //시간 누적(캐퍼시티가 남아있을 때만 증가=> 캐퍼시티가 가득차있다가 남는 공간이 생기면
            if(elapsedTime > interval) //인터벌 확인
            {
                Spawn();
                elapsedTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// 슬라임 한마리만 스폰하는 함수
    /// </summary>
    private void Spawn()
    {
        if (IsSpawnAvailable(out Vector3 spawnablePosition))
        {
            Slime slime = Factory.Instance.GetSlime(spawnablePosition);
            slime.Initialize(mapArea.GridMap,spawnablePosition);
            slime.onDie += () =>
            {
                slimeCount--;
            };
            slimeCount++;
            
            //capacity 이하로 내려가면 다시 스폰
            //슬라임이 스폰될때 부모가 스포너
            //슬라임이 디스폰될때 다시풀을 슬리임의 부모
        }
    }

    /// <summary>
    /// 스폰가능한 지역이 있는지 확인하고 리턴해주는 함수
    /// </summary>
    /// <param name="spawnablePosition">스폰 가능한 위치(월드좌표, 출력용)</param>
    /// <returns>스폰 가능한 지역이 있으면 true</returns>
    bool IsSpawnAvailable(out Vector3 spawnablePosition)
    {
        bool result = false;
        List<Node> positions = new List<Node>();
        foreach (Node node in spawnAreaList)
        {
            if(node.nodeType == Node.NodeType.Plain)
            {
                positions.Add(node);
            }
        }

        if (positions.Count > 0)
        {
            int index = Random.Range(0, positions.Count);
            
            Node target = positions[index];
            spawnablePosition = mapArea.GridToWorld(target.X, target.Y);

            result =true;
        }
        else
        {
            spawnablePosition =Vector3.zero;        //out 파라미터에 무조건 설정이 필요해서 추가
        }
        return result;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 p0 = new(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
        Vector3 p1 = p0+Vector3.right*size.x;
        Vector3 p2 = p0+(Vector3)size;
        Vector3 p3 = p0+Vector3.up*size.y;

        Handles.color = Color.white;
        Handles.DrawLine(p0, p1,5);
        Handles.DrawLine(p1, p2,5);
        Handles.DrawLine(p2, p3,5);
        Handles.DrawLine(p3, p0,5);
    }
#endif
}

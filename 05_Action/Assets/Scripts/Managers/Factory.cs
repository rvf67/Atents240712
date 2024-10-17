using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    /// <summary>
    /// 생성할 때 노이즈 반지름
    /// </summary>
    public float spawnNoise = 0.5f;

    ItemDataManager itemDataManager;

    
    ItemPool itemPool;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }
    protected override void OnInitialize()
    {
        Transform child = transform.GetChild(0);
        itemPool = child.GetComponent<ItemPool>();
        itemPool?.Initialize();
    }

 

    /// <summary>
    /// 아이템을 하나 생성하는 함수(위치 ,, 노이즈 설정 가능)
    /// </summary>
    /// <param name="code">아이템 코드</param>
    /// <param name="position">생성위치</param>
    /// <param name="useNoise">노이즈 사용여부</param>
    /// <returns>아이템의 게임 오브젝트</returns>
    public GameObject MakeItem(ItemCode code, Vector3? position = null, bool useNoise = false)
    {
        ItemData data = itemDataManager[code]; //아이템 데이터 매니저에서 데이터를 하나 꺼냄
        ItemObject obj = itemPool.GetObject();
        obj.transform.position = position!= null ? position.Value:Vector3.zero;
        if(useNoise)
        {
            Vector3 noise = Vector3.zero;
            Vector2 rand = Random.insideUnitCircle * spawnNoise;

            noise.x = rand.x;
            noise.y = rand.y;

            obj.transform.position += noise;
        }


        return obj.gameObject;
    }


    /// <summary>
    /// 아이템을 여러개 생성하는 함수(위치 ,, 노이즈 설정 가능)
    /// </summary>
    /// <param name="code">아이템 코드</param>
    /// <param name="count">아이템의 개수</param>
    /// <param name="position">생성위치</param>
    /// <param name="useNoise">노이즈 사용여부</para
    public GameObject[] MakeItems(ItemCode code, uint count, Vector3? position=null,bool useNoise = false)
    {
        GameObject[] items = new GameObject[count];
        for (int i = 0; i < count; i++)     //count만큼 반복해서 MakeItem 반복중
        {
            items[i] = MakeItem(code,position,useNoise);
        }
        return items;
    }
}

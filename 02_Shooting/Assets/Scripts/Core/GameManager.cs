using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Player Player
    {
        get
        {
            if (player == null)
            {
                OnInitialize(); //oninitialize전에 호풍괴면 일단 초기화먼저 처리
            }
            return player;
        }
    }
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }
}

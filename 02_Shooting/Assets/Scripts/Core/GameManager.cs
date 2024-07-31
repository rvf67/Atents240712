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
                OnInitialize(); //oninitialize���� ȣǳ���� �ϴ� �ʱ�ȭ���� ó��
            }
            return player;
        }
    }
    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player => player;
        
    public bool showSlimePath = false;
    public bool ShowSlimePath => showSlimePath;

    SubmapManager submapManager;
    public SubmapManager SubmapManager => submapManager;


    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        submapManager = GetComponent<SubmapManager>();
        submapManager.PreInitialize();
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();

        submapManager.Initialize();     // 플레이어를 찾은 이후에 실행되어야 한다.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 점수 표시용 UI
    /// </summary>
    ScoreText scoreTextUI;

    /// <summary>
    /// 씬에 있는 플레이어에 접근하기 위한 프로퍼티(읽기전용)
    /// </summary>
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindAnyObjectByType<Player>();
            }
            return player;
        }
    }

    public ScoreText ScoreText
    {
        get
        {
            if (scoreTextUI == null)
            {
                scoreTextUI = FindAnyObjectByType<ScoreText>();
            }
            return scoreTextUI;
        }
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();

        scoreTextUI = FindAnyObjectByType<ScoreText>();
    }
}

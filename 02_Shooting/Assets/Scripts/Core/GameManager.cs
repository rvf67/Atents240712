using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    /// <summary>
    /// 점수 표시용 UI
    /// </summary>
    ScoreText scoreTextUI;
    public Player Player
    {
        get
        {
            if (player == null)
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

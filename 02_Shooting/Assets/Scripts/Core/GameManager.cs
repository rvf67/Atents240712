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
    /// 게임 오버 표시,랭킹 표시, 재시작버튼 용 패널
    /// </summary>
    GameOverPanel gameOverPanelUI;

    /// <summary>
    /// 생명표시용 UI
    /// </summary>
    LifePanel lifePanelUI;
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

        lifePanelUI = FindAnyObjectByType<LifePanel>();
        if(lifePanelUI != null)
        {
            lifePanelUI.OnInitialize();     // 플레이어를 찾은 이후에 실행되어야 함
        }

        gameOverPanelUI= FindAnyObjectByType<GameOverPanel>();
        if(gameOverPanelUI != null)
        {
            gameOverPanelUI.OnInitialize();
        }
    }
}

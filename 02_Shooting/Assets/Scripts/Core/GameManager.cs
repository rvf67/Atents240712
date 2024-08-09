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
    /// 생명 표시용 UI
    /// </summary>
    LifePanel lifePanelUI;

    /// <summary>
    /// 게임 오버 표시,랭킹 표시, 재시작버튼 용 패널
    /// </summary>
    GameOverPanel gameOverPanelUI;

    /// <summary>
    /// 씬에 있는 플레이어에 접근하기 위한 프로퍼티(읽기전용)
    /// </summary>
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

    /// <summary>
    /// ScoreText의 score를 확인하는 프로퍼티
    /// </summary>
    public int Score => ScoreText.Score;    // get만 있는 프로퍼티

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();

        scoreTextUI = FindAnyObjectByType<ScoreText>();
        scoreTextUI?.OnInitialize();
        
        lifePanelUI = FindAnyObjectByType<LifePanel>();
        lifePanelUI?.OnInitialize();     // 플레이어를 찾은 이후에 실행되어야 함
        
        gameOverPanelUI = FindAnyObjectByType<GameOverPanel>();
        gameOverPanelUI?.OnInitialize(); // 플레이어를 찾은 이후에 실행되어야 함
        
    }

    /// <summary>
    /// 점수 추가하는 함수
    /// </summary>
    /// <param name="score">추가되는 점수</param>
    public void AddScore(int score)
    {
        ScoreText?.AddScore(score);
    }
}

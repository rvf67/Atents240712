using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    Player player;
    ScoreText scoreText;
    public int score = 100;

    public bool isStartDie = false;
    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreText;

        if (!isStartDie)
        {
            player.TestDie();
        }
    }
#if UNITY_EDITOR
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.TestDie();
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        scoreText.AddScore(score);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        RankLine line = FindFirstObjectByType<RankLine>();
        line.SetData("°¡°¡°¡",score);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        RankPanel panel = FindFirstObjectByType<RankPanel>();
        panel.Test_DefaultRankPanel();
    }
    protected override void OnTest5(InputAction.CallbackContext context)
    {
        RankPanel panel = FindFirstObjectByType<RankPanel>();
        panel.Test_UpdateRankPanel(score);
    }
#endif
}

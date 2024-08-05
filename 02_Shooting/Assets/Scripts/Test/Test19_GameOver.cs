using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test19_GameOver : TestBase
{
    Player player;
    ScoreText scoreText;
    public int score = 100;
    private void Start()
    {
        player = GameManager.Instance.Player;
        scoreText = GameManager.Instance.ScoreText;
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
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : ObjectPool<PowerUp>
{
    /// <summary>
    /// 점수 표시용 UI
    /// </summary>
    ScoreText scoreText;

    public override void Initialize()
    {
        scoreText = FindAnyObjectByType<ScoreText>();   // 풀이 초기화 될 때 점수 표시용 UI 찾기

        base.Initialize();  // 이 안에서 생성됨
    }
}

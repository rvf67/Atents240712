using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : ObjectPool<PowerUp>
{
    /// <summary>
    /// ���� ǥ�ÿ� UI
    /// </summary>
    ScoreText scoreText;

    public override void Initialize()
    {
        scoreText = FindAnyObjectByType<ScoreText>();   // Ǯ�� �ʱ�ȭ �� �� ���� ǥ�ÿ� UI ã��

        base.Initialize();  // �� �ȿ��� ������
    }
}

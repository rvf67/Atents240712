using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool<T> : ObjectPool<T> where T : EnemyBase
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

    /// <summary>
    /// ���� �ϳ� ������ �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="comp">������ ���� ������Ʈ</param>
    protected override void OnGenerateObject(T comp)
    {
        if (scoreText != null)
        {
            comp.onDie += scoreText.AddScore;   // ��� ��������Ʈ�� ���� ǥ�� UI�� �Լ��� ���
        }
    }
}

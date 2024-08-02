using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool<T> : ObjectPool<T> where T : EnemyBase
{
    /// <summary>
    /// 점수 표시용 UI
    /// </summary>
    ScoreText scoreText;

    public override void Initialize()
    {
        scoreText = GameManager.Instance.ScoreText;   // 풀이 초기화 될 때 점수 표시용 UI 찾기

        base.Initialize();  // 이 안에서 생성됨
    }

    /// <summary>
    /// 적이 하나 생성될 때 실행되는 함수
    /// </summary>
    /// <param name="comp">생성된 적의 컴포넌트</param>
    protected override void OnGenerateObject(T comp)
    {
        if (scoreText != null)
        {
            comp.onDie += scoreText.AddScore;   // 사망 델리게이트에 점수 표시 UI의 함수를 등록
        }
    }
}

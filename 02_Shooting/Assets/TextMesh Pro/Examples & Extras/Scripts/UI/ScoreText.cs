using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI score;

    int goalScore=0;
    float timer;
    public int upSpeed;
    /// <summary>
    /// 보이는 점수
    /// </summary>
    float displayScore;

    public int Score
    {
        get => goalScore;
        private set
        {
            goalScore = value;
            //score.text = $"{goalScore}";
            //score.text = $"{displayScore}";
        } 
    }
    private void Awake()
    {
        Transform child = transform.GetChild(1);
        score = child.GetComponent<TextMeshProUGUI>();
        upSpeed = 50;
    }
    private void Update()
    {
        if (displayScore < goalScore)
        {
            float speed=Mathf.Max((goalScore - displayScore)*5.0f, upSpeed);
            displayScore += Time.deltaTime*speed;

            displayScore = Mathf.Min(displayScore, goalScore);
            score.text = $"{(int)displayScore}";
        }
        
    }

    public void AddScore(int point)
    {
        Score += point;
    }
}

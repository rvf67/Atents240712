using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI score;

    int goalScore=0;

    public int Score
    {
        get => goalScore;
        set
        {
            goalScore = value;
            score.text = $"Score: {score}";
        } 
    }
    private void Awake()
    {
        score =GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        score.text= "±ÛÀÚ";
    }
}

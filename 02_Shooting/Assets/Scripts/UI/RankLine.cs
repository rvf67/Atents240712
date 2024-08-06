using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankLine : MonoBehaviour
{
    TextMeshProUGUI nameText;
    TextMeshProUGUI recordText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        nameText = child.GetComponent<TextMeshProUGUI>();
        child =transform.GetChild(2);
        recordText = child.GetComponent<TextMeshProUGUI>();
    }

    public void SetData(string ranker, int score)
    {
        nameText.text = ranker;
        recordText.text = score.ToString("N0"); //3ÀÚ¸®¸¶´Ù ÄÞ¸¶ Âï±â
    }
}

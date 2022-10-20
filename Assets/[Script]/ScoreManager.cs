using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField]
    private Text text;

    public void UpdateScore(int value)
    {
        score += value;
        text.text = "SCORE: " + score;
    }
}

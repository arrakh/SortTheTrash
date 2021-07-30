using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        GameManager.OnScoreUpdate += ScoreUpdate;
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdate -= ScoreUpdate;
    }

    private void ScoreUpdate(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}

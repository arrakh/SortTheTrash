using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private int getScoreOnCorrect = 10;
    [SerializeField] private int loseScoreOnWrong = 15;
    [SerializeField] private GameObject gameOverPanel;

    public static Action OnGameOver;
    public static Action<int> OnScoreUpdate;

    public int Score 
    { 
        get => score;
        set 
        {
            score = value;
            if(value < 0)
            {
                score = 0;
                GameOver();
            }
            OnScoreUpdate?.Invoke(score);
        }
    }

    public int Lives 
    { 
        get => lives;
        set 
        {
            lives = Mathf.Clamp(value, 0, int.MaxValue);
            if (lives == 0) GameOver();
        } 
    }

    private void OnEnable()
    {
        Trash.OnTrashDrop += TrashDrop;
        TrashClearer.OnTrashCleared += TrashCleared;
    }

    private void OnDisable()
    {
        Trash.OnTrashDrop -= TrashDrop;
        TrashClearer.OnTrashCleared -= TrashCleared;
    }

    //Function ini akan dipanggil saat sebuah sampah di buang
    private void TrashDrop(bool isCorrect)
    {
        if (isCorrect) Score += getScoreOnCorrect;
        else Score -= loseScoreOnWrong;
    }

    private void TrashCleared()
    {
    }

    //Nyalakan panel Game Over lalu restart scene nya dalam 3 detik
    private void GameOver()
    {
        gameOverPanel.SetActive(true);

        Invoke(nameof(RestartLevel), 3f);
        OnGameOver?.Invoke();
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("S_Game");
    }
}

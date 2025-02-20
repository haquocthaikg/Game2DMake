using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    private bool _isGameOver = false;

    private void Start()
    {
        UpdateScoreText();
        gameOverUI.SetActive(false);
    }

    public void AddScore(int score)
    {
        if (_isGameOver) return;
        _score += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = _score.ToString();
    }

    public void GameOver()
    {
        _isGameOver = true;
        _score = 0;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        _isGameOver = false;
        _score = 0;
        UpdateScoreText();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
}
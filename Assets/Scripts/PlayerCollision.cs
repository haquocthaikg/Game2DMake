using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            _gameManager.AddScore(1);
            _audioManager.PlayCoinSound();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            _gameManager.GameOver();
        }
        else if (other.CompareTag("Enemy"))
        {
            _gameManager.GameOver();
        }
    }
}
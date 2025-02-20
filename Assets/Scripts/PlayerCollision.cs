using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            _gameManager.AddScore(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            _gameManager.GameOver();
        }
        else if (other.CompareTag($"Emeny"))
        {
            _gameManager.GameOver();
        }
    }
}
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Animator _coinCountAnimator;
    [SerializeField] private CoinSpawner _spawner;
    [SerializeField] private string _coinCollectedTrigger;

    private int _currentScore;
    private int _totalCoinsCount;

    private void Start()
    {
        ShowScore();
    }

    private void OnEnable()
    {
        _spawner.Spawned += OnCoinSpawned;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= OnCoinSpawned;
    }

    private void OnCoinSpawned(Coin coin)
    {
        _totalCoinsCount++;
        coin.Collected += OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        _currentScore++;
        ShowScore();
        _coinCountAnimator.SetTrigger(_coinCollectedTrigger);
    }

    private void ShowScore()
    {
        _scoreText.text = $"{_currentScore}/{_totalCoinsCount}";
    }
}
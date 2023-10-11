using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Animator _coinCountAnimator;
    [SerializeField] private CoinSpawner _spawner;
    [SerializeField] private string _coinCollectedTrigger;

    private int _currentScore;

    private void Start()
    {
        _currentScore = 0;
        _scoreText.text = _currentScore.ToString();
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
        coin.Collected += OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        _scoreText.text = (++_currentScore).ToString();
        _coinCountAnimator.SetTrigger(_coinCollectedTrigger);
    }
}
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform[] _spawnPoints;

    public event UnityAction<Coin> Spawned;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Coin newCoin = Instantiate(_prefab, spawnPoint, false);

            Spawned?.Invoke(newCoin);
        }
    }
}
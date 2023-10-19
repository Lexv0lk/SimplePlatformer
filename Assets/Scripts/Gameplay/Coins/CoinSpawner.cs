using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform _placesParent;

    public event UnityAction<Coin> Spawned;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (Transform spawnPoint in _placesParent)
        {
            Coin newCoin = Instantiate(_prefab, spawnPoint, false);

            Spawned?.Invoke(newCoin);
        }
    }
}
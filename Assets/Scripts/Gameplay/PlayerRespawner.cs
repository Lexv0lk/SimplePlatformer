using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _respawnPoint;

    public void Respawn()
    {
        _player.SetActive(false);
        _player.transform.position = _respawnPoint.position;
        _player.SetActive(true);
    }
}
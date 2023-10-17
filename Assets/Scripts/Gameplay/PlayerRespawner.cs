using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    [SerializeField] private CharacterMover _player;
    [SerializeField] private Transform _respawnPoint;

    public void Respawn()
    {
        _player.Deactivate();
        _player.transform.position = _respawnPoint.position;
        _player.Activate();
    }
}
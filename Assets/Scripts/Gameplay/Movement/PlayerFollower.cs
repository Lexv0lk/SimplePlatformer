using UnityEngine;

[RequireComponent(typeof(TargetMover))]
public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private PatrolMover _patrolMover;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private float _followSpeed = 2.5f;
    [SerializeField] private float _distanceToTarget = 1f;

    private TargetMover _targetMover;

    private void Awake()
    {
        _targetMover = GetComponent<TargetMover>();
    }

    private void OnEnable()
    {
        _fieldOfView.FoundPlayer += OnFoundPlayer;
        _fieldOfView.LostPlayer += OnLostPlayer;
    }

    private void OnDisable()
    {
        _fieldOfView.FoundPlayer -= OnFoundPlayer;
        _fieldOfView.LostPlayer -= OnLostPlayer;
    }

    private void OnFoundPlayer(Player player)
    {
        _patrolMover.enabled = false;
        _targetMover.SetTarget(player.transform, _followSpeed, _distanceToTarget);
    }

    private void OnLostPlayer()
    {
        _patrolMover.enabled = true;
        _patrolMover.Activate();
    }
}
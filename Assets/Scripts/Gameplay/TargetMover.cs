using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class TargetMover : MonoBehaviour
{
    [SerializeField] private GroundView _groundView;
    [SerializeField] private Repulsor _repulsor;

    private CharacterMover _mover;
    private Transform _target;
    private Vector3 _lastTargetPosition;
    private Vector3 _currentDirection;
    private bool _checkForGroundAhead = true;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _repulsor.Repulsed += OnTookRepulsion;
        _repulsor.RestartedMoving += OnRestartedMoving;
    }

    private void OnDisable()
    {
        _repulsor.Repulsed -= OnTookRepulsion;
        _repulsor.RestartedMoving -= OnRestartedMoving;
    }

    private void FixedUpdate()
    {
        if (_target == null || _repulsor.CanMove == false)
            return;

        if (_target.position != _lastTargetPosition)
            RecalculateDirection();

        if (_groundView.IsGroundAhead)
            _mover.Move(_currentDirection);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _lastTargetPosition = target.position;
        RecalculateDirection();
    }

    public void Discard()
    {
        _target = null;
        _mover.Move(Vector2.zero);
    }

    private void OnTookRepulsion()
    {
        _checkForGroundAhead = false;
        _mover.Move(Vector2.zero);
    }

    private void OnRestartedMoving()
    {
        RecalculateDirection();
    }

    private void RecalculateDirection()
    {
        _currentDirection = (_lastTargetPosition - transform.position).normalized;
    }
}

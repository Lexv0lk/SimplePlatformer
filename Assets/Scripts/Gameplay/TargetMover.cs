using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class TargetMover : MonoBehaviour
{
    [SerializeField] private GroundView _groundView;
    [SerializeField] private Repulsor _repulsor;
    [SerializeField] private CharacterFlipper _flipper;

    private CharacterMover _mover;
    private Transform _target;
    private Vector3 _lastTargetPosition;
    private Vector3 _currentDirection;
    private float _speed;
    private float _accuracy;

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

        if (_groundView.IsGroundAhead && Mathf.Abs(_target.position.x - transform.position.x) >= _accuracy)
            _mover.Move(_currentDirection, _speed);
    }

    public void SetTarget(Transform target, float speed, float accuracy)
    {
        _target = target;
        _speed = speed;
        _accuracy = accuracy;
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
        _mover.Move(Vector2.zero);
    }

    private void OnRestartedMoving()
    {
        if (_target != null)
            RecalculateDirection();
    }

    private void RecalculateDirection()
    {
        _lastTargetPosition = _target.position;
        _currentDirection = (_lastTargetPosition - transform.position).normalized;

        if (Vector2.Dot(transform.right.normalized, _currentDirection) < 0)
            _flipper.Flip();
    }
}
using UnityEngine;

[RequireComponent(typeof(TargetMover))]
public class PatrolMover : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Health _health;

    private const float _inaccuracyValue = 0.05f;
    private int _currentPointIndex = 0;
    private TargetMover _mover;

    private void Awake()
    {
        _mover = GetComponent<TargetMover>();
    }

    private void Start()
    {
        Activate();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - _patrolPoints[_currentPointIndex].position.x) <= _inaccuracyValue)
            PickNextPoint();
    }

    public void Activate()
    {
        _mover.SetTarget(_patrolPoints[_currentPointIndex]);
    }

    private void PickNextPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        _mover.SetTarget(_patrolPoints[_currentPointIndex]);
    }
}
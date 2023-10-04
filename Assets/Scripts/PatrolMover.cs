using UnityEngine;

public class PatrolMover : CharacterMover
{
    [SerializeField] private Transform[] _patrolPoints;

    private const float _inaccuracyValue = 0.05f;
    private int _currentPointIndex = 0;
    private Vector2 _currentDirection = Vector2.zero;

    private void Start()
    {
        _currentDirection = (_patrolPoints[_currentPointIndex].position - transform.position).normalized;
    }

    private void Update()
    {
        //print(transform.position.x + " - " + _patrolPoints[_currentPointIndex].position.x);
        if (Mathf.Abs(transform.position.x - _patrolPoints[_currentPointIndex].position.x) <= _inaccuracyValue)
            PickNextPoint();

        Move(_currentDirection);
    }

    private void PickNextPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        _currentDirection = (_patrolPoints[_currentPointIndex].position - transform.position).normalized;
    }
}

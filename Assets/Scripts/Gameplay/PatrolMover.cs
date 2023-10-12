using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class PatrolMover : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Repulsor _repulsor;
    [SerializeField] private Health _health;

    private const float _inaccuracyValue = 0.05f;
    private int _currentPointIndex = 0;
    private Vector2 _currentDirection = Vector2.zero;
    private CharacterMover _mover;
    private bool _canMove = true;
    private bool _canCheckOnMoving = true;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _repulsor.Repulsed += OnRepulsed;
    }

    private void OnDisable()
    {
        _repulsor.Repulsed -= OnRepulsed;
    }

    private void Start()
    {
        _currentDirection = (_patrolPoints[_currentPointIndex].position - transform.position).normalized;
    }

    private void Update()
    {
        if (_canCheckOnMoving == false)
            return;

        if (_canMove == false)
        {
            _canMove = _mover.GroundChecker.IsGrounded;

            if (_canMove == true)
                _currentDirection = (_patrolPoints[_currentPointIndex].position - transform.position).normalized;
            else
                return;
        }

        if (Mathf.Abs(transform.position.x - _patrolPoints[_currentPointIndex].position.x) <= _inaccuracyValue)
            PickNextPoint();
   
        _mover.Move(_currentDirection);
    }

    private void PickNextPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        _currentDirection = (_patrolPoints[_currentPointIndex].position - transform.position).normalized;
    }

    private void OnRepulsed()
    {
        _canMove = false;
        _mover.Move(Vector2.zero);
        StartCoroutine(StopMoving(0.5f));
    }

    private IEnumerator StopMoving(float time)
    {
        _canCheckOnMoving = false;
        yield return new WaitForSeconds(time);
        _canCheckOnMoving = true;
    }
}
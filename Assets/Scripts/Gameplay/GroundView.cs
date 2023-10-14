using UnityEngine;
using UnityEngine.Events;

public class GroundView : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _distance;
    [SerializeField, Range(0, 90)] private float _offsetAngle;

    public bool IsGroundAhead { get; private set; } = false;
    public float Distance => _distance;
    public float OffsetAngle => _offsetAngle;

    public event UnityAction GroundLost;

    private void FixedUpdate()
    {
        CheckForGround();
    }

    private void CheckForGround()
    {
        Vector2 checkDirection = (Quaternion.AngleAxis(-_offsetAngle, transform.forward) * transform.right).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, _distance, _groundMask);
        bool wasGroundAhead = IsGroundAhead;

        if (hit.collider != null)
            IsGroundAhead = true;
        else
            IsGroundAhead = false;

        if (wasGroundAhead && IsGroundAhead == false)
            GroundLost?.Invoke();
    }
}
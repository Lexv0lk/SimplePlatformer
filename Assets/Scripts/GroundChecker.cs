using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _checkTransform;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkRadius = 0.3f;

    public bool IsGrounded { get; private set; }

    public event UnityAction GroundLost;
    public event UnityAction GroundFound;

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(_checkTransform.position, Vector2.down, _checkRadius, _groundLayer.value);

        if (hitInfo.collider != null)
            FindGround();
        else
            LoseGround();
    }

    private void LoseGround()
    {
        if (IsGrounded)
        {
            IsGrounded = false;
            GroundLost?.Invoke();
        }
    }

    private void FindGround()
    {
        if (IsGrounded == false)
        {
            IsGrounded = true;
            GroundFound?.Invoke();
        }
    }
}
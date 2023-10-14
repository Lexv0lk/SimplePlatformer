using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _viewMask;
    [SerializeField] private float _radius;
    [SerializeField, Range(0, 360)] private float _angle;

    private bool _canSeePlayer = false;

    public event UnityAction<Player> FoundPlayer;
    public event UnityAction LostPlayer;

    public float Radius => _radius;
    public float Angle => _angle;

    private void FixedUpdate()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider2D checkedCollider = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);

        if (checkedCollider != null)
        {
            bool wasPlayerSeen = _canSeePlayer;
            Transform player = checkedCollider.transform;
            Vector2 direction = (player.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, direction) < _angle / 2)
            {
                float distance = Vector2.Distance(transform.position, player.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, _viewMask);

                if (hit.collider != null)
                    _canSeePlayer = hit.collider.gameObject == player.gameObject;
                else
                    _canSeePlayer = false;
            }
            else
            {
                _canSeePlayer = false;
            }

            if (wasPlayerSeen == false && _canSeePlayer == true)
                FoundPlayer?.Invoke(player.GetComponent<Player>());
            else if (wasPlayerSeen == true && _canSeePlayer == false)
                LostPlayer?.Invoke();
        }
    }
}
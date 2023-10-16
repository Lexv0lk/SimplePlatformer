using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Repulsor : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _knockedTime = 0.5f;
    [SerializeField] private bool _disableAirMoving = true;

    private Rigidbody2D _rigidbody;

    public event UnityAction Repulsed;
    public event UnityAction RestartedMoving;

    public bool CanMove { get; private set; } = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeRepulsion(Vector2 force)
    {
        CanMove = false;
        Repulsed?.Invoke();
        StartCoroutine(RestartMovingAbility(_knockedTime));
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator RestartMovingAbility(float lagTime)
    {
        yield return new WaitForSeconds(lagTime);

        while (CanMove == false && _disableAirMoving == true)
        {
            CanMove = _groundChecker.IsGrounded;
            yield return null;
        }

        CanMove = true;
        RestartedMoving?.Invoke();
    }
}
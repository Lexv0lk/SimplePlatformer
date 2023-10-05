using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
    [SerializeField] private string _disappearTrigger;

    private Animator _animator;

    public event UnityAction Collected;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;

        if (collision.gameObject.TryGetComponent(out player) == true)
        {
            _animator.SetTrigger(_disappearTrigger);
            Collected?.Invoke();
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Heal : MonoBehaviour
{
    [SerializeField] private int _healAmount = 15;
    [SerializeField] private string _disappearTrigger;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health;

        if (collision.TryGetComponent(out health))
        {
            health.TakeHeal(_healAmount);
            _animator.SetTrigger(_disappearTrigger);
        }
    }
}
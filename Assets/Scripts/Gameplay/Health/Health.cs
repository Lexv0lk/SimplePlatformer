using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maximalValue = 100;
    private int _currentValue;

    public int MaximalValue => _maximalValue;
    public int CurrentValue => _currentValue;

    public event UnityAction Healed;
    public event UnityAction TakenDamage;

    private void Awake()
    {
        _currentValue = _maximalValue;
    }

    public void TakeDamage(int value)
    {
        _currentValue -= value;
        _currentValue = Mathf.Max(0, _currentValue);
        TakenDamage?.Invoke();
    }

    public void TakeHeal(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Min(_maximalValue, _currentValue);
        Healed?.Invoke();
    }
}
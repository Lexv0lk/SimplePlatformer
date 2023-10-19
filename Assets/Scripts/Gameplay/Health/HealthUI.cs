using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] protected Slider HealthSlider;
    [SerializeField] private Animator _sliderAnimator;
    [SerializeField] private float _sliderSpeed = 1;
    [SerializeField] private string _healthChangingTrigger = "HealthChanging";
    [SerializeField] private string _healthChangedTrigger = "HealthChanged";
    [SerializeField] private string _vanishTrigger = "Vanish";

    private Coroutine _currentChanging;

    protected virtual void Start()
    {
        HealthSlider.maxValue = _health.MaximalValue;
        HealthSlider.value = _health.CurrentValue;
    }

    protected virtual void OnEnable()
    {
        _health.Healed += OnHealthChanged;
        _health.TakenDamage += OnHealthChanged;
    }

    protected virtual void OnDisable()
    {
        _health.Healed -= OnHealthChanged;
        _health.TakenDamage -= OnHealthChanged;
    }

    public void Vanish()
    {
        _sliderAnimator.SetTrigger(_vanishTrigger);
    }

    private void OnHealthChanged()
    {
        if (_currentChanging != null)
            StopCoroutine(_currentChanging);

        _currentChanging = StartCoroutine(ChangeHealthValue(_health.CurrentValue));
    }

    private IEnumerator ChangeHealthValue(int newValue)
    {
        if (HealthSlider.value == newValue)
            yield break;

        _sliderAnimator.SetTrigger(_healthChangingTrigger);

        while (HealthSlider.value != newValue)
        {
            HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, newValue, _sliderSpeed * Time.deltaTime);
            yield return null;
        }

        _sliderAnimator.SetTrigger(_healthChangedTrigger);
    }
}
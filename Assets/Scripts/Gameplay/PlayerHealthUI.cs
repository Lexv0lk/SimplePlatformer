using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Animator _sliderAnimator;
    [SerializeField] private float _sliderSpeed = 1;
    [SerializeField] private string _healthChangingTrigger = "HealthChanging";
    [SerializeField] private string _healthChangedTrigger = "HealthChanged";

    private Coroutine _currentChanging;

    private void Start()
    {
        _healthSlider.maxValue = _playerHealth.MaximalValue;
        _healthSlider.value = _playerHealth.CurrentValue;
        _valueText.text = $"{_playerHealth.CurrentValue}/{_playerHealth.MaximalValue}";
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
        _healthSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
        _healthSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnHealthChanged()
    {
        if (_currentChanging != null)
            StopCoroutine(_currentChanging);

        _sliderAnimator.SetTrigger(_healthChangingTrigger);
        _currentChanging = StartCoroutine(ChangeHealthValue(_playerHealth.CurrentValue));
    }

    private IEnumerator ChangeHealthValue(int newValue)
    {
        while (_healthSlider.value != newValue)
        {
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, newValue, _sliderSpeed * Time.deltaTime);
            yield return null;
        }

        _sliderAnimator.SetTrigger(_healthChangedTrigger);
    }

    private void OnSliderValueChanged(float newValue)
    {
        _valueText.text = $"{(int)newValue}/{_playerHealth.MaximalValue}";
    }
}

using Anima2D;
using System.Collections;
using UnityEngine;

public class HealthChangingPainter : MonoBehaviour
{
    [SerializeField] private Transform _meshesParent;
    [SerializeField] private Health _health;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private Color _healColor;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _defaultColor = Color.white;

    private SpriteMeshInstance[] _meshes;

    private void Awake()
    {
        _meshes = _meshesParent.GetComponentsInChildren<SpriteMeshInstance>();
    }

    private void OnEnable()
    {
        _health.Healed += OnHealed;
        _health.TakenDamage += OnTakenDamage;
    }

    private void OnDisable()
    {
        _health.Healed -= OnHealed;
        _health.TakenDamage -= OnTakenDamage;
    }

    private void OnTakenDamage()
    {
        StartCoroutine(ChangeColor(_damageColor));
    }

    private void OnHealed()
    {
        StartCoroutine(ChangeColor(_healColor));
    }

    private IEnumerator ChangeColor(Color color)
    {
        float passedTime = 0;

        foreach (var mesh in _meshes)
            mesh.color = color;

        while (_meshes[0].color != _defaultColor)
        {
            foreach (var mesh in _meshes)
                mesh.color = Color.Lerp(color, _defaultColor, Mathf.Clamp01(passedTime / _duration));

            yield return null;
            passedTime += Time.deltaTime;
        }
    }
}
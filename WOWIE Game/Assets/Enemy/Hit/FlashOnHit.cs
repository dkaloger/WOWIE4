using DG.Tweening;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Health _health;

    private Sequence _sequence;

    private Color _color;
    private static readonly int Flash = Shader.PropertyToID("Flash");

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        _health.OnHit += HealthOnHit;
    }

    private void OnDisable()
    {
        _health.OnHit -= HealthOnHit;
    }

    private void HealthOnHit(float currentHealth, bool isHit)
    {
        if (_sequence != null)
            _sequence.Kill();

        _sequence = DOTween.Sequence()
            .Append(mat.DOFloat(1, Flash, 0.1f))
            .Append(mat.DOFloat(0, Flash, 0.1f))
            .Play();
    }
}

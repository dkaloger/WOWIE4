using DG.Tweening;
using Entity.Hit;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float flashDelay = 0.25f;
    [SerializeField] private float flashTime = 0.1f;
    [SerializeField] private Image healthBar, healthBarFlash, shieldBar, shieldBarFlash;
    
    [SerializeField] private Health health;

    private void OnEnable()
    {
        if (health is ShieldedHealth shieldedHealth)
            shieldedHealth.OnHitShield += ShieldUpdate;

        health.OnHit += HealthUpdate;
    }

    private void OnDisable()
    {
        if (health is ShieldedHealth shieldedHealth)
            shieldedHealth.OnHitShield -= ShieldUpdate;

        health.OnHit -= HealthUpdate;
    }

    public void HealthUpdate(float newPercent, bool isHit)
    {
        
        healthBar.fillAmount = newPercent;
        healthBarFlash.DOFillAmount(newPercent, flashTime).SetDelay(flashDelay);
    }

    private void ShieldUpdate(float newPercent)
    {
        shieldBar.fillAmount = newPercent;
        shieldBarFlash.DOFillAmount(newPercent, flashTime).SetDelay(flashDelay);
    }
}

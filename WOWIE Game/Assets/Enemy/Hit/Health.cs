using System;
using UnityEngine;

/// <summary>
/// Keep track of health
/// </summary>
public class Health : MonoBehaviour, IHitReceiver
{
    // Serialized Fields - set in the Unity Inspector

    #region SerializedFields

    [SerializeField] private float maxHealth;

    #endregion

    public float MaxHealth => maxHealth;

    // Events - broadcast a message to any objects that are listening

    #region Events

    // the event to broadcast
    public event Action<float, bool> OnHit;

    #endregion


    // Private fields - only used in this script

    #region PrivateFields

    // some private variables to track various health values
    protected float _currentHealth;
    protected float _previousHealth;
    protected float _healthPercentage;

    #endregion

    public float CurrentHealth => _currentHealth;
    public float PreviousHealth => _previousHealth;
    public float HealthPercentage => _healthPercentage;

    /// <summary>
    /// Called when the object is enabled
    /// Reset the current health
    /// </summary>
    protected virtual void OnEnable()
    {
        _healthPercentage = 1;
        _previousHealth = _currentHealth = maxHealth;
    }

    /// <summary>
    /// Receive a hit
    /// </summary>
    /// <param name="data"></param>
    public virtual void ReceiveHit(HitData data)
    {
        if (!enabled)
            return;
        // keep track of what the health used to be
        _previousHealth = _currentHealth;

        // keep track of what the health currently is 
        // keep track of the current health as a percentage
        // - doing it here means we only have to do the divide once, and division is a computationally expensive operation
        _healthPercentage = _currentHealth / maxHealth;
        InvokeHitEvent(data.Damage > 0);
    }

    protected void InvokeHitEvent(bool isHit)
    {
        // tell everything that cares "hey, I've been hit"
        OnHit?.Invoke(_healthPercentage, isHit);
    }
}
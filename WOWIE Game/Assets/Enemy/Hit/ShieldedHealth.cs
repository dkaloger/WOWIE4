using System;
using UnityEngine;

namespace Entity.Hit
{
    public class ShieldedHealth : Health
    {
        [SerializeField] private float maxShield;
        [SerializeField] private float shieldRegen;
        [SerializeField] private float healthRegen;
        [SerializeField] private float shieldCooldownSeconds;
        public float MaxShield => maxShield;
        public float ShieldRegen => shieldRegen;
        public float HealthRegen => healthRegen;

        public float ShieldCooldown => shieldCooldownSeconds;
    
        private float _currentShield;
        private float _previousShield;
        private float _shieldPercentage;

        public event Action<float> OnHitShield;
        public event Action OnShieldDown;

        private float _shieldCooldownTimer;

        protected override void OnEnable()
        {
            base.OnEnable();
            _shieldPercentage = 1;
            _previousShield = _currentShield = MaxShield;
        }


        private void Update()
        {
            _shieldCooldownTimer -= Time.deltaTime;
            if (_shieldCooldownTimer < 0)
            {
                if (_currentShield < MaxShield)
                {
                    _currentShield += ShieldRegen * Time.deltaTime;
                    _shieldPercentage =  _currentShield / MaxShield;
                    OnHitShield?.Invoke(_shieldPercentage);
                    _previousShield = _currentShield;
                }
                else if (_currentHealth < MaxHealth)
                {
                    _currentHealth += HealthRegen * Time.deltaTime;
                    _healthPercentage = _currentHealth / MaxHealth;
                    InvokeHitEvent(false);
                    _previousHealth = _currentHealth;
                }
            }
        }

        public override void ReceiveHit(HitData data)
        {
            // keep track of what the health used to be
            _previousShield = _currentShield;

            // after being hit, delay the regen
            _shieldCooldownTimer = ShieldCooldown;
        
            // keep track of what the health currently is 
            if (_currentShield < 0)
                _currentShield = 0;
        
            // keep track of the current health as a percentage
            // - doing it here means we only have to do the divide once, and division is a computationally expensive operation
            _shieldPercentage = _currentShield / MaxShield;
            OnHitShield?.Invoke(_shieldPercentage);
            
            if (_currentShield <= 0 && _previousShield > 0)
            {
                OnShieldDown?.Invoke();
            }

            data.Damage -= _previousShield;
            if (data.Damage < 0)
                data.Damage = 0;

            if (data.Damage > 0)
                base.ReceiveHit(data);
        }
    }
}
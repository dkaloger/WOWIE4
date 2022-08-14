using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask layersToHit;
    
    [SerializeField] private UnityEvent attackStart;
    [SerializeField] private UnityEvent attackEnd;
    
    [SerializeField] private float hitCooldown;

    [SerializeField] private Vector3 damageOffset;
    [SerializeField] private float damageRadius;

    [SerializeField] private float attackLengthInSeconds = 0.1f;

    [SerializeField, ScriptableObjectDropdown(typeof(AttackTokenHolder))] private AttackTokenHolder tokensToUse;

    [SerializeField] private SpriteRenderer sprite;
    
    private float _hitTimer;

    private bool _attacking;

    private Collider2D[] _hitResults = new Collider2D[16];

    private List<Collider2D> _attackedEnemies = new ();

    private AttackToken _token;
    
    private void Update()
    {
        _hitTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_attacking)
        {
            
            var updatedDamageOffset = damageOffset;
            updatedDamageOffset.x *= sprite.flipX ? -1 : 1;
            
            var numHit = Physics2D.OverlapCircleNonAlloc(transform.position + updatedDamageOffset, damageRadius, _hitResults, layersToHit);
            for (int i = 0; i < numHit; ++i)
            {
                if (_attackedEnemies.Contains(_hitResults[i]))
                    continue;
                _attackedEnemies.Add(_hitResults[i]);
                var hittable = _hitResults[i].GetComponent<IHitReceiver>();
                hittable.ReceiveHit(new HitData
                {
                    Damage = damage,
                    DamageType = DamageType.Melee,
                    IncomingObject = gameObject,
                    IncomingDirection =  (_hitResults[i].transform.position - transform.position).normalized
                });
              

            }
        }
    }

    public void Attack()
    {
        if (_hitTimer < hitCooldown) return;
        if (_token != null) return;
        
        _token = tokensToUse.RequestToken(this, 0);
        if (_token == null) return;
        GetComponent<Animator>().SetTrigger("Attack");
        _attackedEnemies.Clear();
        _hitTimer -= hitCooldown;
        _attacking = true;
        attackStart?.Invoke();
        DOVirtual.DelayedCall(attackLengthInSeconds, () =>
        {
            attackEnd?.Invoke();
            _attacking = false;
            tokensToUse.ReturnToken(_token);
            _token = null;
        });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        var updatedDamageOffset = damageOffset;
        updatedDamageOffset.x *= sprite.flipX ? -1 : 1;
        Gizmos.DrawWireSphere(transform.position + updatedDamageOffset, damageRadius);
    }
}
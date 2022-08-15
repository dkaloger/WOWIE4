using System;
using DG.Tweening;
using UnityEngine;

public class ThrowHeldItem : MonoBehaviour, IHeldItem
{
    [SerializeField] private float damageToDeal = 10f;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    
    [SerializeField] private float throwForce = 10f;

    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private float hitRadius = 0.6f;
    
    private Transform _parent;

    private Collider2D[] _hit = new Collider2D[16];

    private PlayerController controller;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Pickup()
    {
        _rb.velocity = Vector2.zero;
        _rb.simulated = false;
        _parent = transform.parent;
    }

    public void Drop()
    {
        Vector3 target = controller.movement.normalized;
        
        _rb.MovePosition(_parent.position + target * 1.5f);
        _rb.simulated = true;
        _rb.AddForce(target * throwForce);
        _collider.enabled = false;
        DOVirtual.DelayedCall(0.25f, () => _collider.enabled = true);
    }

    private void FixedUpdate()
    {
        if (!_rb.simulated || _rb.velocity.sqrMagnitude < 1)
            return;

        var numHit = Physics2D.OverlapCircleNonAlloc(transform.position, hitRadius, _hit, layersToHit);
        for (int i = 0; i < numHit; ++i)
        {
            _hit[i].GetComponentInParent<IHitReceiver>()?.ReceiveHit(new HitData
            {
                Damage = damageToDeal
            });
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;

public class ThrowHeldItem : MonoBehaviour, IHeldItem
{
    [SerializeField] private float damageToDeal = 10f;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    
    [SerializeField] private float throwForce = 10f;

    private Transform _parent;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void Pickup()
    {
        _rb.velocity = Vector2.zero;
        _rb.simulated = false;
        _parent = transform.parent;
    }

    public void Drop()
    {
        Vector3 target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        target.z = 0;

        _rb.MovePosition(_parent.position + target.normalized * 1.5f);
        _rb.simulated = true;
        _rb.AddForce(target.normalized * throwForce);
        _collider.enabled = false;
        DOVirtual.DelayedCall(0.25f, () => _collider.enabled = true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            col.collider.GetComponent<IHitReceiver>().ReceiveHit(new HitData
            {
                Damage = damageToDeal
            });
        }
    }
}
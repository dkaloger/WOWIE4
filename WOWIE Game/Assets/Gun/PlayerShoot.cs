using System;
using BulletFury;
using UnityEngine;


public class PlayerShoot : MonoBehaviour, IHeldItem
{
    [SerializeField] private BulletManager bullets;

    private bool _held = false;

    public void Pickup()
    {
        _held = true;
    }

    public void Drop()
    {
        _held = false;
    }

    private void Update()
    {
        if (!_held) return;
        
        if (Input.GetButton("Fire1"))
            bullets.Spawn(transform.position, transform.right);
    }
}
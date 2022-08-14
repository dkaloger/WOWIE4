using System;
using BulletFury;
using UnityEngine;


public class PlayerShoot : MonoBehaviour, IHeldItem
{
    [SerializeField] private BulletManager bullets;
    Animator Playeranim;
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
        Playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Playeranim.SetBool("HoldingGun", false);
        if (!_held) return;
        Playeranim.SetBool("HoldingGun", true);

        if (Input.GetButton("Fire1"))
          //  transform.position + (Vector3)movement.normalized + new Vector3(0.0f, -0.5f, 0f)
            bullets.Spawn(Playeranim.gameObject.transform.position +  new Vector3(0.0f, -0.3f, 0f), (Vector3)Playeranim.gameObject.GetComponent<PlayerController>().movement.normalized);
    }
}
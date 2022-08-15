using System;
using BulletFury;
using UnityEngine;


public class PlayerShoot : MonoBehaviour, IHeldItem
{
    [SerializeField] private BulletManager bullets;
    Animator Playeranim;
    private bool _held = false;
    private static readonly int HoldingGun = Animator.StringToHash("HoldingGun");

    private void Start()
    {
        Playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

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
        Playeranim.SetBool(HoldingGun, false);
        if (!_held) return;
        Playeranim.SetBool(HoldingGun, true);

        Vector3 target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        target.z = 0;
        var dir = target;
        if (Input.GetButton("Fire1"))
          //  transform.position + (Vector3)movement.normalized + new Vector3(0.0f, -0.5f, 0f)
            bullets.Spawn(Playeranim.gameObject.transform.position, (Vector3)Playeranim.gameObject.GetComponent<PlayerController>().movement.normalized);


    }//(Vector3)Playeranim.gameObject.GetComponent<PlayerController>().movement.normalized
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 0.25f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float speed = 10;
    [SerializeField] private float stopDistance = 1.25f;
    [SerializeField] private float aggroRange = 4f;
    [SerializeField] private float aggroCooldown = 1f;

    [SerializeField] private UnityEvent InRange;

    private static Transform _player;

    private Rigidbody2D _rb;

    private Vector3 _dirToMove = Vector3.up;
    private Vector3 _velocity;

    private float _stopSq;
    private float _aggroSq;
    private float _aggroTimer = 0f;

    private Squirrel3 _rnd;

    // Start is called before the first frame update
    void Start()
    {
        _rnd = new Squirrel3();
        if (_player == null)
            _player = GameObject.FindWithTag("THE AI").transform;

        _rb = GetComponent<Rigidbody2D>();
        _stopSq = stopDistance * stopDistance;
        _aggroSq = aggroRange * aggroRange;
    }

    private void FixedUpdate()
    {
        var distToPlayerSq = (_player.position - transform.position).sqrMagnitude;

        bool aggro = distToPlayerSq < _aggroSq || _aggroTimer < aggroCooldown;

        if (aggro)
        {
            _aggroTimer = 0;

            if (distToPlayerSq <= _stopSq)
            {
                InRange.Invoke();
                _rb.velocity = Vector3.SmoothDamp(_rb.velocity, Vector3.zero, ref _velocity, acceleration * 2);
            }
            else
            {
                _dirToMove = Vector3.RotateTowards(_dirToMove, (_player.position + (_rnd.Next() > 0.5f ? _player.right : -_player.right) - transform.position).normalized,
                    turnSpeed, 0);
                _rb.velocity = Vector3.SmoothDamp(_rb.velocity, _dirToMove * speed, ref _velocity, acceleration);
            }
        }

        _aggroTimer += Time.fixedDeltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
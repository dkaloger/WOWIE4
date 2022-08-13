using System;
using DG.Tweening;
using UnityEngine;


public class AttackToken
{   
    private const float Cooldown = 1f;

    public event Action<AttackToken> Steal;

    public MonoBehaviour BelongsTo;

    public int CurrentPriority = 0;
    private int id;
    
    private static int _id;

    public AttackToken()
    {
        id = _id++;
    }

    public void Checkout(MonoBehaviour obj, int priority)
    {
        CurrentPriority = priority;
        BelongsTo = obj;
    }
    
    public void StealToken()
    {
        Steal?.Invoke(this);
    }

    public void Return(Action onReturn)
    {
        DOVirtual.DelayedCall(Cooldown, () =>
        {
            BelongsTo = null;
            onReturn?.Invoke();
        }, false);
    }
}
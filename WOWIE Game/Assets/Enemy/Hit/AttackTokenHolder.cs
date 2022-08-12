using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu]
public class AttackTokenHolder : ScriptableObject
{
    [NonSerialized] private List<AttackToken> _tokens;
    [NonSerialized] private Stack<AttackToken> _availableTokens;

    [SerializeField] private int numTokensInPool = 10;


    private void InitTokens()
    {
        _tokens = new List<AttackToken>(numTokensInPool);
        _availableTokens = new Stack<AttackToken>(numTokensInPool);
        for (int i = 0; i < numTokensInPool; i++)
            AddNewTokenToPool();
    }
    
    
    [CanBeNull]
    public AttackToken RequestToken(MonoBehaviour belongsTo, int priority)
    {
        if (_tokens == null || _tokens.Any(t => t == null))
            InitTokens();
        
        AttackToken toReturn = null;
        if (_availableTokens.Any()) 
            toReturn = _availableTokens.Pop();

        if (toReturn == null)
        {
            toReturn = _tokens.FirstOrDefault(t => t.CurrentPriority < priority);
            toReturn?.StealToken();
        }

        toReturn?.Checkout(belongsTo, priority);
        return toReturn;
    }

    public void ReturnToken(AttackToken token)
    {
        token.Return(() =>
        {
            _availableTokens.Push(token);
        });
    }

    public void AddNewTokenToPool()
    {
        _tokens.Add(new AttackToken());
        
        _availableTokens.Push(new AttackToken());
    }

    public void ResetTokens()
    { 
        if (_tokens == null || _tokens.Any(t => t == null))
            InitTokens();
        
        _tokens.Clear();
        _availableTokens.Clear();
    }
}
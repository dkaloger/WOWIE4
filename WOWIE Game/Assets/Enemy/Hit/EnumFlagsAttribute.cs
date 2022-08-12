using System;
using UnityEngine;

/// <summary>
/// Attribute for drawing enum flags with a mask dropdown, like Unity's LayerMask
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute()
    {
    }
}
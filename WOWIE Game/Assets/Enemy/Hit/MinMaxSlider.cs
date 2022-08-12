using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class SliderAttribute : PropertyAttribute
{
    public readonly float Min;
    public readonly float Max;

    public SliderAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

[Serializable]
public class MinMaxSlider
{
    public float Min;
    public float Max;

    public float GetLerpedValue(float t)
    {
        return Mathf.Lerp(Min, Max, t);
    }

    public float GetInverseLerpedValue(float t)
    {
        return Mathf.InverseLerp(Min, Max, t);
    }

    public float GetRandomValue(Squirrel3 rnd)
    {
        return rnd.Range(Min, Max);
    }
}
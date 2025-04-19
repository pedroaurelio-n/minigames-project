using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomProvider
{
    float Value { get; }
    Vector2 InsideCircle { get; }
    Vector3 InsideSphere { get; }
    Color Color { get; }

    int Range (int min, int max);
    float Range (float min, float max);
    Vector2 Range (Vector2 min, Vector2 max);
    Vector3 Range (Vector3 min, Vector3 max);

    bool GuaranteedChance (float chance, ref int failureCount);
    bool IncreasingChance (
        float baseChance,
        float increasePerFailure,
        ref int failureCount,
        float? maxChance = null
    );

    T PickRandom<T> (T[] array);
    T PickRandom<T> (List<T> list);
    T WeightedRandom<T> (List<WeightedObject<T>> weightedList);
    T RandomEnumValue<T> () where T : Enum;
}
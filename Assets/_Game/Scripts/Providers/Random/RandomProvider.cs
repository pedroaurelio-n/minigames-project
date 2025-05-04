using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomProvider : IRandomProvider
{
    public float Value => Random.value;
    public Vector2 InsideCircle => Random.insideUnitCircle;
    public Vector3 InsideSphere => Random.insideUnitSphere;

    public Color Color
    {
        get
        {
            Color randomColor = Random.ColorHSV();
            randomColor.a = 1f;
            return randomColor;
        }
    }

    /// <summary>
    /// Max is exclusive.
    /// </summary>
    public int Range (int min, int max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// Max is inclusive.
    /// </summary>
    public float Range (float min, float max)
    {
        return Random.Range(min, max);
    }

    public Vector2 Range (Vector2 min, Vector2 max)
    {
        return new Vector2(Range(min.x, max.x), Range(min.y, max.y));
    }

    public Vector3 Range (Vector3 min, Vector3 max)
    {
        return new Vector3(Range(min.x, max.x), Range(min.y, max.y), Range(min.z, max.z));
    }

    /// <summary>
    /// Returns true with the given chance. Guarantees success after enough failures.
    /// </summary>
    /// <param name="chance">Value between 0 and 1 (e.g., 0.1 for 10%)</param>
    /// <param name="failureCount">A reference to the number of consecutive failures</param>
    public bool GuaranteedChance (float chance, ref int failureCount)
    {
        if (chance <= 0f)
            return false;
        if (chance >= 1f)
            return true;
        
        int maxFailures = Mathf.CeilToInt(1f / chance);
        if (failureCount >= maxFailures || Range(0f, 1f) < chance)
        {
            failureCount = 0;
            return true;
        }

        failureCount++;
        return false;
    }

    /// <summary>
    /// Returns true with a chance that increases with each failure.
    /// Resets on success.
    /// </summary>
    /// <param name="baseChance">Base probability between 0 and 1 (e.g., 0.1 for 10%)</param>
    /// <param name="increasePerFailure">Amount to add to the chance after each failure between 0 and 1</param>
    /// <param name="failureCount">Reference to the number of consecutive failures</param>
    public bool IncreasingChance (
        float baseChance,
        float increasePerFailure,
        ref int failureCount,
        float? maxChance = null
    )
    {
        if (baseChance <= 0f)
            return false;
        if (baseChance >= 1f)
            return true;
        
        float currentChance = baseChance + (increasePerFailure * failureCount);

        currentChance = maxChance.HasValue
            ? Mathf.Clamp(currentChance, 0f, maxChance.Value)
            : Mathf.Clamp01(currentChance);

        if (Range(0f, 1f) < currentChance)
        {
            failureCount = 0;
            return true;
        }
        
        failureCount++;
        return false;
    }
    
    public T PickRandom<T> (IReadOnlyList<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("List must not be null or empty.");
        
        return list[Range(0, list.Count)];
    }
    
    //TODO pedro: Check for optimization possibilities
    public T PickRandom<T> (HashSet<T> hashset)
    {
        if (hashset == null || hashset.Count == 0)
            throw new ArgumentException("Hashset must not be null or empty.");
        
        int index = Random.Range(0, hashset.Count);
        return hashset.ElementAt(index);
    }

    public T WeightedRandom<T> (List<WeightedObject<T>> weightedList)
    {
        if (weightedList == null || weightedList.Count == 0)
            throw new ArgumentException("The weighted list must not be null or empty.");
        
        //TODO pedro: Check for optimization possibilities
        float totalWeight = 0;
        foreach (WeightedObject<T> weightObject in weightedList)
            totalWeight += weightObject.Weight;

        if (totalWeight <= 0)
            throw new InvalidOperationException("Total weight in the submitted list must be greater than zero.");
        
        float randomNumber = Range(0, totalWeight);
        float summedWeight = 0;
        
        foreach (WeightedObject<T> weightObject in weightedList)
        {
            summedWeight += weightObject.Weight;
            if (randomNumber <= summedWeight)
            {
                return weightObject.Obj;
            }
        }
        
        throw new InvalidOperationException("Weighted random selection failed.");
    }

    public T RandomEnumValue<T> () where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Range(0, values.Length));
    }
}

using System;
using UnityEngine;

[Serializable]
public class SynthesisChance
{
    [Header("Current Rare")]
    public ItemRare fromRare;

    [Header("Result Rare")]
    public ItemRare toRare;

    [Range(0f,100f)]
    public float chance;
}
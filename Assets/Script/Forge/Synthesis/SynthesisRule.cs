using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Synthesis Rule",
    menuName ="Game/Synthesis Rule")]
public class SynthesisRule : ScriptableObject
{
    [Header("Merge Requirement")]
    public int requiredItemAmount = 9;

    [Header("Rare Chances")]
    public List<SynthesisChance> chances = new();
}
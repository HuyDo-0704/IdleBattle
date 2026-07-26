using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Synthesis Rule",
    menuName ="Game/Synthesis Rule")]
public class MergeRule : ScriptableObject
{
    [Header("Merge Requirement")]
    public int requiredItemAmount = 9;

    [Header("Rare Chances")]
    public List<MergeChance> chances = new();
}
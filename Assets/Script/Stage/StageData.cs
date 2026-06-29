using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage")]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public List<EnemyLineupData> enemies;
    [Header("Reward")]
    public int expReward;
    public int GoldReward;
    public List<ItemReward> items;
    [Header("Star Condition")]

    public int requireAliveCharacter = 3;

    [Range(0,100)]
    public int requireRemainHPPercent = 70;

    public int requireMaxRound = 10;
    
}
[System.Serializable]
public class EnemyLineupData
{
    public Position position;
    public Character character;
}
[System.Serializable]
public class ItemReward
{
    public ItemData itemData;
    public ItemRare itemRare;
}
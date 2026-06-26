using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage")]
public class StageData : ScriptableObject
{
    public string stageName;
    public List<EnemyLineupData> enemies;
    public List<ItemReward> items;
    
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
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage")]
public class StageData : ScriptableObject
{
    public string stageName;
    public List<EnemyLineupData> enemies;
}
[System.Serializable]
public class EnemyLineupData
{
    public Position position;
    public Character character;
}
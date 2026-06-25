using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

}
[System.Serializable]
public class Region
{
    public List<Stage> stages = new List<Stage>();
    
}
public enum StatusStage
{
    Locked,
    Unlocked,
    Completed
}
[System.Serializable]

    public class Stage
{
    public StageData stageData;
    public StatusStage status;
    public int StarsEarned;
}

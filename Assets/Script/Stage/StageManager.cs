using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    // Start is called before the first execution of Update after the MonoBehaviour is created
    public StageInfo stageInfo;

    // Update is called once per frame
    void Update()
    {
        
    }
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

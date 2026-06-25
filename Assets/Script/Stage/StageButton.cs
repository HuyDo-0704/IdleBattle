using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public StageData stageData;
    public void ShowStageInfo()
    {
        StageInfo.Instance.UpdateInfoStage(stageData);
    }
}
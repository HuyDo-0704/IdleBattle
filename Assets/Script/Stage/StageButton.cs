using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public StageData stageData;
    public void showStageInfo()
    {
        StageInfo.Instance.UpdateInfoStage(stageData);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public StageData stageData;

    [Header("UI")]
    [SerializeField] private Image[] starImages; // Size = 3

    public void RefreshUI()
    {
        StageSaveData save = StageManager.Instance.stageSaves
            .Find(x => x.stageID == stageData.stageID);

        int starCount = save != null ? save.stars : 0;

        UpdateStars(starCount);
    }

    private void UpdateStars(int starCount)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            StarType type = i < starCount
                ? StarType.Yellow
                : StarType.Empty;

            starImages[i].sprite = DataManager.Instance.DataGame.GetSpriteStar(type);
        }
    }

    public void ShowStageInfo()
    {
        StageInfo.Instance.UpdateInfoStage(stageData);
    }
}
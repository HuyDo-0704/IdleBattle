using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public List<Chapter> chapters =new();

    public List<StageSaveData> stageSaves = new();

    public int CurrentUnlockedStageID; // Stage mở khóa hiện tại 
    // 
    private string SavePath => Path.Combine(Application.persistentDataPath,"StageSave.json");

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadJson();
    }
    public void CompleteStage(StageData stage)
    {
        int stars = StarCalculator.CalculateStars(stage);

        SaveStage(stage.stageID, stars);

        Debug.Log($"Stage {stage.stageName} Clear - {stars} Stars");
    }
    private void SaveStage(int stageID, int stars)
    {
        StageSaveData data =
            stageSaves.Find(x => x.stageID == stageID);

        if (data == null)
        {
            data = new StageSaveData
            {
                stageID = stageID
            };

            stageSaves.Add(data);
        }

        data.status = StatusStage.Completed;

        // Chỉ cập nhật nếu cao hơn
        data.stars = Mathf.Max(data.stars, stars);

        CurrentUnlockedStageID = Mathf.Max(CurrentUnlockedStageID, stageID + 1);
        int chapterIndex = GetChapterIndex(stageID);
        RefreshChapter(chapterIndex);
        SaveJson();
    }
    //================ SAVE =================

    public void SaveJson()
    {
        StageSaveFile file = new StageSaveFile();

        file.currentStageID = CurrentUnlockedStageID;
        file.stageSaves = stageSaves;

        string json =
            JsonUtility.ToJson(file, true);

        File.WriteAllText(SavePath, json);

        Debug.Log("Stage Saved");
    }

    //================ LOAD =================

    public void LoadJson()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No Stage Save");

            stageSaves = new List<StageSaveData>();
            CurrentUnlockedStageID = 0;
            RefreshAllChapters();
            return;
        }

        string json = File.ReadAllText(SavePath);

        StageSaveFile file =
            JsonUtility.FromJson<StageSaveFile>(json);

        if (file != null)
        {
            stageSaves = file.stageSaves ?? new List<StageSaveData>();
            CurrentUnlockedStageID = file.currentStageID;
        }

        Debug.Log("Stage Loaded");
    }

    // DELETED SAVE 
    public void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Stage Save Deleted");
        }

        stageSaves.Clear();
        CurrentUnlockedStageID = 0;

        RefreshAllChapters();

    }
    // CHAPTER
    private int GetChapterIndex(int stageID)
    {
        for (int i = 0; i < chapters.Count; i++)
        {
            foreach (StageButton stage in chapters[i].stages)
            {
                if (stage.stageData.stageID == stageID)
                    return i;
            }
        }

        return -1;
    }
    public void RefreshChapter(int chapterIndex)
    {
        if (chapterIndex < 0 || chapterIndex >= chapters.Count)
            return;

        Chapter chapter = chapters[chapterIndex];

        bool unlocked = false;

        foreach (StageButton stage in chapter.stages)
        {
            stage.RefreshUI();

            if (stage.stageData.stageID <= CurrentUnlockedStageID)
                unlocked = true;
        }

        chapter.IsUnlocked = unlocked;
    }
    public void RefreshAllChapters()
    {
        for (int i = 0; i < chapters.Count; i++)
        {
            RefreshChapter(i);
        }
    }
}

[System.Serializable]
public class Chapter
{
    public List<StageButton> stages = new List<StageButton>();
    public bool IsUnlocked;
    
}
public enum StatusStage
{
    Locked,
    Unlocked,
    Completed
}
[System.Serializable]
public class StageSaveFile
{
    public int currentStageID;

    public List<StageSaveData> stageSaves = new();
}
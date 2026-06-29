using UnityEngine;

[System.Serializable]
public class Character
{
    public CurrentStats currentStats; // bảng chỉ số 
    public float PowerStats;
    [Header("Progress")]
    public int CurrentLevel = 1;
    public int CurrentExp;
    public int star;
    //
    public bool isLineup;
    // Cộng EXP 
    public void AddExp(int exp)
    {
        CurrentExp += exp;
        Debug.Log($"{currentStats.baseStats.characterName} add {exp}");
        while (CurrentExp >= GetMaxExp())
        {
            CurrentExp -= GetMaxExp();
            LevelUp();
        }

        // CharacterInventoryManager.Instance.SaveInventory(); cái này thêm sau 
    }
    // lên cấp 
    private void LevelUp()
    {
        CurrentLevel++;

        // Cập nhật lại chỉ số
        currentStats.InitializeStats(CurrentLevel);

        Debug.Log($"{currentStats.baseStats.characterName} Level Up -> {CurrentLevel}"); // về sau sẽ xóa 
    }
    public int GetMaxExp()
    {
        return 100 + (CurrentLevel - 1) * 20;
    }
    public float GetExpPercent()
    {
        return (float)CurrentExp / GetMaxExp();
    }
}
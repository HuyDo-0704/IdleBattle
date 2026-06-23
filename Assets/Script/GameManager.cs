using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Character> playersTeam = new List<Character>();

    public float CurrentPowerTeam;
    public float LevelPlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RefreshLineup();
    }

    // ⬇ Lấy team từ Inventory và khởi tạo
    public void RefreshLineup()
    {
        playersTeam = CharacterInventory.Instance.GetLineupCharacters();

        foreach (var c in playersTeam)
            c.currentStats.InitializeStats();

        UpdateTeamPower();
    }

    // ⬇ Tính tổng Power
    public void UpdateTeamPower()
    {
        CurrentPowerTeam = 0;

        foreach (var c in playersTeam)
            CurrentPowerTeam += c.currentStats.PowerStats;
    }

    // ⬇ Thêm tướng vào đội hình
    public void SetLineup(Character character, bool value)
    {
        character.isLineup = value;
        RefreshLineup();
    }
    // setup enemie stage 
}

[System.Serializable]
public class Character
{
    public CurrentStats currentStats;
    public int star;
    public bool isLineup;
    
}




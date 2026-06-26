using UnityEngine;

public enum PanelType
{
    Home,
    Battle
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float CurrentPowerTeam;
    public int LevelPlayer;
    [Header("Panels")]
    [SerializeField] GameObject HomePanel;
    [SerializeField] GameObject BattlePanel;
    public GameObject ChangeScene; // về sau để private hoặc tách ra 1 script riêng để quản lý UI chuyển cảnh

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
        // Khởi tạo chỉ số của tất cả nhân vật trong túi đồ
        CharacterInventoryManager.Instance.InitializeAllCharacters(); 
        // Cập nhật sức mạnh đội hình
        UpdateTeamPower();
    }

    public void UpdateTeamPower()
    {
        CurrentPowerTeam = 0;

        foreach (var lineupPos in Lineup.Instance.myLineup)
        {
            if (lineupPos.CharIndex < 0 ||
                lineupPos.CharIndex >= CharacterInventoryManager.Instance.ownedCharacters.Count)
                continue;

            Character character =
                CharacterInventoryManager.Instance.ownedCharacters[lineupPos.CharIndex];

            if (character == null)
                continue;

            CurrentPowerTeam +=
                character.currentStats.PowerStats;
        }
    }
    // Hàm để Chuyển đổi Panel
    public void ChangePanel(PanelType panel)
    {
        HomePanel.SetActive(panel == PanelType.Home);
        BattlePanel.SetActive(panel == PanelType.Battle);
    }
}

[System.Serializable]
public class Character
{
    public CurrentStats currentStats;
    public int star;
    public bool isLineup;
    
}




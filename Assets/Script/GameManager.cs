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
    public Character CreateCharacter(string characterID, int level = 1, int star = 1, string uid = null, bool isLineup = false, CharacterEquipment equipments = null)
    {
        CharacterBaseStats baseStats =
            DataManager.Instance.DataGame.GetCharacter(characterID);

        if (baseStats == null)
        {
            Debug.LogError($"Character ID '{characterID}' not found!");
            return null;
        }

        Character character = new Character();

        character.uid = string.IsNullOrEmpty(uid)
            ? System.Guid.NewGuid().ToString()
            : uid;

        character.CurrentLevel = level;
        character.star = star;
        character.isLineup = isLineup;

        character.currentStats = new CurrentStats();
        character.currentStats.baseStats = baseStats;
        character.currentStats.equipmentStats = new Stats();
        character.currentStats.FinalStats = new Stats();

        character.equipments = equipments ?? new CharacterEquipment();

        character.currentStats.RecalculateStats(level);

        return character;
    }
}





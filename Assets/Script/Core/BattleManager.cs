using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public TurnManager turnManager;
    public List<PositionSpawn> spawnPositionsPlayer;
    public List<PositionSpawn> spawnPositionsEnemy;
    public BattleTeam enemyTeam;
    public BattleTeam playerTeam;

    private void Awake()
    {
        Instance = this;
    }
    // hàm để bắt đầu trận đấu
    public void StartBattle(StageData stageData)
    {
        StartCoroutine(StartBattleRoutine(stageData));
    }

    private IEnumerator StartBattleRoutine(StageData stageData)
    {
        Animator animator = GameManager.Instance.ChangeScene.GetComponent<Animator>();

        // Đóng màn hình
        animator.gameObject.SetActive(true);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);

        // Chuyển sang Battle UI
        GameManager.Instance.ChangePanel(PanelType.Battle);

        // Spawn toàn bộ nhân vật
        SpawnEnemyTeam(stageData.enemies);
        SpawnCharactersPlayerInBattle();

        // Đợi Unity hoàn thành việc khởi tạo object (Awake/Start)
        yield return null;
        yield return new WaitForEndOfFrame();

        // Mở màn hình
        animator.SetTrigger("Open");

        // Đợi animation mở kết thúc
        yield return new WaitForSeconds(3f);

        Debug.Log("=== BATTLE START ===");

        turnManager.SetUp();
    }
    // thêm hàm để spawn tướng lên sân đấu dựa theo line up của người chơi 
    public void SpawnCharactersPlayerInBattle()
    {
        playerTeam.members.Clear();

        foreach (PositionInLineup slot in Lineup.Instance.myLineup)
        {
            if (slot.CharIndex < 0 ||
                slot.CharIndex >= CharacterInventoryManager.Instance.ownedCharacters.Count)
            {
                Debug.LogWarning($"Invalid CharIndex {slot.CharIndex}");
                continue;
            }

            Character character =
                CharacterInventoryManager.Instance.ownedCharacters[slot.CharIndex];

            PositionSpawn spawnData =
                GetPlayerSpawnPoint(slot.position);

            if (spawnData == null)
            {
                Debug.LogError($"Missing spawn point for {slot.position}");
                continue;
            }

            GameObject obj = 
                Instantiate
                (
                    character.currentStats.baseStats.characterPrefab,
                    spawnData.spawnPoint.transform
                );

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 100f;

            CharacterManager manager =
                obj.GetComponent<CharacterManager>();

            manager.SetupCharacter(character);
            manager.Hud.SetTeam(TypeTeam.Player);
            playerTeam.members.Add(manager);

            Debug.Log(
                $"Spawn {character.currentStats.baseStats.characterName} at {slot.position}");
        }
    }
    public void SpawnEnemyTeam(List<EnemyLineupData> enemyLineup)
    {
        enemyTeam.members.Clear();

        foreach (EnemyLineupData slot in enemyLineup)
        {
            PositionSpawn spawnData =
                GetEnemySpawnPoint(slot.position);

            if (spawnData == null)
            {
                Debug.LogError(
                    $"Missing enemy spawn point for {slot.position}");
                continue;
            }

            if (slot.character == null)
                continue;

            GameObject obj =
                Instantiate(
                    slot.character.currentStats.baseStats.characterPrefab,
                    spawnData.spawnPoint.transform);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 100f;

            CharacterManager manager =
                obj.GetComponent<CharacterManager>();

            manager.SetupCharacter(slot.character);

            manager.Hud.SetTeam(TypeTeam.Enemy);

            enemyTeam.members.Add(manager);

            Debug.Log(
                $"Spawn Enemy {slot.character.currentStats.baseStats.characterName} at {slot.position}");
        }
    }
    // hàm để lấy vị trí spawn của tướng dựa theo position
    private PositionSpawn GetEnemySpawnPoint(Position position)
    {
        return spawnPositionsEnemy.Find(
            x => x.position == position);
    }
    private PositionSpawn GetPlayerSpawnPoint(Position position)
    {
        return spawnPositionsPlayer.Find(x => x.position == position);
    }
    public BattleTeam GetEnemyTeam(
        CharacterManager character)
    {
        if (playerTeam.members.Contains(character))
            return enemyTeam;

        return playerTeam;
    }

    public bool CheckBattleEnd()
    {
        if (!playerTeam.HasAliveMember())
            return true;

        if (!enemyTeam.HasAliveMember())
            return true;

        return false;
    }
}

[System.Serializable]
public class PositionSpawn
{
    public Position position;
    public GameObject spawnPoint;
}
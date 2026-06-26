using UnityEngine;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public static StageInfo Instance;
    
    [SerializeField] private TMP_Text stageNameText;
    // Info Enemy Stage
    [SerializeField] private GameObject EnemyContainer; // nơi chứa hình ảnh các enemy
    [SerializeField] private GameObject EnemyPrefabs; // prefab của enemy
    [SerializeField] private RewardPanel rewardPanel;
    private Animator animator;
    private StageData CurrentStage;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        animator = GetComponent<Animator>();
        Instance = this;
    }
    public void UpdateInfoStage(StageData data)
    {
        CurrentStage = data;
        animator.SetTrigger("Show");
        stageNameText.text = CurrentStage.stageName;
        rewardPanel.ShowReward(CurrentStage.items);
        SpawnEnemy();

    }
    public void StartStage()
    {
        if (CurrentStage != null)
        {
            BattleManager.Instance.StartBattle(CurrentStage);
        }
    }
    private void SpawnEnemy()
    {
        // Xóa enemy cũ
        foreach (Transform child in EnemyContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn enemy mới
        foreach (EnemyLineupData enemy in CurrentStage.enemies)
        {
            if (enemy.character == null)
                continue;

            GameObject obj =
                Instantiate(
                    EnemyPrefabs,
                    EnemyContainer.transform);

            EnemyStageUI ui =
                obj.GetComponent<EnemyStageUI>();

            ui.UpdateEnemyUI(enemy.character);
        }
    }
}

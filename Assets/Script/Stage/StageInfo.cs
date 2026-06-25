using UnityEngine;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public static StageInfo Instance;
    
    [SerializeField] private TMP_Text stageNameText;
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
    }
    public void StartStage()
    {
        if (CurrentStage != null)
        {
            BattleManager.Instance.StartBattle(CurrentStage);
        }
    }
}

using UnityEngine;
using TMPro;
public class StageInfo : MonoBehaviour
{
    public static StageInfo Instance;
    
    [SerializeField] private TMP_Text stageNameText;
    private Animator animator;
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
        animator.SetTrigger("Show");
        stageNameText.text = data.stageName;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResultUI : MonoBehaviour
{
    public static PanelResultUI Instance;

    public enum result
    {
        win,
        lose
    }
    [SerializeField] private GameObject BG;
    [SerializeField] private Sprite VictoryPanel;
    [SerializeField] private Sprite DefeatPanel;

    [SerializeField] private RewardPanel rewardPanel;
    [Header("Exp Panel UI")]
    [SerializeField] GameObject CharPrefabs;
    [SerializeField] GameObject container;
    private void Awake()
    {
        Instance = this;
        BG.SetActive(false);
    }

    public void UpdatePanelUI(result result,
        List<ItemReward> rewards)
    {
        gameObject.SetActive(true);

        switch (result)
        {
            case result.win:
                Victory(rewards);
                break;

            case result.lose:
                Defeat();
                break;
        }
    }
    private void ShowCharacterExp(int addExp)
    {
        // Xóa UI cũ
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        // Spawn UI cho từng nhân vật
        foreach (Character character in CharacterInventoryManager.Instance.ownedCharacters)
        {
            GameObject obj = Instantiate(CharPrefabs, container.transform);

            CharacterExpUI ui = obj.GetComponent<CharacterExpUI>();

            ui.Setup(character);

            StartCoroutine(ui.PlayExpAnimation(addExp));
        }
    }
    private void Victory(List<ItemReward> rewards)
    {
        BG.SetActive(true);

        BG.GetComponent<Image>().sprite = VictoryPanel;
        ShowCharacterExp(BattleManager.Instance.CurrentStage.expReward);
        rewardPanel.ShowReward(rewards);
    }

    private void Defeat()
    {
        BG.SetActive(true);

        BG.GetComponent<Image>().sprite = DefeatPanel;

    }

    public void HidePanel()
    {
        GameManager.Instance.ChangePanel(PanelType.Home);
        gameObject.SetActive(false);
    }
}
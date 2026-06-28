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

    private void Victory(List<ItemReward> rewards)
    {
        BG.SetActive(true);

        BG.GetComponent<Image>().sprite = VictoryPanel;

        rewardPanel.ShowReward(rewards);
    }

    private void Defeat()
    {
        BG.SetActive(true);

        BG.GetComponent<Image>().sprite = DefeatPanel;

        rewardPanel.Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
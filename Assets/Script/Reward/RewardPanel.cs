using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{

    [SerializeField] private Transform content;
    [SerializeField] private ItemRewardUI itemPrefab;


    public void ShowReward(List<ItemReward> rewards)
    {
        gameObject.SetActive(true);

        Clear();

        foreach (ItemReward item in rewards)
        {
            ItemRewardUI ui =
                Instantiate(itemPrefab, content);

            ui.Setup(item.itemData, item.itemRare);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Clear()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);
    }
}
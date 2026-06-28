using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    private readonly Dictionary<ItemRare, float> dropRate =
    new Dictionary<ItemRare, float>()
    {
        { ItemRare.Common, 65f },
        { ItemRare.Rare, 25f },
        { ItemRare.Epic, 8f },
        { ItemRare.Legendary, 1.8f },
        { ItemRare.Mythic, 0.2f }
    };
    public void Awake()
    {
        Instance = this;
    }
    public List<ItemReward> GiveStageReward(StageData stage)
    {
        List<ItemReward> rewards = new List<ItemReward>();

        foreach (ItemReward reward in stage.items)
        {
            float chance = GetDropChance(reward.itemRare);

            if (Random.Range(0f, 100f) > chance)
                continue;

            ItemInventoryManager.Instance.AddItem(
                reward.itemData,
                reward.itemRare);

            rewards.Add(reward);

            Debug.Log($"Reward : {reward.itemData.itemName}");
        }

        return rewards;
    }
    // hàm lấy tỉ lệ rơi item
    private float GetDropChance(ItemRare rare)
    {
        return dropRate[rare];
    }
}

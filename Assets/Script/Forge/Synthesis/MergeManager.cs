using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;

    [SerializeField] private MergeRule synthesisRule;
    [SerializeField] MergePanel synthesisPanel;

    public List<EquipmentItem> mergeItems = new();
    public int RequiredItemCount => 9;
    private void Awake()
    {
        Instance = this;
    }

    public bool AddItem(EquipmentItem item)
    {
        if (item == null)
        {
            Debug.Log("Item is null.");
            return false;
        }

        if (mergeItems.Count >= synthesisRule.requiredItemAmount)
        {
            Debug.Log("Merge slots are full.");
            return false;
        }

        // Nếu không phải item đầu tiên thì phải cùng rarity
        if (mergeItems.Count > 0)
        {
            ItemRare firstRare = mergeItems[0].Data.defaultRare;

            if (item.Data.defaultRare != firstRare)
            {
                Debug.Log("Not same rarity.");
                return false;
            }
        }

        mergeItems.Add(item);

        Debug.Log($"Add Item : {item.Data.itemName} | Rare : {item.Data.defaultRare} ({mergeItems.Count}/{synthesisRule.requiredItemAmount})");

        synthesisPanel.Refresh();

        return true;
    }
    public bool RemoveItem(EquipmentItem item)
    {
        if (item == null)
            return false;

        if (!mergeItems.Remove(item))
            return false;

        synthesisPanel.Refresh();

        return true;
    }
    public void Clear()
    {
        mergeItems.Clear();
        synthesisPanel.Refresh();

        Debug.Log("Merge slots cleared.");
    }

    public void Merge()
    {
        if (mergeItems.Count != synthesisRule.requiredItemAmount)
        {
            Debug.Log("Need 9 items.");
            return;
        }

        ItemRare currentRare = mergeItems[0].Data.defaultRare;

        // Chỉ kiểm tra cùng Rare
        if (mergeItems.Any(x => x.Data.defaultRare != currentRare))
        {
            Debug.Log("Items must have same rare.");
            return;
        }

        ItemRare resultRare = RollRare(currentRare);

        // Fail
        if (resultRare == currentRare)
        {
            Debug.Log("Merge Failed.");

            foreach (var item in mergeItems)
                ItemInventoryManager.Instance.RemoveItem(item.uid);

            mergeItems.Clear();
            synthesisPanel.ResetUI();
            return;
        }

        List<EquipmentData> candidates =
        DataManager.Instance.DataGame.itemDatabase.Equipment.Where(x => x.defaultRare == resultRare).ToList();

        if (candidates.Count == 0)
        {
            Debug.LogError($"No Equipment with Rare : {resultRare}");
            return;
        }

        EquipmentData randomEquipment =
            candidates[Random.Range(0, candidates.Count)];

        foreach (var item in mergeItems)
            ItemInventoryManager.Instance.RemoveItem(item.uid);

        ItemInventoryManager.Instance.AddItem(randomEquipment);

        mergeItems.Clear();
        // reset Inventory
        synthesisPanel.ResetUI();
        synthesisPanel.ShowMergeResult(randomEquipment);
        Debug.Log($"Merge Success : {resultRare}");
    }

    private ItemRare RollRare(ItemRare currentRare)
    {
        List<MergeChance> chances =
            synthesisRule.chances
            .Where(x => x.fromRare == currentRare)
            .OrderByDescending(x => (int)x.toRare)
            .ToList();

        float roll = Random.Range(0f, 100f);

        foreach (var chance in chances)
        {
            if (roll <= chance.chance)
                return chance.toRare;
        }

        return currentRare;
    }
}
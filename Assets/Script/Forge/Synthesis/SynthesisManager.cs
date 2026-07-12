using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynthesisManager : MonoBehaviour
{
    public static SynthesisManager Instance;

    [SerializeField] private SynthesisRule synthesisRule;
    [SerializeField] SynthesisPanel synthesisPanel;

    public List<EquipmentItem> mergeItems = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(EquipmentItem item)
    {
        if (mergeItems.Count >= synthesisRule.requiredItemAmount)
            return;

        mergeItems.Add(item);
        synthesisPanel.Refresh();
    }

    public void Clear()
    {
        mergeItems.Clear();
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

        Debug.Log($"Merge Success : {resultRare}");
    }

    private ItemRare RollRare(ItemRare currentRare)
    {
        List<SynthesisChance> chances =
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
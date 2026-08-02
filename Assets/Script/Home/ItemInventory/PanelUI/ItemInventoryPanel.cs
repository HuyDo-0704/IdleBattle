using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventoryPanel : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemInventoryPrefab prefab;

    private readonly List<ItemInventoryPrefab> items = new();

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (var i in items)
            Destroy(i.gameObject);

        items.Clear();

        // Spawn từ rare cao -> thấp
        var sortedItems = ItemInventoryManager.Instance.ownedItems.OrderByDescending(item =>DataManager.Instance.DataGame.itemDatabase.GetItem(item.itemID).defaultRare);

        foreach (Item item in sortedItems)
        {
            ItemInventoryPrefab ui = Instantiate(prefab, content);
            ui.Setup(item);
            items.Add(ui);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryPanel : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemInventoryPrefab prefab;

    private readonly List<ItemInventoryPrefab> items =
        new();

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (var i in items)
            Destroy(i.gameObject);

        items.Clear();

        foreach (Item item in ItemInventoryManager.Instance.ownedItems)
        {
            ItemInventoryPrefab ui =
                Instantiate(prefab, content);

            ui.Setup(item);

            items.Add(ui);
        }
    }
}
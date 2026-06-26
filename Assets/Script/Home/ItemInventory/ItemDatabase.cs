using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
// tích hợp vào bên ScriptableObject DataGame để dễ quản lý
public class ItemDatabase
{
    public List<ItemData> items;

    private Dictionary<int, ItemData> cache;

    public ItemData GetItem(int id)
    {
        if (cache == null)
        {
            cache = new Dictionary<int, ItemData>();

            foreach (var item in items)
                cache[item.itemID] = item;
        }

        return cache[id];
    }
}
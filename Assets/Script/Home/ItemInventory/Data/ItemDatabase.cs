using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabase
{
    [Header("All Equipment")]
    public List<EquipmentData> Equipment;

    [Header("Other Items (Material, Consumable,...)")]
    public List<ConsumableData> Consumable;

    private Dictionary<int, EquipmentData> equipmentCache;
    private Dictionary<int, ItemData> itemCache;

    private void BuildCache()
    {
        if (equipmentCache == null)
        {
            equipmentCache = new Dictionary<int, EquipmentData>();

            foreach (var equipment in Equipment)
            {
                if (equipment != null)
                    equipmentCache[equipment.itemID] = equipment;
            }
        }

        if (itemCache == null)
        {
            itemCache = new Dictionary<int, ItemData>();

            foreach (var item in Consumable)
            {
                if (item != null)
                    itemCache[item.itemID] = item;
            }
        }
    }

    public EquipmentData GetEquipmentData(int id)
    {
        BuildCache();

        equipmentCache.TryGetValue(id, out EquipmentData equipment);
        return equipment;
    }

    public ItemData GetItem(int id)
    {
        BuildCache();

        // Ưu tiên tìm Equipment trước
        if (equipmentCache.TryGetValue(id, out EquipmentData equipment))
            return equipment;

        itemCache.TryGetValue(id, out ItemData item);
        return item;
    }
}
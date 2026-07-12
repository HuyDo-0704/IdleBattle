using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="Synthesis Database",
    menuName ="Game/Synthesis Database")]
public class SynthesisDatabase : ScriptableObject
{
    public SynthesisRule synthesisRule;

    public List<ItemData> availableItems = new();

    public List<ItemData> GetItemsByRare(ItemRare rare)
    {
        return availableItems.Where(x => x.defaultRare == rare).ToList();
    }

    public List<ItemData> GetItemsByType(EquipmentType type, ItemRare rare)
    {
        return availableItems
            .Where(x =>
            {
                if (x is EquipmentData equipment)
                {
                    return equipment.equipmentType == type &&
                           equipment.defaultRare == rare;
                }

                return false;
            })
            .ToList();
    }
}
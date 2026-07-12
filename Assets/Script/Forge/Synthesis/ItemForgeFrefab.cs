
using UnityEngine;
using UnityEngine.UI;

public class ItemForgeFrefab : MonoBehaviour
{
    public EquipmentItem equipmentItem;

    [SerializeField] private Image icon;

    public void Setup(EquipmentItem item)
    {
        equipmentItem = item;

        icon.sprite = item.Data.icons;
    }

    public void OnClick()
    {
        SynthesisManager.Instance.AddItem(equipmentItem);
    }
}


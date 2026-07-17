using UnityEngine;
using UnityEngine.UI;

public class ItemForgeFrefab : MonoBehaviour
{
    public EquipmentItem equipmentItem;

    [SerializeField] private Image icon;
    [SerializeField] private Image BG;
    [SerializeField] private GameObject selectImage;
    public void Setup(EquipmentItem item)
    {
        equipmentItem = item;

        icon.sprite = item.Data.icons;
        BG.color = DataManager.Instance.DataGame.GetColorByRarity(item.Data.defaultRare);

        UpdateSelectUI();
    }

    public void ToggleClick()
    {
        if (SynthesisManager.Instance.mergeItems.Contains(equipmentItem))
            SynthesisManager.Instance.RemoveItem(equipmentItem);
        else
            SynthesisManager.Instance.AddItem(equipmentItem);
    }

    private void UpdateSelectUI()
    {
        bool isSelected =
            SynthesisManager.Instance.mergeItems.Contains(equipmentItem);

        selectImage.SetActive(isSelected);
    }
    public void RefreshUI()
    {
        selectImage.SetActive(
            SynthesisManager.Instance.mergeItems.Contains(equipmentItem));
    }
}
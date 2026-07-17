using UnityEngine;
using UnityEngine.UI;

public class MergeSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] Image BG;
    Color defaultColor = Color.gray1 ;

    private EquipmentItem currentItem;

    public void SetItem(EquipmentItem item)
    {
        currentItem = item;

        if (item == null)
        {
            icon.enabled = false;
            icon.sprite = null;
            return;
        }

        icon.enabled = true;
        icon.sprite = item.Data.icons;
        BG.color = DataManager.Instance.DataGame.GetColorByRarity(item.Data.defaultRare);
    }

    public EquipmentItem GetItem()
    {
        return currentItem;
    }

    public void Clear()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        BG.color = defaultColor;
    }
    public void OnClickRemove()
    {
        if (currentItem == null)
            return;

        SynthesisManager.Instance.RemoveItem(currentItem);
    }
}
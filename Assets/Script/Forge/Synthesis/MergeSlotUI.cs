using UnityEngine;
using UnityEngine.UI;

public class MergeSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] Image BG;
    Color defaultColor = Color.gray1 ;

    private EquipmentItem currentItem;
    // cho item 
    public void SetItem(EquipmentItem item)
    {
        currentItem = item;

        if (item == null)
        {
            Clear();
            return;
        }

        SetVisual(item.Data.icons, item.Data.defaultRare);
    }
    // cho itemdata
    public void SetItem(EquipmentData data)
    {
        currentItem = null;

        if (data == null)
        {
            Clear();
            return;
        }

        SetVisual(data.icons, data.defaultRare);
    }
    // update hình ảnh 
    private void SetVisual(Sprite sprite, ItemRare rare)
    {
        icon.enabled = true;
        icon.sprite = sprite;
        BG.color = DataManager.Instance.DataGame.GetColorByRarity(rare);
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

        MergeManager.Instance.RemoveItem(currentItem);
    }
}
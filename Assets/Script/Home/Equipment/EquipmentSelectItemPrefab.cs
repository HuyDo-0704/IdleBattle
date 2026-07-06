using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectItemPrefab : MonoBehaviour
{
    public Image icon;
    public Image rarity;
    EquipmentItem item;
    public GameObject equippedIcon; 
    Character character;

    public void Setup(Character character, EquipmentItem equipment)
    {
        this.character = character;
        this.item = equipment;
        rarity.color = DataManager.Instance.DataGame.GetColorByRarity(item.itemRare);
        icon.sprite = equipment.Data.GetSpriteIcon(item.itemRare);
        if( equipment.isEquipped == true)
        {
            equippedIcon.SetActive(true);
        }
        else
        {
            equippedIcon.SetActive(false);
        }
    }

    public void OnClick()
    {
        EquipmentManager.Instance.Equip(character, item);

        EquipmentPanelUI panel = EquipmentManager.Instance.UI;

        panel.Refresh();

        EquipmentSelectPanel.Instance.Refresh();
        // đóng panel khi trang bị xong 
        EquipmentSelectPanel.Instance.Panel.SetActive(false);
    }
}

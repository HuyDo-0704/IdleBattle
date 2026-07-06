using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    public EquipmentType equipmentType;
    public GameObject EmtyUI;
    public Image icon;
    private Character currentCharacter;

    public void Refresh(Character character)
    {
        currentCharacter = character;

        EquipmentItem item =
            EquipmentManager.Instance.GetEquipment(character, equipmentType);

        if(item.Data == null)
        {
            EmtyUI.SetActive(false);
        }
        else
        {
            EmtyUI.SetActive(true);
            icon.sprite = item.Data.GetSpriteIcon(item.itemRare);
        }
    }

    public void OnClick()
    {
        EquipmentSelectPanel.Instance.Show(currentCharacter, equipmentType);
    }
}

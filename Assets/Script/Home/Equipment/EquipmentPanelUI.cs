using UnityEngine;

public class EquipmentPanelUI : MonoBehaviour
{
    public Character currentCharacter;

    public EquipmentSlotUI weapon;
    public EquipmentSlotUI helmet;
    public EquipmentSlotUI armor;
    public EquipmentSlotUI shoes;
    public EquipmentSlotUI ring;
    public EquipmentSlotUI necklace;

    public void Show(Character character)
    {
        currentCharacter = character;

        Refresh();
    }

    public void Refresh()
    {
        weapon.Refresh(currentCharacter);
        helmet.Refresh(currentCharacter);
        armor.Refresh(currentCharacter);
        shoes.Refresh(currentCharacter);
        ring.Refresh(currentCharacter);
        necklace.Refresh(currentCharacter);
        
    }
}
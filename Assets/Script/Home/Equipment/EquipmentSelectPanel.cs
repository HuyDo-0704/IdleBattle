using UnityEngine;

public class EquipmentSelectPanel : MonoBehaviour
{
    public static EquipmentSelectPanel Instance;
    public GameObject Panel;
    public Transform content;

    public EquipmentSelectItemPrefab prefab;

    private Character currentCharacter;

    private EquipmentType currentType;

    void Awake()
    {
        Instance = this;
        Panel.SetActive(false);
    }

    public void Show(Character character,EquipmentType type)
    {
        currentCharacter = character;
        currentType = type;

        Panel.SetActive(true);

        Refresh();
    }

    public void Refresh()
    {
        foreach(Transform child in content)
            Destroy(child.gameObject);

        foreach(Item item in ItemInventoryManager.Instance.ownedItems)
        {
            if(item is not EquipmentItem equipment)
                continue;

            if(equipment.Data.equipmentType != currentType)
                continue;

            Instantiate(prefab, content).Setup(currentCharacter, equipment);
        }
    }
}
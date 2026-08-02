
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryPrefab : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Image rarityFrame;
    [SerializeField] private GameObject equippedIcon;
    private Button button;

    private Item runtimeItem;
    private ItemData itemData;

    public void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Setup(Item item)
    {
        runtimeItem = item;

        itemData = DataManager.Instance.DataGame.itemDatabase.GetItem(item.itemID);

        icon.sprite = itemData.icons;

        rarityFrame.color =
            DataManager.Instance.DataGame.GetColorByRarity(itemData.defaultRare);


        equippedIcon.SetActive(item.isEquipped);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log(runtimeItem.GetType().Name);
        if (runtimeItem is EquipmentItem equipment)
        {
            ItemInfoUI.Instance.Show(equipment);
        }
        else
        {
            Debug.LogWarning("Item này chưa hỗ trợ hiển thị.");
        }
    }
}
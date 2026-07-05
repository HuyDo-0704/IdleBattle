using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class ItemInfoUI : MonoBehaviour
{
    public static ItemInfoUI Instance;
    [Header("Icon")]
    [SerializeField] private Image icon;
    [SerializeField] private Image rarityFrame;

    [Header("Info")]
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text descriptionText;

    [Header("Stats")]
    [SerializeField] private TMP_Text StatsText; // n

    [Header("Buttons")]
    [SerializeField] private Button equipButton;
    [SerializeField] private Button lockButton;
    private EquipmentItem currentItem;
    private void Awake()
    {
        Instance = this;
    }
    public void Show(Item item)
    {
        // gọi animation
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Show");
        gameObject.SetActive(true);

        switch (item)
        {
            case EquipmentItem equipment:
                UpdateInfoEquipmentItem(equipment);
                break;

            // case ConsumableItem consumable:
            //     UpdateInfoConsumableItem(consumable);
            //     break;

            // case MaterialItem material:
            //     UpdateInfoMaterialItem(material);
            //     break;

            default:
                Debug.LogWarning($"Không hỗ trợ Item: {item.GetType().Name}");
                break;
        }
    }
    public void UpdateInfoEquipmentItem(EquipmentItem item)
    {
        if (item == null)
            return;

        currentItem = item;

        EquipmentData data = DataManager.Instance.DataGame.itemDatabase.GetEquipmentData(item.itemID);

        if (data == null)
        {
            Debug.LogError($"EquipmentData ID {item.itemID} not found.");
            return;
        }

        //-------------------------
        // Icon
        //-------------------------

        icon.sprite = data.GetSpriteIcon(item.itemRare);

        rarityFrame.color =
            DataManager.Instance.DataGame.GetColorByRarity(item.itemRare);

        //-------------------------
        // Info
        //-------------------------

        itemName.text = data.itemName;


        levelText.text = $"Lv.{item.level}";

        typeText.text = data.equipmentType.ToString();

        descriptionText.text = data.description;

        //-------------------------
        // Stats
        //-------------------------
        // gộp các chỉ số thành một chuỗi duy nhất để hiển thị trong StatsText
        StringBuilder sb = new StringBuilder();

        if (data.hp > 0)
            sb.AppendLine($"HP: {data.hp}");

        if (data.atk > 0)
            sb.AppendLine($"ATK: {data.atk}");

        if (data.def > 0)
            sb.AppendLine($"DEF: {data.def}");

        if (data.speed > 0)
            sb.AppendLine($"SPD: {data.speed}");

        if (data.critRate > 0)
            sb.AppendLine($"Crit Rate: {data.critRate}%");

        if (data.critDamage > 0)
            sb.AppendLine($"Crit Damage: {data.critDamage}%");

        StatsText.text = sb.ToString().TrimEnd();

        //-------------------------
        // Buttons
        //-------------------------

        //equipButton.GetComponentInChildren<TMP_Text>().text = item.isEquipped ? "Unequip" : "Equip";

        //lockButton.GetComponentInChildren<TMP_Text>().text = item.isLock ? "Unlock" : "Lock";
    }
}
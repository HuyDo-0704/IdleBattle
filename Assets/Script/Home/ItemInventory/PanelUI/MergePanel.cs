using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class MergePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<MergeSlotUI> mergeSlots;
    [SerializeField] private GameObject inventorContent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button mergeButton;
    [SerializeField] private TMP_Dropdown rarityDropdown;
    private EquipmentType? currentEquipmentFilter = null;
    private ItemRare? currentRareFilter = null;
    [Header("Merge Result")]
    [SerializeField] private GameObject VFXSuccess;
    [SerializeField] private GameObject VFXFailed;
    [SerializeField] private MergeSlotUI mergeResulSlots;
    private void Start()
    {
        mergeResulSlots.Clear();
        mergeButton.onClick.AddListener(OnClickMerge);

        rarityDropdown.onValueChanged.AddListener(OnRarityChanged);

        SpawnInventoryItem();

        Refresh();
    }
    private void OnEnable()
    {
        currentEquipmentFilter = null;
        currentRareFilter = null;

        rarityDropdown.SetValueWithoutNotify(0);

        SpawnInventoryItem();
        Refresh();
    }
    private void OnDisable()
    {
        MergeManager.Instance.Clear();
    }
    public void Refresh()
    {
        List<EquipmentItem> items = MergeManager.Instance.mergeItems;

        for (int i = 0; i < mergeSlots.Count; i++)
        {
            if (i < items.Count)
            {
                mergeSlots[i].SetItem(items[i]);
            }
            else
            {
                mergeSlots[i].Clear();
            }
        }

        mergeButton.interactable = items.Count == MergeManager.Instance.RequiredItemCount;
        RefreshInventorySelection();
    }

    private void OnClickMerge()
    {
        MergeManager.Instance.Merge();

        Refresh();
    }
    private void SpawnInventoryItem()
    {
        foreach (Transform child in inventorContent.transform)
        {
            Destroy(child.gameObject);
        }

        IEnumerable<EquipmentItem> equipments =
            ItemInventoryManager.Instance.ownedItems
            .OfType<EquipmentItem>();

        // Filter Type
        if (currentEquipmentFilter.HasValue)
        {
            equipments = equipments.Where(x =>
                x.Data.equipmentType == currentEquipmentFilter.Value);
        }

        // Filter Rare
        if (currentRareFilter.HasValue)
        {
            equipments = equipments.Where(x =>
                x.Data.defaultRare == currentRareFilter.Value);
        }

        equipments = equipments
            .OrderByDescending(x => x.Data.defaultRare);

        foreach (EquipmentItem equipment in equipments)
        {
            GameObject obj =
                Instantiate(itemPrefab, inventorContent.transform);

            ItemForgeFrefab forgeItem =
                obj.GetComponent<ItemForgeFrefab>();

            forgeItem.Setup(equipment);
        }
        RefreshInventorySelection();
    }
    public void SetEquipmentFilter(int type)
    {
        if (type == -1)
            currentEquipmentFilter = null;
        else
            currentEquipmentFilter = (EquipmentType)type;

        SpawnInventoryItem();
        Refresh();
    }
    public void ResetUI()
    {
        SpawnInventoryItem();
        Refresh();
    }
    public void RefreshInventorySelection()
    {
        foreach (Transform child in inventorContent.transform)
        {
            child.GetComponent<ItemForgeFrefab>()?.RefreshUI();
        }
    }
    private void OnRarityChanged(int index)
    {
        currentRareFilter = index == 0
            ? null
            : (ItemRare)index;

        SpawnInventoryItem();
        Refresh();
    }
    public void ShowMergeResult(EquipmentData data, bool isSuccess)
    {
        if (isSuccess)
        {
            mergeResulSlots.SetItem(data);

            VFXSuccess.SetActive(true);
        }
        else
        {
            mergeResulSlots.Clear();

            VFXFailed.SetActive(true);
        }
    }
}
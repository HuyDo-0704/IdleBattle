using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthesisPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<Image> mergeSlots;
    [SerializeField] private GameObject contentItem;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button mergeButton;

    private void Start()
    {
        mergeButton.onClick.AddListener(OnClickMerge);

        SpawnInventoryItem();

        Refresh();
    }

    public void Refresh()
    {
        List<EquipmentItem> items =
            SynthesisManager.Instance.mergeItems;

        for (int i = 0; i < mergeSlots.Count; i++)
        {
            if (i < items.Count)
            {
                mergeSlots[i].enabled = true;
                mergeSlots[i].sprite = items[i].Data.icons;
            }
            else
            {
                mergeSlots[i].sprite = null;
                mergeSlots[i].enabled = false;
            }
        }

        mergeButton.interactable =
            items.Count ==
            SynthesisManager.Instance
            .GetComponent<SynthesisManager>()
            .GetComponent<SynthesisManager>()
            .mergeItems.Count;
    }

    private void OnClickMerge()
    {
        SynthesisManager.Instance.Merge();

        Refresh();
    }
    private void SpawnInventoryItem()
    {
        foreach(Transform child in contentItem.transform)
        {
            Destroy(child.gameObject);
        }


        foreach(Item item in ItemInventoryManager.Instance.ownedItems)
        {
            if(item is EquipmentItem equipment)
            {
                GameObject obj =
                    Instantiate(itemPrefab, contentItem.transform);


                ItemForgeFrefab forgeItem =
                    obj.GetComponent<ItemForgeFrefab>();

                forgeItem.Setup(equipment);
            }
        }
    }
}
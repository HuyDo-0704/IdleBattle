using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ItemInventoryManager : MonoBehaviour
{
    public static ItemInventoryManager Instance;
    [Header("Currencies")]
    public int GoldCoin; // vàng của người chơi 
    public int DiamonCoin;
    public List<Item> ownedItems = new List<Item>();

    private string SavePath =>
        Path.Combine(Application.persistentDataPath,
        "ItemInventory.json");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //========================
    // ADD
    //========================
    public void Start ()
    {
        LoadInventory();
    }
    public void AddItem(ItemData data)
    {
        Item item = null;

        switch (data)
        {
            case EquipmentData equipmentData:

                EquipmentItem equipment = new EquipmentItem();
                equipment.Data = equipmentData;

                item = equipment;

                break;

            case ConsumableData:
                //item = new ConsumableItem();
                break;

            case MaterialData:
                //item = new MaterialItem();
                break;

            //case ChestData:
            //    item = new ChestItem();
            //    break;

            default:
                Debug.LogError($"Unknown ItemData type: {data.GetType().Name}");
                return;
        }

        item.itemID = data.itemID;
        item.level = 1;
        item.star = 1;

        ownedItems.Add(item);

        SaveInventory();

        Debug.Log($"Receive Item : {data.itemName}");
    }

    //========================
    // REMOVE
    //========================

    public void RemoveItem(string uid)
    {
        ownedItems.RemoveAll(x => x.uid == uid);

        SaveInventory();
    }

    //========================
    // SAVE
    //========================

    public void SaveInventory()
    {
        string json =
            JsonUtility.ToJson(
                new ItemListWrapper(ownedItems),
                true);

        File.WriteAllText(
            SavePath,
            json);
    }

    //========================
    // LOAD
    //========================

    public void LoadInventory()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);

        ItemListWrapper wrapper =
            JsonUtility.FromJson<ItemListWrapper>(json);

        ownedItems.Clear();

        foreach (Item item in wrapper.items)
        {
            Debug.Log(DataManager.Instance);

            Debug.Log(DataManager.Instance?.DataGame);

            Debug.Log(DataManager.Instance?.DataGame?.itemDatabase);

            ItemData data = DataManager.Instance.DataGame.itemDatabase.GetItem(item.itemID);

            Item runtimeItem = CreateRuntimeItem(data);

            runtimeItem.uid = item.uid;
            runtimeItem.itemID = item.itemID;
            runtimeItem.level = item.level;
            runtimeItem.star = item.star;
            runtimeItem.isLock = item.isLock;
            runtimeItem.isEquipped = item.isEquipped;

            ownedItems.Add(runtimeItem);
        }
    }
    private Item CreateRuntimeItem(ItemData data)
    {
        switch (data)
        {
            case EquipmentData equipmentData:
                return new EquipmentItem()
                {
                    Data = equipmentData
                };

            case ConsumableData:
                //return new ConsumableItem();
                return new Item();

            case MaterialData:
                //return new MaterialItem();
                return new Item();

            //case ChestData:
            //    return new ChestItem();

            default:
                Debug.LogError($"Unknown ItemData: {data.GetType().Name}");
                return new Item();
        }
    }
}
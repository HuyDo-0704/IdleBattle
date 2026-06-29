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

        LoadInventory();
    }

    //========================
    // ADD
    //========================

    public void AddItem(ItemData data , ItemRare rare)
    {
        Item item = new Item();

        item.itemID = data.itemID;
        item.level = 1;
        item.star = 1;
        item.itemRare = rare;
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

        string json =
            File.ReadAllText(SavePath);

        ItemListWrapper wrapper =
            JsonUtility.FromJson<ItemListWrapper>(json);

        ownedItems = wrapper.items;
    }
}
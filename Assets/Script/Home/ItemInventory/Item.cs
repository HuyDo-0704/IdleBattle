using System;

[Serializable]
public class Item
{
    // ID duy nhất của item
    public string uid;

    // ID ItemData
    public int itemID;

    // Runtime
    public int level;
    public int star;
    public ItemRare itemRare;
    public bool isLock;
    public bool isEquipped;

    public Item()
    {
        uid = Guid.NewGuid().ToString();
    }
}
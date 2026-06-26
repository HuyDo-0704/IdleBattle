using System;
using System.Collections.Generic;

[Serializable]
public class ItemListWrapper
{
    public List<Item> items;

    public ItemListWrapper(List<Item> items)
    {
        this.items = items;
    }
}
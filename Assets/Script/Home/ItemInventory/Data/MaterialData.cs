using UnityEngine;

[CreateAssetMenu(menuName ="Idle RPG/Item/Material")]
public class MaterialData : ItemData
{
    //public MaterialType materialType;

    public bool stackable = true;

    public int maxStack = 9999;
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRewardUI  : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    // về sau thêm sau
    public void Setup(ItemData itemData, ItemRare rare)
    {

        icon.sprite = itemData.GetSpriteIcon(rare);
    }
}
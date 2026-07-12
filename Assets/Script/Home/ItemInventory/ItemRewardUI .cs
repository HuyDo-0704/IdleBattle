
using UnityEngine;
using UnityEngine.UI;

public class ItemRewardUI  : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Image rareimage;
    // về sau thêm sau
    public void Setup(ItemData itemData, ItemRare rare)
    {

        icon.sprite = itemData.icons;
        rareimage.color = DataManager.Instance.DataGame.GetColorByRarity(rare);
    }
    // lấy màu dựa theo rarity
}
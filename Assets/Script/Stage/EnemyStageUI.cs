
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStageUI : MonoBehaviour
{
    [SerializeField] private GameObject StarContainer; // nơi chứa các hình ảnh sao
    [SerializeField] private Image EnemyIcon; // nơi chứa hình ảnh các enemy
    [SerializeField] private TMP_Text LevelText; // level của enemy
    public void UpdateEnemyUI(Character enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Enemy is null. Cannot update UI.");
            return;
        }

        EnemyIcon.sprite = enemy.currentStats.baseStats.characterIcon;
        LevelText.text = $"Lv. {enemy.currentStats.CurrentLevel}";

        // Cập nhật số lượng sao
        GetSpriteStar(enemy);
    }
    public void GetSpriteStar(Character character)
    {
        int star = character.star;

        Sprite emptyStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Empty);
        Sprite yellowStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Yellow);
        Sprite redStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Red);
        Sprite diamondStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Diamond);

        Image[] starImages = StarContainer.GetComponentsInChildren<Image>();

        // reset
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = emptyStar;
        }

        // logic
        if (star <= 5)
        {
            for (int i = 0; i < star; i++)
            {
                starImages[i].sprite = yellowStar;
            }
        }
        else if (star <= 10)
        {
            int redCount = star - 5;

            for (int i = 0; i < redCount; i++)
            {
                starImages[i].sprite = redStar;
            }
        }
        else
        {
            int diamondCount = star - 10;

            for (int i = 0; i < diamondCount && i < starImages.Length; i++)
            {
                starImages[i].sprite = diamondStar;
            }
        }
    }
}

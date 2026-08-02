using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterExpUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text addExpText;
    [SerializeField] private TMP_Text levelText;

    private Character character;

    public void Setup(Character character)
    {
        this.character = character;

        icon.sprite = character.currentStats.baseStats.characterIcon;

        UpdateUI();
    }

    private void UpdateUI()
    {
        levelText.text = $"Lv.{character.CurrentLevel}";

        int needExp = character.GetMaxExp();

        expSlider.maxValue = needExp;
        expSlider.value = character.CurrentExp;

        expText.text = $"{character.CurrentExp}/{needExp}";
    }

    public IEnumerator PlayExpAnimation(int addExp)
    {
        StartCoroutine(AnimateAddExp(addExp));

        yield return new WaitForSeconds(0.4f);

        int remainExp = addExp;

        while (remainExp > 0)
        {
            int needExp = character.GetMaxExp();

            int expToLevelUp = needExp - character.CurrentExp;

            if (remainExp <= expToLevelUp)
            {
                yield return AnimateExp(
                    character.CurrentExp,
                    character.CurrentExp + remainExp,
                    needExp);

                character.CurrentExp += remainExp;

                remainExp = 0;
            }
            else
            {
                yield return AnimateExp(
                    character.CurrentExp,
                    needExp,
                    needExp);

                remainExp -= expToLevelUp;

                character.CurrentLevel++;
                character.CurrentExp = 0;

                UpdateUI();

                yield return new WaitForSeconds(0.2f);
            }
        }

        UpdateUI();
    }

    private IEnumerator AnimateExp(int from, int to, int maxExp)
    {
        float time = 0f;
        float duration = 1.4f;

        expSlider.maxValue = maxExp;

        while (time < duration)
        {
            time += Time.deltaTime;

            float value = Mathf.Lerp(from, to, time / duration);

            expSlider.value = value;

            expText.text = $"{Mathf.RoundToInt(value)}/{maxExp}";

            yield return null;
        }

        expSlider.value = to;
        expText.text = $"{to}/{maxExp}";
    }
    private IEnumerator AnimateAddExp(int targetExp)
    {
        float duration = 1.4f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            int value = Mathf.RoundToInt(
                Mathf.Lerp(0, targetExp, time / duration));

            addExpText.text = $"+{value} EXP";

            yield return null;
        }

        addExpText.text = $"+{targetExp} EXP";
    }
}
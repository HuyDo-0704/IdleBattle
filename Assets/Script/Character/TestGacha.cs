using UnityEngine;

public class TestHero : MonoBehaviour
{
    [SerializeField] private int level = 5;

    public void SummonCharacter()
    {
        foreach (CharacterBaseStats baseStats in DataManager.Instance.DataGame.characterBaseStats)
        {
            if (baseStats == null)
                continue;

            Character character =
                GameManager.Instance.CreateCharacter(baseStats.characterID, level);

            CharacterInventoryManager.Instance.AddCharacter(character);
        }

        Debug.Log("Summon All Characters Complete!");
    }
}
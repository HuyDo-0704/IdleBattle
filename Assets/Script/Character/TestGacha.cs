using UnityEngine;

public class TestGacha : MonoBehaviour
{
    public CharacterBaseStats characterBaseStats;
    public void SummonCharacter()
    {

        Character character =
            GameManager.Instance.CreateCharacter(characterBaseStats.characterID);

        CharacterInventoryManager.Instance.AddCharacter(character);
    }
}

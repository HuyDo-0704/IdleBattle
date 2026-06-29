using UnityEngine;
public enum CardType
{
    ButtonShowInfo
    
}
public class CardButton : MonoBehaviour
{
    public InfoCharButton infoButton;
    public GameObject Status;
    public void UpdateCard(Character character)
    {
        infoButton.UpdateCard(character);
        CheckStatus(character);
    }
    public void CheckStatus(Character character)
    {   
        // Update status GameObject
        if (Status == null)
        {
            return;
        }
        Status.SetActive(character.isLineup);
    }
    public void OnCardButtonClicked()
    {
        if (CharacterInventoryManager.Instance == null)
        {
            Debug.LogWarning("CharacterInventory Instance is null.");
            return;
        }

        int index = infoButton.characterID;

        if (index < 0 || index >= CharacterInventoryManager.Instance.ownedCharacters.Count)
        {
            Debug.LogWarning($"CharacterID {infoButton.characterID} is out of range.");
            return;
        }

        Character character = CharacterInventoryManager.Instance.ownedCharacters[index];
        CharDetail.Instance.UpdateInfo(character, infoButton.characterID);
        CharDetail.Instance.OpenPanel();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CharDetail : MonoBehaviour
{
    public static CharDetail Instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text charName;
    [SerializeField] Image RarityFrame;
    [SerializeField] GameObject CharStand;
    [SerializeField] StatsIndex statsIndex;
    private Animator animator;
    int currentCharID;
    void Start()
    {
        Instance = this;
        panel.SetActive(false);
        animator = GetComponent<Animator>();
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
        animator.SetTrigger("Info");
    }
    // Update is called once per frame
    public void UpdateInfo( Character character , int charID )
    {

        charName.text = character.currentStats.baseStats.characterName;
        //RarityFrame.sprite = charBase.rarityFrame;
        statsIndex.UpdateStats(character);
        // Xóa CharStand cũ nếu có
        foreach (Transform child in CharStand.transform)
        {
            Destroy(child.gameObject);
        }

        // Tạo CharStand mới
        if (character.currentStats.baseStats.characterPrefab != null)
        {
            GameObject charInstance = Instantiate(character.currentStats.baseStats.characterPrefab, CharStand.transform);
            // set position local về (0,0,0)
            charInstance.transform.localPosition = Vector3.zero;
            // set scale local về (1,1,1)
            charInstance.transform.localScale = Vector3.one;

        }
        currentCharID = charID;
    }
    public void NextChar()
    {
        int nextCharID = currentCharID + 1;
        int index = nextCharID;

        if (index < 0 || index >= CharacterInventoryManager.Instance.ownedCharacters.Count)
        {
            Debug.Log("Reached last character.");
            return;
        }

        Character nextChar = CharacterInventoryManager.Instance.ownedCharacters[index];
        UpdateInfo(nextChar, nextCharID);
    }
    public void PreviousChar()
    {
        int prevCharID = currentCharID - 1;
        int index = prevCharID;

        if (index < 0 || index >= CharacterInventoryManager.Instance.ownedCharacters.Count)
        {
            Debug.Log("Reached first character.");
            return;
        }

        Character prevChar = CharacterInventoryManager.Instance.ownedCharacters[index];
        UpdateInfo(prevChar, prevCharID);
    }
}

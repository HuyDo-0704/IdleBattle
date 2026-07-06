using UnityEngine;
using UnityEngine.UI;

public class CharDetail : MonoBehaviour
{
    public static CharDetail Instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text charName;
    [SerializeField] Image RarityFrame;
    [SerializeField] GameObject CharStand;
    [SerializeField] GameObject Star;
    public StatsIndex statsIndex;
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
        GetSpriteStar(character);
        EquipmentManager.Instance.UI.Show(character);
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
    public void GetSpriteStar(Character character)
    {
        int star = character.star;

        Sprite emptyStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Empty);
        Sprite yellowStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Yellow);
        Sprite redStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Red);
        Sprite diamondStar = DataManager.Instance.DataGame.GetSpriteStar(StarType.Diamond);

        Image[] starImages = Star.GetComponentsInChildren<Image>();

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

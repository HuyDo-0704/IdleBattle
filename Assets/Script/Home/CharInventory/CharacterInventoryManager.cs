using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CharacterInventoryManager : MonoBehaviour
{
    public static CharacterInventoryManager Instance;

    public List<Character> ownedCharacters = new();

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "CharacterInventory.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadCharacters();
    }
    //========================
    // ADD
    //========================

    public void AddCharacter(Character character)
    {
        if (character == null)
            return;

        // tránh add trùng
        if (ownedCharacters.Exists(x => x.uid == character.uid))
            return;

        ownedCharacters.Add(character);

        SaveCharacters();
    }

    //========================
    // REMOVE
    //========================

    public void RemoveCharacter(string uid)
    {
        ownedCharacters.RemoveAll(x => x.uid == uid);
        SaveCharacters();
    }

    //========================
    // SAVE
    //========================

    public void SaveCharacters()
    {
        CharacterSaveWrapper wrapper = new CharacterSaveWrapper();

        foreach (Character c in ownedCharacters)
        {
            wrapper.characters.Add(new CharacterSaveData()
            {
                uid = c.uid,
                characterID = c.currentStats.baseStats.characterID,

                level = c.CurrentLevel,
                star = c.star,

                isLineup = c.isLineup,
                equipments = c.equipments
            });
        }

        File.WriteAllText(
            SavePath,
            JsonUtility.ToJson(wrapper, true));
    }

    //========================
    // LOAD
    //========================

    public void LoadCharacters()
    {
        if (!File.Exists(SavePath))
            return;

        CharacterSaveWrapper wrapper =
            JsonUtility.FromJson<CharacterSaveWrapper>(
                File.ReadAllText(SavePath));

        ownedCharacters.Clear();

        foreach (var save in wrapper.characters)
        {
            Character character = GameManager.Instance.CreateCharacter(
                save.characterID,
                save.level,
                save.star,
                save.uid,
                save.isLineup,
                save.equipments);

            if (character != null)
                ownedCharacters.Add(character);
        }
        foreach (Character c in ownedCharacters)
        {
            EquipmentManager.Instance.UpdateEquipmentStats(c);
        }
        Debug.Log($"Loaded {ownedCharacters.Count} Characters");
    }

    
    //========================
    // LINEUP
    //========================

    public List<Character> GetLineupCharacters()
    {
        return ownedCharacters.FindAll(c => c.isLineup);
    }

    //========================
    // RECALCULATE
    //========================

    public void InitializeAllCharacters()
    {
        foreach (Character character in ownedCharacters)
        {
            if (character == null)
                continue;

            character.currentStats.RecalculateStats(character.CurrentLevel);
        }

        Debug.Log($"Initialized {ownedCharacters.Count} characters");
    }
}
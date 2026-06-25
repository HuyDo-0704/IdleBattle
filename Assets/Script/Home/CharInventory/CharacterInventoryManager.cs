using UnityEngine;
using System.Collections.Generic;

public class CharacterInventoryManager : MonoBehaviour
{
    public static CharacterInventoryManager Instance;

    // Tất cả nhân vật mà người chơi đã sở hữu
    public List<Character> ownedCharacters = new List<Character>();

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

    // Lấy danh sách tướng đang được xếp vào đội hình
    public List<Character> GetLineupCharacters()
    {
        return ownedCharacters.FindAll(c => c.isLineup);
    }
    // cập nhập chỉ số của tướng trong túi đồ
    public void InitializeAllCharacters()
    {
        foreach (Character character in ownedCharacters)
        {
            if (character == null)
                continue;

            if (character.currentStats == null)
                continue;

            character.currentStats.InitializeStats();
        }

        Debug.Log($"Initialized {ownedCharacters.Count} characters");
    }
}

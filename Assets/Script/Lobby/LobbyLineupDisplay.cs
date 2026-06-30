using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class LobbyLineupDisplay : MonoBehaviour
{
    [SerializeField] private Transform lobby;

    private readonly List<GameObject> spawnedCharacters = new();

    private void Start()
    {
        SpawnLineup();
    }

    public void SpawnLineup()
    {
        foreach (PositionInLineup slot in Lineup.Instance.myLineup)
        {
            if (slot.CharIndex < 0 ||
                slot.CharIndex >= CharacterInventoryManager.Instance.ownedCharacters.Count)
            {
                Debug.LogWarning($"Invalid CharIndex {slot.CharIndex}");
                continue;
            }

            Character character =
                CharacterInventoryManager.Instance.ownedCharacters[slot.CharIndex];

            GameObject obj = Instantiate(
                character.currentStats.baseStats.characterPrefab,
                lobby
            );

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one * 150f;

            CharacterManager manager = obj.GetComponent<CharacterManager>();
            manager.currentState = State.Idle;
            manager.CheckState();

            // Lưu lại object đã spawn
            spawnedCharacters.Add(obj);

            Debug.Log($"Spawn {character.currentStats.baseStats.characterName} at {slot.position}");
        }
    }

    public void ClearLineup()
    {
        foreach (GameObject obj in spawnedCharacters)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawnedCharacters.Clear();
    }

    public void RefreshLineup()
    {
        ClearLineup();
        SpawnLineup();
    }
}
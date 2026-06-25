using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Queue<CharacterManager> turnQueue =
        new Queue<CharacterManager>();

    public void SetUp()
    {
        List<CharacterManager> allCharacters =
            new List<CharacterManager>();

        allCharacters.AddRange(
            BattleManager.Instance.playerTeam.members);

        allCharacters.AddRange(
            BattleManager.Instance.enemyTeam.members);

        StartBattle(allCharacters);

        StartCoroutine(BattleLoop());
    }

    public void StartBattle(List<CharacterManager> allCharacters)
    {
        Debug.Log($"Total Character : {allCharacters.Count}");

        allCharacters.Sort(
            (a, b) =>
            b.Stats.CSpeed.CompareTo(
                a.Stats.CSpeed));

        foreach (var character in allCharacters)
        {
            Debug.Log(
                $"Queue Add : {character.name} SPD:{character.Stats.CSpeed}");

            turnQueue.Enqueue(character);
        }
    }

    private IEnumerator BattleLoop()
    {
        while (!BattleManager.Instance.CheckBattleEnd())
        {
            yield return StartCoroutine(NextTurn());

            yield return new WaitForSeconds(1f);
        }

        Debug.Log("=== BATTLE END ===");
    }

    private IEnumerator NextTurn()
    {
        if (turnQueue.Count == 0)
        {
            Debug.LogError("Queue Empty");
            yield break;
        }

        CharacterManager current =
            turnQueue.Dequeue();

        Debug.Log(
            $"TURN : {current.name} HP:{current.Hud.currentHealth}");

        if (!current.Hud.IsDead)
        {
            yield return StartCoroutine(
                current.Acting());
        }

        turnQueue.Enqueue(current);
    }
}
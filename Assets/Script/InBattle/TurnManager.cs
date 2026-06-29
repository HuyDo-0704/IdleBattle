using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Queue<CharacterManager> turnQueue = new Queue<CharacterManager>();

    [Header("Round")]
    [SerializeField] private int maxRound = 20;

    public int CurrentRound { get; private set; }

    public void SetUp()
    {
        StopAllCoroutines();

        turnQueue.Clear();

        CurrentRound = 1;

        BuildRoundQueue();

        Debug.Log($"===== ROUND {CurrentRound} =====");

        StartCoroutine(BattleLoop());
    }

    private void BuildRoundQueue()
    {
        turnQueue.Clear();

        List<CharacterManager> aliveCharacters = new List<CharacterManager>();

        foreach (CharacterManager character in BattleManager.Instance.playerTeam.members)
        {
            if (!character.Hud.IsDead)
                aliveCharacters.Add(character);
        }

        foreach (CharacterManager character in BattleManager.Instance.enemyTeam.members)
        {
            if (!character.Hud.IsDead)
                aliveCharacters.Add(character);
        }

        aliveCharacters.Sort(
            (a, b) => b.CChartacter.currentStats.CSpeed.CompareTo(a.CChartacter.currentStats.CSpeed));

        Debug.Log($"Round {CurrentRound} - Alive : {aliveCharacters.Count}");

        foreach (CharacterManager character in aliveCharacters)
        {
            Debug.Log($"Queue Add : {character.name} SPD:{character.CChartacter.currentStats.CSpeed}");

            turnQueue.Enqueue(character);
        }
    }

    private IEnumerator BattleLoop()
    {
        while (!BattleManager.Instance.CheckBattleEnd())
        {
            if (CurrentRound > maxRound)
            {
                Debug.Log("Max Round Reached!");

                BattleManager.Instance.BattleLose();

                yield break;
            }

            while (turnQueue.Count > 0)
            {
                yield return StartCoroutine(NextTurn());

                if (BattleManager.Instance.CheckBattleEnd())
                    yield break;

                yield return new WaitForSeconds(1f);
            }

            CurrentRound++;

            Debug.Log($"===== ROUND {CurrentRound} =====");

            BuildRoundQueue();
        }

        Debug.Log("=== BATTLE END ===");
    }

    private IEnumerator NextTurn()
    {
        if (turnQueue.Count == 0)
            yield break;

        CharacterManager current = turnQueue.Dequeue();

        Debug.Log($"TURN : {current.name}");

        if (!current.Hud.IsDead)
        {
            yield return StartCoroutine(current.Acting());
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class StatsIndex : MonoBehaviour
{
    [Header("Current Stats UI")]
    public Text level;
    public Text Atk;
    public Text Def;
    public Text Hp;
    public Text Speed;
    public Text CritRate;
    public Text CritDmg;
    public Text Power; // thêm sau

    // Update is called once per frame
    public void UpdateStats(Character character)
    {
        level.supportRichText = true;
        Atk.supportRichText = true;
        Def.supportRichText = true;
        Hp.supportRichText = true;
        Speed.supportRichText = true;
        CritRate.supportRichText = true;
        CritDmg.supportRichText = true;

        level.text = character.CurrentLevel.ToString("F0");

        Atk.text = "<color=#FF4D00>ATK</color> : <color=white>" 
            + character.currentStats.CAttack.ToString("F0") + "</color>";

        Hp.text = "<color=#00C853>HP</color> : <color=white>" 
            + character.currentStats.MHealth.ToString("F0") + "</color>";

        Def.text = "<color=#FFD600>DEF</color> : <color=white>" 
            + character.currentStats.CDef.ToString("F0") + "</color>";

        Speed.text = "<color=#2196F3>SPD</color> : <color=white>" 
            + character.currentStats.CSpeed.ToString("F1") + "</color>";

        CritRate.text = "<color=#FF9800>CR</color> : <color=white>" 
            + character.currentStats.CCriticalRate.ToString("F1") + "%</color>";

        CritDmg.text = "<color=#D32F2F>CD</color> : <color=white>" 
            + character.currentStats.CCriticalDamage.ToString("F1") + "%</color>";
    }

}

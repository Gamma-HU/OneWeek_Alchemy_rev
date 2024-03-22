using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Test : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    [SerializeField]
    BattleManager.Action bleed;
    [SerializeField]
    BattleManager.Action remove;
    int count;

    public override void OnBattleStart()
    {
        battleManager.Enqueue(character, character, bleed);
    }
    public override void OnAttack(int DMG, bool missed)
    {
        count++;
        if (count == 2)
        {
            count = 0;
            battleManager.Enqueue(character, character, remove);
        }
        FindObjectOfType<BattleManager>().Enqueue(character, character, action);
    }
}
